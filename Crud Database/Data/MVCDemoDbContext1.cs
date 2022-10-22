using Crud_Database.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Crud_Database.Data
{
    public class MVCDemoDbContext1 : DbContext
    {
        public MVCDemoDbContext1(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Student> Students { get; set; }
    }
}
