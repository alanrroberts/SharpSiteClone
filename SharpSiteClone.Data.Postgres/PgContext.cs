﻿using Microsoft.EntityFrameworkCore;
namespace SharpSiteClone.Data.Postgres;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class PgContext : DbContext
{
    public PgContext(DbContextOptions<PgContext> options) : base(options) { }
    
    public DbSet<PgPost> Posts => Set<PgPost>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<PgPost>()
            .Property(e => e.Published)
            .HasConversion(new DateTimeOffsetConverter());
    }
}

public class DateTimeOffsetConverter : ValueConverter<DateTimeOffset, DateTimeOffset>
{
    public DateTimeOffsetConverter() : base(
        v => v.UtcDateTime,
        v => v)
    {
    }
}