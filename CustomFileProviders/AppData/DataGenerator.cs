using CustomFileProviders.AppData.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomFileProviders.AppData
{
    public class DataGenerator
    {
        //public static async Task Generate(IHost host)
        public static async Task Generate(IServiceProvider services)
        {
            // IServiceScope scope = host.Services.CreateScope();
            // IServiceProvider services = scope.ServiceProvider;

            IWebHostEnvironment environment = services.GetRequiredService<IWebHostEnvironment>();

            if (environment.IsDevelopment())
            {
                // Since DbContext is a scoped service, NOT a singleton service:
                IServiceScope scope = services.CreateScope();

                AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                // if (context.Database.EnsureCreated()) await context.Database.MigrateAsync();

                // context.Database.EnsureCreated(); // only created tables based on model

                await context.Database.MigrateAsync(); // apply migrations to database

                if (!context.DbViews.Any()) PopulateSampleData(environment, context);
            }
        }

        private static void PopulateSampleData(IWebHostEnvironment environment, AppDbContext context)
        {
            ViewEntity view = new ViewEntity();

            view.Location = "/Views/DbViews/Index.cshtml";
            view.Content = "Welcome! @System.DateTime.Now";
            view.LastModified = DateTime.MinValue;
            view.LastRequested = DateTime.MinValue;

            context.DbViews.Add(view);

            context.SaveChanges();
        }
    }
}
