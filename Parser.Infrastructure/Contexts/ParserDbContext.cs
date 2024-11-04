using Microsoft.EntityFrameworkCore;
using Parser.Domain.Entities;

namespace Parser.Infrastructure.Contexts;

/// <summary>
/// Контекст базы данных.
/// </summary>
public class ParserDbContext : DbContext
{
    /// <summary>
    /// Шаблоны.
    /// </summary>
    public DbSet<Template> Templates { get; set; }
    
    /// <summary>
    /// Атрибуты шаблонов.
    /// </summary>
    public DbSet<TemplateAttribute> TemplateAttributes { get; set; }
    
    /// <summary>
    /// Лист данных
    /// </summary>
    public DbSet<DataSheet> DataSheets { get; set; }
    
    /// <summary>
    /// Значения атрибутов шаблонов.
    /// </summary>
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