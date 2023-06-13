using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DockerTraining.Databases;

internal class MyDbContext : DbContext
{
    public DbSet<DisplayEntity> Displays { get; set; } = default!;

    public MyDbContext([NotNull] DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder == null)
            throw new ArgumentNullException(nameof(optionsBuilder));

        optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddDebug()));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (modelBuilder == null)
            throw new ArgumentNullException(nameof(modelBuilder));

        modelBuilder.ApplyConfiguration(new DisplayConfiguration());
    }
}
