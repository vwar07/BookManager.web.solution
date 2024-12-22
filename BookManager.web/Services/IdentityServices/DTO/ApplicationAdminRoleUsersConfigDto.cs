namespace BookManager.web.Services.IdentityServices.DTO;


public class ApplicationAdminRoleUsersConfigDto
{
    public List<AdminRoleUserDto> ApplicationAdminRoleUsers { get; set; }
}

public class AdminRoleUserDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

