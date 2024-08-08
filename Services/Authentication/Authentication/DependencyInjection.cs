using Authentication.Data;
using Authentication.Services;
using Authentication.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Authentication
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services,
          IConfiguration config, IWebHostEnvironment environment)
        {

            var identityUrl = config.GetValue<string>("Identity:IdentityUrl");
            var apiName = config.GetValue<string>("Identity:ApiName");
            var tokenUrl = config.GetValue<string>("Identity:TokenUrl");
            var apiSecret = config.GetValue<string>("Identity:ApiSecret");
            services.Configure<AppSettings>(config.GetSection("AppSettings"));

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    builder.WithOrigins("https://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                {
                    options.Authority = identityUrl;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters.ValidateAudience = false;
                });

            services.Configure<AppSettings>(config.GetSection("AppSettings"));

            ServicesConfiguration.ConfigureCORS(services, config);

            ServicesConfiguration.ConfigureMvcAndRazor(services, environment);

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(config["ConnectionStrings:SqlServer"]);
            });

            ServicesConfiguration.ConfigureIdentity(services, config, environment);

            var tokenLifeSpan = config.GetValue<int>("DataProtectionTokenLifeSpanHours");

            services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromHours(tokenLifeSpan));

            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }
    }
}

