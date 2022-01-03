using CustomFileProviders.AppData.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomFileProviders.AppData.Configurations
{
    public class ViewEntityConfiguration : IEntityTypeConfiguration<ViewEntity>
    {
        public void Configure(EntityTypeBuilder<ViewEntity> builder)
        {
            builder.Property(p => p.Location).IsRequired();
            builder.Property(p => p.Content).IsRequired();
            builder.Property(p => p.LastModified).IsRequired();
        }
    }
}
