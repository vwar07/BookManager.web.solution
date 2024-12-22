using BookManager.web.Data;
using BookManager.web.Data.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BookManager.web.Data.Models.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using BookManager.web.Services.EmailService;
using Microsoft.AspNetCore.Identity.UI.Services;
using Hangfire;
using Hangfire.SqlServer;
using Serilog;
using BookManager.web.Services.IdentityServices.DTO;
using BookManager.web.Factories.IdentityFactories;
using BookManager.Web.Services.ApplicationHostedServices;
using Project.Tracker.Web.Services.IdentityServices;
using BookManager.web.Data.DataAccess;
using Microsoft.AspNetCore.Builder;
using BookManager.web.Data.DataAccess.BookRepo;
using BookManager.web.Data.DataAccess.CartRepo;
using BookManager.web.Data.DataAccess.OrderRepo;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("BookManagerConnectionString");

builder.Services.AddDbContext<BookManagerDbContext>(options =>
{
	options.UseSqlServer(connectionString).ConfigureWarnings(warnings => warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
});

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<BookManagerwebContext>();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
	options.SignIn.RequireConfirmedAccount = false;
	options.SignIn.RequireConfirmedEmail = true;
	options.User.RequireUniqueEmail = true;
	options.Lockout.AllowedForNewUsers = true;
	options.Lockout.MaxFailedAccessAttempts = 5;

})
	.AddRoles<ApplicationRole>()
	.AddRoleManager<RoleManager<ApplicationRole>>()
	.AddEntityFrameworkStores<BookManagerDbContext>()
	.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
	options.AccessDeniedPath = new PathString("/Account/AccessDenied");
	options.Cookie.Name = "Cookie";
	options.Cookie.HttpOnly = true;
	options.ExpireTimeSpan = TimeSpan.FromMinutes(720);
	options.LoginPath = new PathString("/identity/account/login");
	options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
	options.SlidingExpiration = true;
	options.LogoutPath = new PathString("/identity/account/logout");
});

builder.Services.AddAuthentication();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddScoped<ApplySeedDataConfiguration>();
builder.Services.AddSingleton<IEmailSender, EmailSender>();
builder.Services.AddSingleton<DefaultApplicationRoleConfiguration>();
builder.Services.AddHostedService<RoleInitializationHostedService>();
builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, CustomClaimsPrincipalFactory>();
builder.Services.AddTransient<UserResolverService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();


IConfiguration applicationAdminRoleConfiguration = new ConfigurationBuilder()
                            .AddJsonFile("Services/IdentityServices/ApplicationAdminUserConfig.json", optional: false, reloadOnChange: true)
                            .Build();
builder.Services.Configure<ApplicationAdminRoleUsersConfigDto>(
    applicationAdminRoleConfiguration.GetSection("ApplicationAdminRoleConfig"));



builder.Services.AddHangfire(config =>
{
	config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
	.UseSimpleAssemblyNameTypeSerializer()
	.UseRecommendedSerializerSettings()
	.UseStorage(
		new SqlServerStorage(
			connectionString,
			new SqlServerStorageOptions
			{
				QueuePollInterval = TimeSpan.FromSeconds(5),
				JobExpirationCheckInterval = TimeSpan.FromHours(1),
				CountersAggregateInterval = TimeSpan.FromMinutes(5),
				PrepareSchemaIfNecessary = true,
				DashboardJobListLimit = 25000,
				TransactionTimeout = TimeSpan.FromMinutes(1),
				//TablesPrefix = "Hangfire",
				
				InvisibilityTimeout = TimeSpan.FromDays(1),
			}
		));
});


var app = builder.Build();

app.Use(async (context, next) =>
{
	var userName = context.User.Identity.IsAuthenticated ? context.User.Identity.Name : "Unauthorized"; 
	Log.Logger.Error($"Unauthorized access error: Username- {userName}");
	await next();
	if (context.Response.StatusCode == 404)
	{
		context.Request.Path = "/not-found";
		await next();
	}
});


if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}


using (var scope = app.Services.CreateScope())
{
	var seedDataService = scope.ServiceProvider.GetRequiredService<ApplySeedDataConfiguration>();
	await seedDataService.ApplyExistingMigrationAsync();
	await seedDataService.SeedStoredProceduresAsync();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
	 name: "areas",
	 //pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}"
	 pattern: "{controller=Book}/{action=ViewBooks}");
   
	endpoints.MapRazorPages();

});

app.Run();
