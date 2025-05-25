using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace YouthUnionManagement.Data
{
    public static class DbContextFactory
    {
        public static ApplicationDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=YouthUnionManagement;Trusted_Connection=True;TrustServerCertificate=True;");
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}