using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace darts
{
    internal class ContextDB : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=DartsDB.db");
        }
    }
}
