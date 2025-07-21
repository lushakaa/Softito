using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using YuvamHazir.Infrastructure.Context;

namespace YuvamHazir.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<YuvamHazirDbContext>
    {
        public YuvamHazirDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<YuvamHazirDbContext>();
            // Buraya kendi bağlantı stringinizi yazın

           optionsBuilder.UseSqlServer("Server=localhost;Database=YuvamHazirDb3;User Id=SA;Password=reallyStrongPwd123;TrustServerCertificate=True;");
           // optionsBuilder.UseSqlServer("Server=HARUN;Database=YuvamHazirDb;Trusted_Connection=True;TrustServerCertificate=True;");



           // optionsBuilder.UseSqlServer("Server=localhost;Database=YuvamHazirDb2;User Id=SA;Password=siFdegisiyom1.;TrustServerCertificate=True;");

            return new YuvamHazirDbContext(optionsBuilder.Options);
        }
    }
}
