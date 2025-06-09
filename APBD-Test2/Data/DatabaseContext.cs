using Microsoft.EntityFrameworkCore;

namespace APBD_Test2.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // model builder for each table
        modelBuilder.Entity<Student>(e =>
        {

        });

        // Seed Data seed each table
        modelBuilder.Entity(/*table name to seed*/).HasData(
        );
    }
}