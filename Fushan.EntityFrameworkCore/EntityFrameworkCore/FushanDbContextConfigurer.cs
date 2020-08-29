using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Fushan.EntityFrameworkCore
{
    public static class FushanDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<FushanContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<FushanContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
