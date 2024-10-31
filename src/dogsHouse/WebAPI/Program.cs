using Application;
using Infrastructure;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using NArchitecture.Core.CrossCuttingConcerns.Exception.WebApi.Extensions;
using NArchitecture.Core.CrossCuttingConcerns.Logging.Configurations;
using NArchitecture.Core.Localization.WebApi;
using NArchitecture.Core.Persistence.Dynamic;
using NArchitecture.Core.Persistence.WebApi;
using Persistence;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Threading.RateLimiting;
using WebAPI;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices(
    builder.Configuration.GetSection("SeriLogConfigurations:FileLogConfiguration")
        .Get<FileLogConfiguration>()
        ?? throw new InvalidOperationException("FileLogConfiguration section cannot found in configuration.")
);
builder.Services.AddInfrastructureServices();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(opt =>
    opt.AddDefaultPolicy(p =>
    {
        p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    })
);

builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
        RateLimitPartition.GetFixedWindowLimiter("GlobalRateLimit", _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 10,
            Window = TimeSpan.FromSeconds(1),
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = 5
        }));
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

    // Set example data for DynamicQuery
    c.MapType<DynamicQuery>(() => new OpenApiSchema
    {
        Example = new OpenApiObject
        {
            ["sort"] = new OpenApiArray
            {
                new OpenApiObject
                {
                    ["field"] = new OpenApiString("weight"),
                    ["dir"] = new OpenApiString("asc")
                }
            },
            ["filter"] = new OpenApiObject
            {
                ["field"] = new OpenApiString("name"),
                ["operator"] = new OpenApiString("eq"),
                ["value"] = new OpenApiString("Neo")
            }
        }
    });
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.DocExpansion(DocExpansion.None);
    });
}

if (app.Environment.IsProduction())
    app.ConfigureCustomExceptionMiddleware();

app.UseDbMigrationApplier();

app.MapControllers();

const string webApiConfigurationSection = "WebAPIConfiguration";
WebApiConfiguration webApiConfiguration =
    app.Configuration.GetSection(webApiConfigurationSection).Get<WebApiConfiguration>()
    ?? throw new InvalidOperationException($"\"{webApiConfigurationSection}\" section cannot found in configuration.");
app.UseCors(opt => opt.WithOrigins(webApiConfiguration.AllowedOrigins).AllowAnyHeader().AllowAnyMethod().AllowCredentials());

app.UseResponseLocalization();

app.Run();
