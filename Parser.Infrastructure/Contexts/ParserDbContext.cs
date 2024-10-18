using Microsoft.EntityFrameworkCore;
using Parser.Domain.Entities;

namespace Parser.Infrastructure.Contexts;

public class ParserDbContext : DbContext
{
    public DbSet<Template> Templates { get; set; }
    
    public DbSet<TemplateAttribute> TemplateAttributes { get; set; }
    
    public DbSet<DataSheet> DataSheets { get; set; }
    
    public DbSet<DataSheetValue> DataSheetValues { get; set; }

    public ParserDbContext(DbContextOptions<ParserDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ParserDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}