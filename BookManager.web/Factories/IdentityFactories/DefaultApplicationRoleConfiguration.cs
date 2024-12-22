using BookManager.web.Constants;
using BookManager.web.Data.Models.Identity;
using BookManager.web.Services.IdentityServices.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace BookManager.web.Factories.IdentityFactories
{
    public class DefaultApplicationRoleConfiguration
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<DefaultApplicationRoleConfiguration> _logger;
        private readonly ApplicationAdminRoleUsersConfigDto _userConfig;
        public DefaultApplicationRoleConfiguration(
            IServiceProvider services,
            ILogger<DefaultApplicationRoleConfiguration> logger,
            IOptions<ApplicationAdminRoleUsersConfigDto> usersConfig
            )
        {
            _services = services;
            _logger = logger;
            _userConfig = usersConfig.Value;
        }

        public async Task CreateDefaultRoles()
        {
            try
            {
                using var scope = _services.CreateScope();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

                var defaultRoles = Utility.GetDefaultRoleList();

                foreach (KeyValuePair<int, string> role in defaultRoles)
                {
                    if (!await roleManager.RoleExistsAsync(role.Value))
                    {
                        ApplicationRole applicationRole = new ApplicationRole();
                        //applicationRole.Id = role.Key;
                        applicationRole.Name = role.Value;

                        await roleManager.CreateAsync(applicationRole);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Application default role creation failed - error message : {ex.Message}");
                throw;
            }
        }

        public async Task ApplicationAdminUserCreation()
        {
            try
            {
                var application_admins = _userConfig?.ApplicationAdminRoleUsers;

                if (application_admins is not null)
                {
                    using var scope = _services.CreateScope();
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                    foreach (var adminRoleUser in application_admins)
                    {
                        if (await userManager.FindByEmailAsync(adminRoleUser.Email) == null) 
                        {
                            var user = new ApplicationUser
                            {
                                FirstName = adminRoleUser.FirstName,
                                LastName = adminRoleUser.LastName,
                                Email = adminRoleUser.Email,
                                UserName = adminRoleUser.Email,
                                EmailConfirmed = true
                            };

                            await userManager.CreateAsync(user, adminRoleUser.Password);

                            await userManager.AddToRoleAsync(user, DefaultRoles.ApplicationAdmin.ToString());
                            await userManager.AddToRoleAsync(user, DefaultRoles.User.ToString()); 
                        }
                        else
                        {
                            var existUser = await userManager.FindByEmailAsync(adminRoleUser.Email);

                            if (!await userManager.IsInRoleAsync(existUser, DefaultRoles.ApplicationAdmin.ToString())) 
                                await userManager.AddToRoleAsync(existUser, DefaultRoles.ApplicationAdmin.ToString());

                            //if (await userManager.IsInRoleAsync(existUser, DefaultRoles.User.ToString())) // Remove user Role
                            //	await userManager.RemoveFromRoleAsync(existUser, DefaultRoles.User.ToString());
                        }
                    }

                    foreach (var user in userManager.Users.ToList())
                    {

                        if (await userManager.IsInRoleAsync(user, DefaultRoles.ApplicationAdmin.ToString())) // Remove Admin Role

                            if (!application_admins.Any(applicationAdmin => applicationAdmin.Email.Equals(user.Email)))

                                await userManager.RemoveFromRoleAsync(user, DefaultRoles.ApplicationAdmin.ToString());


                        if (!await userManager.IsInRoleAsync(user, DefaultRoles.ApplicationAdmin.ToString())) // Map user Role

                            if (!await userManager.IsInRoleAsync(user, DefaultRoles.User.ToString()))

                                if (!application_admins.Any(x => x.Email.Equals(user.Email)))

                                    await userManager.AddToRoleAsync(user, DefaultRoles.User.ToString());

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Application default user creation failed - error message : {ex.Message}");
                throw;
            }
        }
    }
}
