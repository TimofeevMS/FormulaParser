using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Parser.Domain.Entities;
using Parser.Domain.Interfaces;
using Parser.Infrastructure.Contexts;

namespace Parser.Infrastructure.Repositories;

public class TemplateRepository : ITemplateRepository
{
    private readonly IConfigurationProvider _configuration;
    private readonly ParserDbContext _context;

    public TemplateRepository(ParserDbContext context, IConfigurationProvider configuration)
    {
        _context = context;
        _configuration = configuration;
    }
    
    public async Task AddTemplate(Template template, CancellationToken cancellationToken)
    {
        await _context.Templates.AddAsync(template, cancellationToken);
        
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Template?> GetTemplate(Guid requestId, CancellationToken cancellationToken)
    {
        return await _context.Templates
                             .Include(t => t.Attributes)
                             .FirstOrDefaultAsync(t => t.Id == requestId, cancellationToken);
    }

    public async Task UpdateTemplate(Template template, CancellationToken cancellationToken = default)
    {
        _context.Templates.Update(template);
        
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Template>> GetAllTemplates(CancellationToken cancellationToken = default)
    {
        return await _context.Templates
                             .AsNoTracking()
                             .Include(t => t.Attributes)
                             .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TDto>> GetForMenuTemplates<TDto>(CancellationToken cancellationToken)
    {
        return await _context.Templates
                             .ProjectTo<TDto>(_configuration)
                             .ToListAsync(cancellationToken);
    }
}