using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Parser.Domain.Entities;
using Parser.Domain.Interfaces;
using Parser.Infrastructure.Contexts;

namespace Parser.Infrastructure.Repositories;

public class DataSheetRepository : RepositoryGeneric<DataSheet>, IDataSheetRepository
{
    private readonly IConfigurationProvider _configuration;
    private readonly ParserDbContext _context;

    public DataSheetRepository(ParserDbContext context, IConfigurationProvider configuration) : base(context)
    {
        _context = context;
        _configuration = configuration;
    }

    public override async Task<DataSheet?> GetByIdAsync(Guid requestId, CancellationToken cancellationToken)
    {
        return await _context.DataSheets
                             .Include(d => d.Values).ThenInclude(v => v.TemplateAttribute)
                             .FirstOrDefaultAsync(d => d.Id == requestId, cancellationToken);
    }

    public async Task<IEnumerable<TDto>> GetForMenuAsync<TDto>(CancellationToken cancellationToken)
    {
        return await _context.DataSheets
                             .ProjectTo<TDto>(_configuration)
                             .ToListAsync(cancellationToken);
    }
}