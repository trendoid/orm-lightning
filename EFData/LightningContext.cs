using EFData.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EFData
{
    public class LightningContext : DbContext
    {
        public LightningContext(DbContextOptions<LightningContext> options)
            : base(options)
        { }

        public DbSet<Heckler> Hecklers { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }    
}
