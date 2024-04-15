using darts.db.Entities;
using Microsoft.EntityFrameworkCore;

namespace darts.db
{
    public class ContextDB : DbContext
    {
        public DbSet<UserEntity> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Darts.db");
        }
    }
}
