using Parser.Application;
using Parser.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
       .AddCors(options =>
                {
                    options.AddPolicy("AllowAll", builder =>
                                                  {
                                                      builder.AllowAnyOrigin()
                                                             .AllowAnyMethod()
                                                             .AllowAnyHeader();
                                                  });
                })
       .AddEndpointsApiExplorer()
       .AddSwaggerGen()
       .AddInfrastructure(builder.Configuration)
       .AddApplication()
       .AddControllers();

var app = builder.Build();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
app.Run();