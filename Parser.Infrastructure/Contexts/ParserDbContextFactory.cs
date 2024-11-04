using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Parser.Infrastructure.Contexts;

/// <summary>
/// Фабрика контекста базы данных.
/// </summary>
public class ParserDbContextFactory : IDesignTimeDbContextFactory<ParserDbContext>
{
    public ParserDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ParserDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=FormulaParser;Username=postgres;Password=123");

        return new ParserDbContext(optionsBuilder.Options);
    }
}