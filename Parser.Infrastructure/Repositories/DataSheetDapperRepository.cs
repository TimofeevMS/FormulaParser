using System.Data;
using AutoMapper;
using Dapper;
using Parser.Domain.Entities;
using Parser.Domain.Interfaces;

namespace Parser.Infrastructure.Repositories;

public class DataSheetDapperRepository : DapperRepository<DataSheet>, IDataSheetDapperRepository
{
    private readonly IMapper _mapper;
    private readonly IDbConnection _dbConnection;

    public DataSheetDapperRepository(IDbConnection dbConnection, IMapper mapper) : base(dbConnection)
    {
        _dbConnection = dbConnection;
        _mapper = mapper;
    }

    public override async Task<DataSheet?> GetByIdAsync(Guid dataSheetId, CancellationToken cancellationToken)
    {
        var query = """
                            SELECT ds."Id",
                                   v."Id", v."DataSheetId", v."TemplateAttributeId",
                                   ta."Id", ta."Name"
                            FROM "DataSheets" ds
                            LEFT JOIN "DataSheetValues" v ON ds."Id" = v."DataSheetId"
                            LEFT JOIN "TemplateAttributes" ta ON v."TemplateAttributeId" = ta."Id"
                            WHERE ds.Id = @DataSheetId
                    """;

        var dataSheetDictionary = new Dictionary<Guid, DataSheet>();

        var dataSheets = await _dbConnection.QueryAsync<DataSheet, DataSheetValue, TemplateAttribute, DataSheet>(
                          query,
                          (dataSheet, value, templateAttribute) =>
                          {
                              if (!dataSheetDictionary.TryGetValue(dataSheet.Id, out var currentDataSheet))
                              {
                                  currentDataSheet = dataSheet;
                                  dataSheetDictionary.Add(currentDataSheet.Id, currentDataSheet);
                              }

                              if (value != null)
                              {
                                  if (templateAttribute != null)
                                  {
                                      value.TemplateAttribute = templateAttribute;
                                  }
                                  currentDataSheet.Values.Add(value);
                              }
                              return currentDataSheet;
                          },
                          new { DataSheetId = dataSheetId },
                          splitOn: "Id,TemplateAttributeId");

        return dataSheets.FirstOrDefault();
    }

    public async Task<IEnumerable<TDto>> GetForMenuAsync<TDto>(CancellationToken cancellationToken)
    {
        var query = "SELECT * FROM \"DataSheets\"";

        var dataSheets = await _dbConnection.QueryAsync<DataSheet>(query);

        var dtos = _mapper.Map<List<TDto>>(dataSheets);

        return dtos;
    }
}