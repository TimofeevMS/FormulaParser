using Parser.Application;
using Parser.Filters;
using Parser.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services
       .AddSerilog()
       .AddCors(options =>
                {
                    options.AddPolicy("AllowAll", policyBuilder =>
                                                  {
                                                      policyBuilder.AllowAnyOrigin()
                                                                   .AllowAnyMethod()
                                                                   .AllowAnyHeader();
                                                  });
                })
       .AddEndpointsApiExplorer()
       .AddSwaggerGen()
       .AddInfrastructure(builder.Configuration)
       .AddApplication()
       .AddControllers(options =>
                       {
                           options.Filters.Add<ResultFilter>(); // Добавляем глобально
                       });

Log.Logger = new LoggerConfiguration()
             .WriteTo.Console()
             .CreateLogger();

var app = builder.Build();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseReDoc();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
app.Run();