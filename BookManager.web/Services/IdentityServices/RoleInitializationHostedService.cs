namespace BookManager.Web.Services.ApplicationHostedServices;

using BookManager.web.Factories.IdentityFactories;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

public class RoleInitializationHostedService : IHostedService
{

	private readonly DefaultApplicationRoleConfiguration _roleConfig;
	public RoleInitializationHostedService(DefaultApplicationRoleConfiguration roleConfig)
	{
		_roleConfig = roleConfig;
	}

	public async Task StartAsync(CancellationToken cancellationToken)
	{
		await _roleConfig.CreateDefaultRoles();
		await _roleConfig.ApplicationAdminUserCreation();
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}
}

