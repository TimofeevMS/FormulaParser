using System.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Parser.Domain.Entities;
using Parser.Domain.Interfaces;
using Parser.Infrastructure.Contexts;

namespace Parser.Infrastructure.Repositories;

public class TemplateDapperRepository : DapperRepository<Template>, ITemplateDapperRepository
{
    private readonly IMapper _mapper;
    private readonly IDbConnection _dbConnection;

    public TemplateDapperRepository(IDbConnection dbConnection, IMapper mapper) : base(dbConnection)
    {
        _dbConnection = dbConnection;
        _mapper = mapper;
    }

    public override async Task<Template?> GetByIdAsync(Guid templateId, CancellationToken cancellationToken)
    {
        var query = """
                            SELECT t."Id", t."Name",
                                   a."Id", a."TemplateId", a."Name", a."Description", a."Formula", a."Type"
                            FROM "Templates" t
                            LEFT JOIN "TemplateAttributes" a ON t."Id" = a."TemplateId"
                            WHERE t."Id" = @TemplateId
                    """;

        var templateDictionary = new Dictionary<Guid, Template>();

        var templates = await _dbConnection.QueryAsync<Template, TemplateAttribute, Template>(
                         query,
                         (template, attribute) =>
                         {
                             if (!templateDictionary.TryGetValue(template.Id, out var currentTemplate))
                             {
                                 currentTemplate = template;
                                 templateDictionary.Add(currentTemplate.Id, currentTemplate);
                             }

                             if (attribute != null)
                             {
                                 currentTemplate.Attributes ??= new List<TemplateAttribute>();
                                 currentTemplate.Attributes.Add(attribute);
                             }
                             return currentTemplate;
                         },
                         new { TemplateId = templateId },
                         splitOn: "Id");

        return templates.FirstOrDefault();
    }

    public async Task<IEnumerable<TDto>> GetForMenuAsync<TDto>(CancellationToken cancellationToken)
    {
        var query = "SELECT * FROM \"Templates\"";

        var templates = await _dbConnection.QueryAsync<Template>(query);

        var dtos = _mapper.Map<List<TDto>>(templates);

        return dtos;
    }
}