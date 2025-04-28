using Bookify.Api.Extensions;
using Bookify.Api.OpenApi;
using Bookify.Application;
using Bookify.Application.Abstractions.Data;
using Bookify.Infrastructure;
using Dapper;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((context, configuration) => { configuration.ReadFrom.Configuration(context.Configuration); });

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var descriptions = app.DescribeApiVersions();
        foreach (var description in descriptions)
        {
            var url = $"/swagger/{description.GroupName}/swagger.json";
            var name = description.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
        }
    });

    app.ApplyMigrations();
    app.SeedData();
    // app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseCustomExceptionHandler();
app.UseRequestContextLogging();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();