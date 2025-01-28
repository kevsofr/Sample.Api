using Sample.Api.Middlewares;
using Sample.Dal.Repositories;
using Sample.Domain.Interfaces.Repositories;
using Sample.Domain.Interfaces.Services;
using Sample.Domain.Services;
using Scalar.AspNetCore;

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
        builder.Services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
        });
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
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}