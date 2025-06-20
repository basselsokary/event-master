﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EventMaster.Infrastructure;
using EventMaster.Infrastructure.Context;
using EventMaster.Infrastructure.User;
using EventMaster.Infrastructure.Authentication;
using EventMaster.Infrastructure.Repositories.Implementations;
using EventMaster.Application.Common.Interfaces.Repositories;
using System.Security.Claims;
using EventMaster.Application.Common.Interfaces.Authentication;
using EventMaster.Infrastructure.Email;
using EventMaster.Application.Common.Interfaces.Email;
using EventMaster.Infrastructure.User.Services;
using EventMaster.Application.Common.Interfaces.UnitOfWork;
using EventMaster.Infrastructure.Repositories;

namespace EventMaster.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration) =>
        services
            .AddServices()
            .AddDatabase(configuration)
            .AddAuthentication(configuration)
            .AddRepositories();

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IUserContext, CurrentUser>();

        services.AddTransient<IEmailSender, EmailSender>();

        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<ITokenProvider, TokenProvider>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddAuthorization();

        services.AddHttpContextAccessor();

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(option =>
            option.UseSqlServer(configuration.GetConnectionString("SqlServer")));

        services.AddIdentity<AppUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 3;
        }).AddEntityFrameworkStores<AppDbContext>()
          .AddDefaultTokenProviders();

        return services;
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));

        var jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();

        services.Configure<DataProtectionTokenProviderOptions>(options =>
            options.TokenLifespan = TimeSpan.FromDays(jwtSettings!.AccessTokenExpirationInMinutes));
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtSettings!.Issuer,

                ValidateAudience = true,
                ValidAudience = jwtSettings.Audience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),

                ValidateLifetime = true,

                NameClaimType = ClaimTypes.NameIdentifier // Important for UserIdentifier
            };
        });

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        return services;
    }
}
