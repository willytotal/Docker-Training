using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DockerTraining.Databases;

internal class DisplayConfiguration : IEntityTypeConfiguration<DisplayEntity>
{
    public void Configure(EntityTypeBuilder<DisplayEntity> builder)
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        builder.ToTable("Display");
        builder.HasKey(e => e.DisplayId);

        builder.Property(e => e.Description).HasMaxLength(128).IsRequired();
        
        builder.Property(e => e.Created).HasColumnType("datetime").HasDefaultValueSql("getutcdate()")
               .IsRequired();

        builder.HasData(DataSeeder.DisplaySeeds());
    }
}
