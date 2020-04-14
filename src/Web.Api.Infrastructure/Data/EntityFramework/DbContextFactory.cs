using Microsoft.EntityFrameworkCore;
 

namespace Web.Api.Infrastructure.Data.EntityFramework
{
    public class DbContextFactory : DesignTimeDbContextFactoryBase<AppDbContext>
    {
        protected override AppDbContext CreateNewInstance(DbContextOptions<AppDbContext> options)
        {
            return new AppDbContext(options);
        }
    }
}
