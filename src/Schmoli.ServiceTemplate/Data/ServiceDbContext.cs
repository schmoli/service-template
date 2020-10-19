using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Schmoli.ServiceTemplate.Data.Configurations;
using Schmoli.ServiceTemplate.Models;
using Schmoli.Services.Core.Data.Postgres;

namespace Schmoli.ServiceTemplate.Data
{
    public class ServiceDbContext : DbContext
    {
        private readonly PostgresOptions _settings;
        public ServiceDbContext(DbContextOptions<ServiceDbContext> options,
                                IOptions<PostgresOptions> settings)
            : base(options)
        {
            _settings = settings.Value;
        }

        public DbSet<PrimaryItem> PrimaryItems { get; set; }
        public DbSet<SecondaryItem> SecondaryItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new PrimaryItemEntityTypeConfiguration())
                .ApplyConfiguration(new SecondaryItemEntityTypeConfiguration())
                .HasPostgresExtension("citext");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql(_settings.ConnectionString,
                x => x.MigrationsHistoryTable("MigrationHistory", "migrations"));
        }
    }
}
