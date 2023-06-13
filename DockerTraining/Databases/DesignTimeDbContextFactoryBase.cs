using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DockerTraining.Databases;

internal class MyDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
{
    public MyDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
        optionsBuilder.UseSqlServer("Server=tcp:localhost,1433;Initial Catalog=DockerTraining;Persist Security Info=False;User ID=sa;Password=Letmein1;MultipleActiveResultSets=False;Connection Timeout=30;TrustServerCertificate=True");

        return new MyDbContext(optionsBuilder.Options);
    }
}
