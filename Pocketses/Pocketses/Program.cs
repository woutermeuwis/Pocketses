using Microsoft.EntityFrameworkCore;
using Pocketses.Core.DataAccessLayer;
using Pocketses.Core;

var builder = WebApplication.CreateBuilder(args);

// setup database
builder.Services.AddDbContext<PocketsesContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("PocketsesContext")));

// Add services to the DI Container
builder.Services.AddControllersWithViews();

// Add project specific configs
builder.Services.InitializeCore();


// Build app
var app = builder.Build();

// initialize core project
app.Services.InitializeCore();

// Configure the HTTP request pipeline
if(!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // default value is 30 days [https://aka.ms/aspnetcore-hsts]
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();