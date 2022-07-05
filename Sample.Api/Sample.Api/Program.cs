using System.Reflection;
using Microsoft.OpenApi.Models;
using Sample.Api.Middlewares;
using Sample.Api.SwaggerExamples.Requests;
using Sample.Dal.Repositories;
using Sample.Domain.Interfaces.Repositories;
using Sample.Domain.Interfaces.Services;
using Sample.Domain.Services;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My sample API", Version = "v1" });
    c.ExampleFilters();

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<CreateValueRequestExample>();
builder.Services.AddScoped<IValueService, ValueService>();
builder.Services.AddScoped<IFakeRepository, FakeRepository>();

var app = builder.Build();

app.UseMiddleware<LoggingMiddleware>();

app.UseMiddleware<ErrorLoggingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
