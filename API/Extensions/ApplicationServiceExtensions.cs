using System.Text;
using API.Helpers;
using Application.UnitOfWork;
using AspNetCoreRateLimit;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

public static class AppServicesExtensions
{
    public static void ConfigureCors(this IServiceCollection services) => 
    services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy", builder =>
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
    );
    });
    public static void AddAppServices (this IServiceCollection services)
    {
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        

    }
    public static void ConfigureRatelimiting(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddInMemoryRateLimiting();
        services.Configure<IpRateLimitOptions>(options =>
        {
            options.QuotaExceededMessage = "¡Se superó la cuota de llamadas API! Máximo admitido 10 por Minuto";
            options.EnableEndpointRateLimiting = true;
            options.StackBlockedRequests = false;
            options.HttpStatusCode = 429;
            options.RealIpHeader = "X-Real-IP";
            options.GeneralRules = new List<RateLimitRule>
            {
                new() {
                    Endpoint = "*",
                    Period = "60s",
                    Limit = 10
                }
            };
        });
    }
    public static void ConfigureApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new QueryStringApiVersionReader("Ver"),
                new HeaderApiVersionReader("X-Version")
            );
        });
    }
       


}