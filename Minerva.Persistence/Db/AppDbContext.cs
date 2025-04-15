using Microsoft.EntityFrameworkCore;
using Minerva.Persistence.Entities.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Persistence.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Relation> Relations => Set<Relation>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Relation>().ToTable("Relations");
        }
    }
}
