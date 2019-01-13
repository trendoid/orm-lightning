using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using EFData;

namespace EFData
{
    public class LightningContextFactory : IDesignTimeDbContextFactory<LightningContext>
    {
        public LightningContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LightningContext>();
            optionsBuilder.UseSqlServer("Server=(local);Database=LightingDatabase;Trusted_Connection=True;");

            return new LightningContext(optionsBuilder.Options);
        }
    }
}