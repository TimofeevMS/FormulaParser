using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Parser.Domain.Entities;
using Parser.Domain.Interfaces;
using Parser.Infrastructure.Contexts;

namespace Parser.Infrastructure.Repositories;

public class TemplateRepository : RepositoryGeneric<Template>, ITemplateRepository
{
    private readonly IConfigurationProvider _configuration;
    private readonly ParserDbContext _context;

    public TemplateRepository(ParserDbContext context, IConfigurationProvider configuration) : base(context)
    {
        _context = context;
        _configuration = configuration;
    }

    public override async Task<Template?> GetByIdAsync(Guid templateId, CancellationToken cancellationToken)
    {
        return await _context.Templates
                             .Include(t => t.Attributes)
                             .FirstOrDefaultAsync(t => t.Id == templateId, cancellationToken);
    }

    public async Task<IEnumerable<TDto>> GetForMenuAsync<TDto>(CancellationToken cancellationToken)
    {
        return await _context.Templates
                             .ProjectTo<TDto>(_configuration)
                             .ToListAsync(cancellationToken);
    }
}