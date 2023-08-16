using System.Security.Claims;
using System.Text;
using Cryptography;
using CryptoMarketplace.Utils;
using Entities.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository.Contracts;
using Repository.Repository;
using Service.Contracts;
using Service.LoggerService;
using Service.Services;
using Shared.Utility;

namespace CryptoMarketplace.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
    {
 
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder
                    .WithOrigins(
                        "http://0.0.0.0:3000"
                        , "https://0.0.0.0:3000"
                        , "http://localhost:3000"
                        , "https://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
        });
    }
    

    public static void ConfigureIISIntegration(this IServiceCollection services) =>
        services.Configure<IISOptions>(options => { });

    
    public static void ConfigureLoggerService(this IServiceCollection services) =>
        services.AddSingleton<ILoggerManager, LoggerManager>();

    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void ConfigureServiceManager(this IServiceCollection services) =>
        services.AddScoped<IServiceManager, ServiceManager>();

    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<RepositoryContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking), ServiceLifetime.Transient);

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        var builder = services.AddIdentity<ApplicationUser, ApplicationRole>(o =>
        {
            o.Password.RequireDigit = true;
            o.Password.RequireLowercase = false;
            o.Password.RequireUppercase = false;
            o.Password.RequiredLength = 8;
            o.Password.RequireNonAlphanumeric = false;
        }).AddEntityFrameworkStores<RepositoryContext>().AddDefaultTokenProviders();
    }

    public static void ConfigureResponseCaching(this IServiceCollection services) => services.AddResponseCaching();

    public static void ConfigureJwtUtils(this IServiceCollection services) =>
        services.AddSingleton<IJwtUtils, JwtUtils>();

    public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration) =>
        services.Configure<JwtConfiguration>(configuration.GetSection("JwtSettings"));
    

    public static void ConfigureTokenLifetime(this IServiceCollection services) =>
        services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromHours(2));

    public static void AddCryptographyUtils(this IServiceCollection services) =>
        services.AddScoped<ICryptoUtils, CryptoUtils>();


    public static void AddAuthenticationCookie(this IServiceCollection services) =>
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = new PathString("/Authentication/Login");
                options.AccessDeniedPath = new PathString("/Authentication/Login");
                options.LogoutPath = new PathString("/Authentication/Logout");
                options.SlidingExpiration = true;
                options.Cookie.Path = "/";
            });

    //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    //    .AddCookie(options =>
    //    {
    //        options.LoginPath = new PathString("/../Views/Authentication/Login");
    //        options.ExpireTimeSpan = TimeSpan.FromHours(2);
    //    });


    public static void ConfigureSignalR(this IServiceCollection services) =>
        services.AddSignalR().AddJsonProtocol(options =>
        {
            options.PayloadSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        });

    public static void AddHubService(this IServiceCollection services) =>
        services.AddScoped<ICryptoHubService, CryptoHubService>();
}