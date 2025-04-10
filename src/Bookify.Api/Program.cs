using Bookify.Api.Extensions;
using Bookify.Application;
using Bookify.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.ApplyMigrations();
    app.SeedData();
    // app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCustomExceptionHandler();

app.MapControllers();

app.Run();