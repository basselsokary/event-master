using EventMaster.Infrastructure;
using EventMaster.Application;
using EventMaster.Application.Hubs.Notification;
using EventMaster.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using EventMaster.Infrastructure.User;
using Microsoft.EntityFrameworkCore;

namespace EventMaster.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddPresentation()
            .AddApplication()
            .AddInfrastructure(builder.Configuration);

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            // var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            // var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            // context.Database.EnsureCreated();
            // context.Database.EnsureDeleted();
            // context.Database.Migrate();

            await DatabaseSeeder.SeedAsync(roleManager);
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        #region Middlewares
        // app.UseCors("AllowAllOrigins");
        app.UseExceptionHandler();
        
        app.UseCors();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();
        #endregion

        #region Map Endpoints
        app.MapHub<NotificationHub>("/hubs/notifications");
        #endregion

        app.Run();
    }
}
