using System.Reflection;
using Microsoft.OpenApi.Models;
using Sample.Api.Middlewares;
using Sample.Api.SwaggerExamples.Requests;
using Sample.Dal.Repositories;
using Sample.Domain.Interfaces.Repositories;
using Sample.Domain.Interfaces.Services;
using Sample.Domain.Services;
using Swashbuckle.AspNetCore.Filters;

namespace Sample.Api;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);
        builder.Services.AddAuthentication()
            .AddJwtBearer(options =>
            {
                options.Authority = builder.Configuration["IdentityServer"];
                options.TokenValidationParameters.ValidateAudience = false;
            });
        builder.Services.AddAuthorizationBuilder()
            .AddPolicy("SampleApiPolicy", policy =>
            {
                policy.RequireClaim("scope", "Sample.Api");
            });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "My sample API", Version = "v1" });
            options.ExampleFilters();

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        builder.Services.AddSwaggerExamplesFromAssemblyOf<CreateValueRequestExample>();
        builder.Services.AddScoped<IValueService, ValueService>();
        builder.Services.AddScoped<IFakeRepository, FakeRepository>();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseMiddleware<LoggingMiddleware>();
        app.UseMiddleware<ErrorLoggingMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}