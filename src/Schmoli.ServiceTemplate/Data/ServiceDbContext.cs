using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Schmoli.Services.Core.Data.Postgres;
using Schmoli.ServiceTemplate.Data.Configurations;
using Schmoli.ServiceTemplate.Models;

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
            ReadDatesAsUtc(builder);

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

        /// <summary>
        /// Set all read datetimes to UTC
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected void ReadDatesAsUtc(ModelBuilder modelBuilder)
        {
            var converter = new ValueConverter<DateTime, DateTime>(
                x => x,
                x => DateTime.SpecifyKind(x, DateTimeKind.Utc));
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(converter);
                    }
                }
            }
        }
    }
}
