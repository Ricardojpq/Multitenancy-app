using Asp.Versioning;
using Destructurama;
using LookupTables.Api.Extensions;
using LookupTables.Api.Settings;
using LookupTables.Database;
using LookupTables.Database.Files;
using LookupTables.Database.Persistence;
using LookupTables.Database.Persistence.SeedData;
using LookupTables.Database.Services.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Net.Mime;
using System.Reflection;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
string versionNumber = "v" + Assembly.GetEntryAssembly()!.GetCustomAttribute<AssemblyInformationalVersionAttribute>()!
.InformationalVersion;

var mongoDBSettings = builder.Configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();

AppSettings appSettings;
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
appSettings = appSettingsSection.Get<AppSettings>()!;

MongoDBSettings mongoDbSettings;
var mongoDBSettingsSection = builder.Configuration.GetSection("MongoDBSettings");
mongoDbSettings = mongoDBSettingsSection.Get<MongoDBSettings>()!;

MongoDBSettings mongoTenantDbSettings;
var mongoTenantDBSettingsSection = builder.Configuration.GetSection("MongoTenantDBSettings");
mongoTenantDbSettings = mongoTenantDBSettingsSection.Get<MongoDBSettings>()!;



builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options => options.UseCamelCasing(true)); ;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
    config.ApiVersionReader = new QueryStringApiVersionReader("api-version");
    SwaggerConfiguration.UseQueryStringApiVersion();
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = appSettings.MicroserviceName,
            Version = versionNumber,
            Description = "An API to perform The Starter Ops",
            TermsOfService = new Uri(appSettings.TermsOfService),
            Contact = new OpenApiContact
            {
                Name = "Portfolio",
                Email = appSettings.SharedMail,
                Url = new Uri(appSettings.PortfolioUrl)
            },
            License = new OpenApiLicense { Name = "Portfolio", Url = new Uri(appSettings.PortfolioUrl) }
        });

    options.AddSecurityDefinition("Authorization",
        new OpenApiSecurityScheme
        {
            Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
        });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});



builder.Services
    .AddHealthChecks()
    .AddMongoDb(mongodbConnectionString: $"{mongoDbSettings.Server}/{mongoDbSettings.Database}",
        name: "MongoDb",
        failureStatus: HealthStatus.Unhealthy);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.Authority = appSettings.IdentityServerUrl;
        options.TokenValidationParameters.ValidateAudience = false;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
    });

if (appSettings.RequiredAuthorization)
{
    builder.Services.AddAuthorization();
}

builder.Services.AddCors(options =>
{
    options.AddPolicy("default", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

Log.Logger = new LoggerConfiguration().ReadFrom
    .Configuration(builder.Configuration)
    .MinimumLevel.Debug()
    .Destructure.JsonNetTypes()
    .WriteTo.Console()
    .CreateLogger();
Log.Debug("Successfully Setup Serilog");

builder.Services.Configure<MongoDBSettings>(mongoDBSettingsSection);
builder.Services.Configure<AppSettings>(appSettingsSection);
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();  
builder.Services.AddSingleton<IMyCache, MyMemoryCache>();

builder.Services.AddDbContext<LookupTableDbContext>(options =>
    options.UseMongoDB(mongoDBSettings.Server ?? "", mongoDBSettings.Database ?? "")
);

builder.Services.AddDbContext<TenantDbContext>(options =>
    options.UseMongoDB(mongoTenantDbSettings.Server ?? "", mongoTenantDbSettings.Database ?? "")
);

builder.Services.AddMultiTenancy()
    .WithResolutionStrategy<TenantService>()
    .WithStore<TenantStoreService>();

var app = builder.Build();

app.UseRouting();
app.UseCors("default");
app.UseAuthentication();
app.UseAuthorization();


app.MapHealthChecks("/healthcheck");
app.MapHealthChecks("/healthcheck-details",
    new HealthCheckOptions
    {
        ResponseWriter = async (context, report) =>
        {
            var result = JsonSerializer.Serialize(
                new
                {
                    status = report.Status.ToString(),
                    monitors = report.Entries.Select(e => new { key = e.Key, value = Enum.GetName(typeof(HealthStatus), e.Value.Status) })
                });
            context.Response.ContentType = MediaTypeNames.Application.Json;
            await context.Response.WriteAsync(result);
        }
    }
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DisplayRequestDuration();
        options.InjectStylesheet(appSettings.SwaggerStyle);
    });
}
else
{
    app.UseHttpsRedirection();
}

app.MapControllers();
app.UseMultiTenancy();

#region InicializacionDB
using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>()
           .CreateScope())
{
    var context = scope.ServiceProvider.GetService<LookupTableDbContext>()!;
    var tenantContext = scope.ServiceProvider.GetService<TenantDbContext>()!;
    if (appSettings.RunDbInitializer)
    {
        DbInitializer.Initialize(context, tenantContext, builder.Configuration).GetAwaiter().GetResult();
    }
}
#endregion

app.Run();
