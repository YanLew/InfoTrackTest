using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace InfoTrackTest.Repositories.Context
{
    public class InfoTrackTestContextFactory : IDesignTimeDbContextFactory<InfoTrackTestContext>
    {
        public InfoTrackTestContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json")
             .Build();

            var builder = new DbContextOptionsBuilder<InfoTrackTestContext>();
            builder.UseSqlServer(configuration.GetConnectionString("InfoTrackTestDb"));

            return new InfoTrackTestContext(builder.Options);
        }
    }
}
