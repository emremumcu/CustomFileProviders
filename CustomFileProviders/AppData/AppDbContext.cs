using CustomFileProviders.AppData.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CustomFileProviders.AppData
{
    public class AppDbContext: DbContext
    {
        // protected AppDbContext(DbContextOptions contextOptions) : base(contextOptions) { }
        // protected AppDbContext(DbContextOptions<AppDbContext> contextOptions) : base(contextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                // string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                // IConfigurationBuilder builder = new ConfigurationBuilder();
                // IConfigurationRoot configuration = new ConfigurationBuilder()
                //    .SetBasePath(Directory.GetCurrentDirectory())
                //    .AddJsonFile("data.json")
                //    .Build();
                // var connectionString = configuration.GetConnectionString("CMSConnectionString");

                optionsBuilder.UseSqlServer("Server=.;Database=DbFileProvider;Trusted_Connection=True;");
            }
        }

        public DbSet<ViewEntity> DbViews { get; set; }
    }
}
