using CarStore.API.Converters;
using CarStore.API.Filters;
using CarStore.API.Token;
using CarStore.Application;
using CarStore.Domain.Security.Tokens;
using CarStore.Infrastructure;
using CarStore.Infrastructure.Migrations;
using CarStore.Infrastrucutre.Redis;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder
    .Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new StringConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Description =
                @"JWT Authorization header using the Bearer scheme.
                        Enter 'Bearer' [space] and then your token in the text input below.
                        Example:  'Bearer 123uSabcdef'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
        }
    );

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer",
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            },
        }
    );
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "CarStore - API",
        Version = "v1",
        Description = "Documentação da API da CarStore.",
    });

    c.DocumentFilter<HealthCheckDocumentFilter>();
});

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddApplications(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddInfraRedisServices(builder.Configuration);
builder.Services.AddScoped<ITokenProvider, HttpContextTokenValue>();

if (!builder.Configuration.IsUnitTestEnviroment())
{
    builder.Services.AddHealthChecks()
     .AddMySql(
         builder.Configuration.GetConnectionString("ConnectionMySql")!,
         name: "mysql",
         failureStatus: HealthStatus.Unhealthy)
     .AddRedis(
         builder.Configuration.GetConnectionString("ConnectionRedis")!,
         name: "redis",
         failureStatus: HealthStatus.Unhealthy);
}

builder.Services.AddHealthChecksUI(setup =>
{
    setup.AddHealthCheckEndpoint("API Health", "/health");
}).AddInMemoryStorage();


builder.Services.AddHttpContextAccessor();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();


app.UseSwaggerUI(c =>
{
    c.ConfigObject.AdditionalItems["persistAuthorization"] = true;
});


if (!builder.Configuration.IsUnitTestEnviroment())
{

    app.MapHealthChecks("/health", new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
        ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    }
    });

    app.MapHealthChecksUI(options =>
    {
        options.UIPath = "/health-ui";
    });
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

MigrateDataBase();

await app.RunAsync();

void MigrateDataBase()
{
    if (builder.Configuration.IsUnitTestEnviroment())
    {
        return;
    }
    var connectionString = builder.Configuration.ConnectionString();
    var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

    DatabaseMigration.Migrate(connectionString!, serviceScope.ServiceProvider);
}

public partial class Program { }