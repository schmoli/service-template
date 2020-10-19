using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schmoli.ServiceTemplate.Models;

namespace Schmoli.ServiceTemplate.Data.Configurations
{
    internal class SecondaryItemEntityTypeConfiguration : IEntityTypeConfiguration<SecondaryItem>
    {
        public void Configure(EntityTypeBuilder<SecondaryItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .UseIdentityColumn();

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(32)
                   .HasColumnType("citext");

            builder.HasMany(x => x.PrimaryItems).WithOne(y => y.SecondaryItem).OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.Name)
                   .IsUnique();

            builder.ToTable("SecondaryItems");
        }
    }
}
