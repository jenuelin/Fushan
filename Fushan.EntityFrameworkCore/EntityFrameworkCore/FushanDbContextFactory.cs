using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Fushan.Configuration;
using Fushan.Web;

namespace Fushan.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class FushanDbContextFactory : IDesignTimeDbContextFactory<FushanDbContext>
    {
        public FushanDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<FushanDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            FushanDbContextConfigurer.Configure(builder, configuration.GetConnectionString(FushanConsts.ConnectionStringName));

            return new FushanDbContext(builder.Options);
        }
    }
}
