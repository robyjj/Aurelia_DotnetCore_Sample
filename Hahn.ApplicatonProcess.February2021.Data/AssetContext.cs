using Hahn.ApplicatonProcess.February2021.Models;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicatonProcess.February2021.Data
{
    public class AssetContext: DbContext
    {
        public AssetContext(DbContextOptions<AssetContext> options) : base(options)
        {

        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Specify the relation between model classes. For now we have only one.

            modelBuilder.Seed();
        }

        public DbSet<Asset> Assets { get; set; }

    }
}
