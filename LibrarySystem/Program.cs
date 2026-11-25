using Microsoft.EntityFrameworkCore;
using LibrarySystem.Data;
using LibrarySystem.Extensions;
using LibrarySystem.Middlewares;

namespace LibrarySystem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Inversion Of Control Container (IoC) => Dependency Injection
            builder.Services.AddControllers();

            builder.Services.AddServices(builder.Configuration);

            var app = builder.Build();

            #region Seeding
            // Automatic Migration
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                using var dbContext = services.GetRequiredService<ApplicationDbContext>();
                await dbContext.Database.MigrateAsync();

                // FIX (from the example): the Authentication database is
                // migrated too. The example only migrated the main database,
                // so Register/Login failed until the Auth migration was
                // applied by hand.
                using var authDbContext = services.GetRequiredService<AuthenticationDbContext>();
                await authDbContext.Database.MigrateAsync();

                // FIX (from the example): the content root is passed in, so
                // the seed JSON files are found on any machine.
                await StoreContextSeeding.SeedAsync(dbContext, app.Environment.ContentRootPath);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();

                logger.LogError(ex, ex.Message);
            }

            #endregion

            app.UseMiddleware<RequestTimingMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.UseRequestInterceptor("""
                    function (request) {
                        const authorization = request.headers.Authorization || request.headers.authorization;

                        if (authorization && !/^Bearer\s/i.test(authorization)) {
                            request.headers.Authorization = `Bearer ${authorization}`;
                            delete request.headers.authorization;
                        }

                        return request;
                    }
                    """);
            });

            app.UseHttpsRedirection();

            // FIX (from the example): the CORS policy was registered in
            // AddServices but UseCors was never called, so it did nothing.
            app.UseCors("ConfigurationOrigins");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
