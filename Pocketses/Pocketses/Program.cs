using Microsoft.EntityFrameworkCore;
using Pocketses.Core.DataAccessLayer;
using Pocketses.Core;
using Autofac;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using System.Reflection;
using Pocketses.Core.Repositories.Interfaces;
using Pocketses.Core.AppServices.Interfaces;
using Autofac.Extensions.DependencyInjection;
using Pocketses.Web;

var builder = WebApplication.CreateBuilder(args);

// setup autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new WebAutofacModule()));

// setup database
builder.Services.AddDbContext<PocketsesContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("PocketsesContext")));

// Add services to the DI Container
builder.Services.AddControllersWithViews();

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