using Microsoft.EntityFrameworkCore;
namespace SharpSiteClone.Data.Postgres;

public class PgContext : DbContext
{
    public PgContext(DbContextOptions<PgContext> options) : base(options) { }
    
    public DbSet<PgPost> Posts => Set<PgPost>();
}