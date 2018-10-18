#pragma warning disable 1591
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RB.JobAssistant.Data.Samples;
using RB.JobAssistant.Util;

namespace RB.JobAssistant.Data
{
    public sealed class JobAssistantContext : DbContext
    {
        private readonly ILogger<JobAssistantContext> _logger;

        public JobAssistantContext(DbContextOptions<JobAssistantContext> options) : base(options)
        {
            _logger = ApplicationLogging.CreateTypeLogger<JobAssistantContext>();
        }

        public JobAssistantContext()
        {
            if (!Database.EnsureCreated())
            {
                Database.Migrate();
                _logger.LogInformation("Database migration step triggered.  Verify database schema.");
            }
        }

        public DbSet<Trade> Trades { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Tool> Tools { get; set; }
        public DbSet<Accessory> Accessories { get; set; }

        public DbSet<Tenant> Tenants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _logger.LogDebug("Entered OnConfiguring method of JobAssistantContext.");
            if (optionsBuilder.IsConfigured == false)
                _logger.LogInformation("OptionsBuilder object is not configured. Check configuration settings.");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO: Represent relationship of entities to Tenant via DomainId.

            modelBuilder.Entity<Accessory>(entity =>
            {
                entity.HasKey(e => e.AccessoryId)
                    .HasName("PK_Accessories");

                entity.Property(e => e.Name).HasMaxLength(256);
            });

            modelBuilder.Entity<Application>(entity =>
            {
                entity.HasKey(e => e.ApplicationId)
                    .HasName("PK_Applications");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.HasOne(d => d.Material)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.MaterialId)
                    .HasConstraintName("FK_Applications_Materials");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.HasKey(e => e.JobId)
                    .HasName("PK_Jobs");

                entity.Property(e => e.Name).HasMaxLength(256);
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.HasKey(e => e.MaterialId)
                    .HasName("PK_Materials");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Materials)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_Materials_Jobs");
            });

            modelBuilder.Entity<Tool>(entity =>
            {
                entity.HasKey(e => e.ToolId)
                    .HasName("PK_Tools");

                entity.Property(e => e.Name).HasMaxLength(256);
            });

            // Many-to-many relationship from applications-to-tools and applications-to-accessories.
            //
            modelBuilder.Entity<ApplicationToolRelationship>().HasKey(t => new {t.ApplicationId, t.ToolId});
            modelBuilder.Entity<ApplicationAccessoryRelationship>().HasKey(t => new {t.ApplicationId, t.AccessoryId});

            // TODO: modelBuilder.Entity<ApplicationToolRelationship>().Ignore(r => r.Tools);

            // Many-to-many relationship from jobs-to-tools and jobs-to-accessories.
            //
            modelBuilder.Entity<JobToolRelationship>().HasKey(t => new {t.JobId, t.ToolId});
            modelBuilder.Entity<JobAccessoryRelationship>().HasKey(t => new {t.JobId, t.AccessoryId});

            // TODO: modelBuilder.Entity<ApplicationAccessoryRelationship>().Ignore(r => r.Accessories);
        }
    }

    public static class JobAssistantDbContextExtensions
    {
        public static void LoadSampleData(this JobAssistantContext dbContext)
        {
            SampleBoschGreenToolsDataSet.SeedBoschDiyGreenToolsGraphData(dbContext);
;           SampleDremelToolsDataSet.SeedDremelGraphData(dbContext);
            SampleBoschToolsDataSet.SeedBoschToolTradesGraphData(dbContext);
            SampleBoschToolsDataSet.SeedBoschToolsGraphData(dbContext);
        }
    }
}