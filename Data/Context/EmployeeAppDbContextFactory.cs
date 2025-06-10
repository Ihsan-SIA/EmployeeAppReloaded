using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Data.Context;

namespace Data;

public class EmployeeAppDbContextFactory : IDesignTimeDbContextFactory<EmployeeAppDbContext>
{
    public EmployeeAppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EmployeeAppDbContext>();
        var connectionString = "server=localhost;port=3306;database=employeedbreloaded;user=root;password=1hsan@SQL";
        optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36)));

        return new EmployeeAppDbContext(optionsBuilder.Options);
    }
}
