using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Parser.Domain.Interfaces;
using Parser.Infrastructure.Contexts;
using Parser.Infrastructure.Contexts.Interceptors;
using Parser.Infrastructure.Repositories;

namespace Parser.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<ParserDbContext>(options =>
                                               {
                                                   options.UseNpgsql(connectionString);
                                                   options.AddInterceptors(new ModifyEntryDateInterceptor());
                                               });
        services.AddTransient<IDbConnection>(sp => new NpgsqlConnection(connectionString));
        
        services.AddScoped<IRepository, TemplateEfCoreRepository>();
        services.AddScoped<IRepository, TemplateDapperRepository>();
        services.AddScoped<IRepository, DataSheetEfCoreRepository>();
        services.AddScoped<IRepository, DataSheetDapperRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}