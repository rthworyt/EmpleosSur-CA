using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using EmpleosSur.Infraestructure.Data;

public class EmpleosSurDBContextFactory : IDesignTimeDbContextFactory<EmpleosSurDBContext>
{
    public EmpleosSurDBContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EmpleosSurDBContext>();
        optionsBuilder.UseSqlServer("Server=DESKTOP-4C2LPF7\\SQLEXPRESS;Database=EmpleosSurDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");

        return new EmpleosSurDBContext(optionsBuilder.Options);
    }
}
