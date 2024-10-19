using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Parser.Application.Interfaces;
using Parser.Application.Services;
using Parser.Application.Services.CustomFunctions;
using Parser.Application.Services.VariableStrategy;

namespace Parser.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(DependencyInjection).Assembly);
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        
        services.AddScoped<IFormulaService, FormulaService>();
        services.AddScoped<ICustomFunctionHandler, MRoundHandler>();
        services.AddScoped<ICustomFunctionHandler, AndHandler>();

        services.AddScoped<IVariableLoaderFactory, VariableLoaderFactory>();
        services.AddScoped<IVariableLoaderStrategy, ValueLoaderStrategy>();
        services.AddScoped<IVariableLoaderStrategy, FormulaLoaderStrategy>();
        
        return services;
    }
}