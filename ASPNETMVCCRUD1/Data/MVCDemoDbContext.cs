using ASPNETMVCCRUD1.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ASPNETMVCCRUD1.Data
{
    public class MVCDemoDbContext : DbContext
    {
        public MVCDemoDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Employee> Employees { get; set; }

    }
}
