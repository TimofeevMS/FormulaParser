﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Parser.Domain.Interfaces;
using Parser.Infrastructure.Contexts;
using Parser.Infrastructure.Contexts.Interceptors;
using Parser.Infrastructure.Repositories;

namespace Parser.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ParserDbContext>(options =>
                                               {
                                                   options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
                                                   options.AddInterceptors(new ModifyEntryDateInterceptor());
                                               });
        
        services.AddScoped<IRepository, TemplateRepository>();
        services.AddScoped<IRepository, DataSheetRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}