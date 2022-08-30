using Microsoft.EntityFrameworkCore;
using POC.DataAccess.Models;

namespace POC.DataAccess
{
    public class ExampleDbContext : DbContext
    {
        public ExampleDbContext(DbContextOptions<ExampleDbContext> options)
         : base(options)
        {
            if (options is null) throw new ArgumentNullException(nameof(options));
        }

        public DbSet<ExampleTable> ExampleTables { get; set; }
    }
}
