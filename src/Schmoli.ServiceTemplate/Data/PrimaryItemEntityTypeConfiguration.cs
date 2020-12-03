using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schmoli.ServiceTemplate.Models;

namespace Schmoli.ServiceTemplate.Data.Configurations
{
    /// <summary>
    /// Configure the schema for this service here.
    /// </summary>
    internal class PrimaryItemEntityTypeConfiguration : IEntityTypeConfiguration<PrimaryItem>
    {
        public void Configure(EntityTypeBuilder<PrimaryItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .UseIdentityColumn();

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(32)
                   .HasColumnType("citext");

            builder.HasIndex(x => x.Name)
                   .IsUnique();

            builder.HasOne(x => x.SecondaryItem)
                   .WithMany(y => y.PrimaryItems)
                   .HasForeignKey(x => x.SecondaryItemId);
            builder.ToTable("PrimaryItems");
        }
    }
}
