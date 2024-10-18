using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Parser.Domain.Entities;
using Parser.Domain.Interfaces;
using Parser.Infrastructure.Contexts;

namespace Parser.Infrastructure.Repositories;

public class DataSheetRepository : IDataSheetRepository
{
    private readonly IConfigurationProvider _configuration;
    private readonly ParserDbContext _context;

    public DataSheetRepository(ParserDbContext context, IConfigurationProvider configuration)
    {
        _context = context;
        _configuration = configuration;
    }
    
    public async Task AddDataSheet(DataSheet dataSheet, CancellationToken cancellationToken)
    {
        await _context.DataSheets.AddAsync(dataSheet, cancellationToken);
        
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<DataSheet?> GetDataSheet(Guid requestId, CancellationToken cancellationToken)
    {
        return await _context.DataSheets
                             .Include(d => d.Template).ThenInclude(v => v.Attributes)
                             .Include(d => d.Values).ThenInclude(v => v.TemplateAttribute)
                             .FirstOrDefaultAsync(d => d.Id == requestId, cancellationToken);
    }

    public async Task<IEnumerable<TDto>> GetForMenuTemplates<TDto>(CancellationToken cancellationToken)
    {
        return await _context.DataSheets
                             .ProjectTo<TDto>(_configuration)
                             .ToListAsync(cancellationToken);
    }

    public async Task UpdateDataSheet(DataSheet dataSheet, CancellationToken cancellationToken)
    {
        _context.DataSheets.Update(dataSheet);
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}