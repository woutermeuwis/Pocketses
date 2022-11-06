using Microsoft.EntityFrameworkCore;
using Pocketses.Core;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Pocketses.Web;
using Microsoft.AspNetCore.Identity;
using Pocketses.Core.DataAccessLayer;
using Autofac.Core;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

// setup autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new WebAutofacModule()));

// setup database
builder.Services.AddDbContext<PocketsesContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("PocketsesContext")));

// add identity
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
       .AddEntityFrameworkStores<PocketsesContext>();

// Add controllers to the DI Container
builder.Services.AddControllersWithViews();

// configure services
ConfigureServices(builder.Services, builder.Configuration);

// Build app
var app = builder.Build();

// initialize core project
app.Services.InitializeCore();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // default value is 30 days [https://aka.ms/aspnetcore-hsts]
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

MapControllerRoutes(app);
app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.Configure<IdentityOptions>(options =>
    {
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;

        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;

        options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.@+";
        options.User.RequireUniqueEmail = true;
    });

    services.ConfigureApplicationCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

        options.LoginPath = "/Identity/Account/Login";
        options.AccessDeniedPath = "/Identity/Account/AccessDenied";
        options.SlidingExpiration = true;
    });

}

void MapControllerRoutes(WebApplication app)
{
    app.MapControllerRoute(name: "area", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
}