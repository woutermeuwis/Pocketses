using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pocketses.Api;
using Pocketses.Core;
using Pocketses.Core.DataAccessLayer;
using Pocketses.Core.Models;
using System.Reflection;
using System.Text;


WebApplication.CreateBuilder(args)
	.ConfigureServices()
	.Build()
	.ConfigureWebApplication()
	.Run();

/// <summary>
/// Extensions class to simplify program config
/// </summary>
public static class ProgramExtensions
{
	/// <summary>
	/// Configure Services for the WebApplicationBuilder
	/// </summary>
	public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
	{
		builder.ConfigureAutofac();
		builder.ConfigureDatabase();

		AddAuthentication(builder.Services, builder.Configuration);
		builder.Services.AddHttpContextAccessor();
		builder.Services.AddControllers();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(config =>
		{
			config.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "PocketsesApiAnnotation.xml"));
		});

		return builder;
	}

	/// <summary>
	/// Configure WebApplication
	/// </summary>
	public static WebApplication ConfigureWebApplication(this WebApplication app)
	{
		if (app.Environment.IsDevelopment())
			app.ConfigureForDevelopment();

		app.Services.RunDatabaseMigrations();
		app.Services.InitializeCore();


		app.UseHttpsRedirection();
		app.UseAuthentication();
		app.UseAuthorization();
		app.MapControllers();

		return app;
	}

	private static void ConfigureAutofac(this WebApplicationBuilder builder)
	{
		builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
		builder.Host.ConfigureContainer<ContainerBuilder>(b => b.RegisterModule(new ApiAutofacModule()));
	}

	private static void ConfigureDatabase(this WebApplicationBuilder builder)
	{
		var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
		var dbPath = Path.Join(path, "Pocketses.db");
		builder.Services.AddDbContext<PocketsesContext>(options => options.UseSqlite($"Data Source={dbPath}"));
	}

	private static void RunDatabaseMigrations(this IServiceProvider services)
	{
		try
		{
			services.GetRequiredService<PocketsesContext>().Database.Migrate();
		}
		catch (Exception ex)
		{
			var logger = services.GetRequiredService<ILogger<Program>>();
			logger.LogError(ex, "An error occurred while migrating the database");
		}
	}

	private static void AddAuthentication(this IServiceCollection services, IConfiguration config)
	{
		services.AddIdentity<User, IdentityRole>()
			.AddEntityFrameworkStores<PocketsesContext>();

		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		}
		).AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = config["Tokens:Issuer"],
				ValidAudience = config["Tokens:Audience"],
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokens:Key"]))
			};
		});
	}

	private static void ConfigureForDevelopment(this WebApplication app)
	{
		app.UseSwagger();
		app.UseSwaggerUI();

		app.UseCors(builder =>
		{
			builder.AllowAnyOrigin();
			builder.AllowAnyMethod();
			builder.AllowAnyHeader();
		});
	}

	private static void ConfigureAutoMapper(Assembly assembly, ContainerBuilder builder)
	{
		var profiles = assembly.DefinedTypes.Where(t => typeof(Profile).IsAssignableFrom(t)).Select(t => Activator.CreateInstance(t) as Profile).ToArray();
		var configuration = new MapperConfiguration(cfg => cfg.AddProfiles(profiles));

#if DEBUG
		configuration.AssertConfigurationIsValid();
#endif

		builder
			.RegisterInstance(configuration.CreateMapper())
			.As<IMapper>()
			.SingleInstance();
	}

}