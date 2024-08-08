using Authentication.Configuration;
using Authentication.Data;
using Authentication.Entities;
using Authentication.Services;
using Authentication.Services.Implementation;
using Authentication.Validators;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace Authentication
{
    public class ServicesConfiguration
    {
        internal const string CorsRuleName = "CorsRule";

        internal static void ConfigureCORS(IServiceCollection services, IConfiguration config)
        {
            var allowedOrigins = config.GetSection("Identity:AllowedCorsOrigins").Get<string[]>();
            services.AddCors(options =>
            {
                options.AddPolicy(CorsRuleName,
                    builder =>
                    {
                        builder.WithOrigins(allowedOrigins)
                            .SetIsOriginAllowedToAllowWildcardSubdomains()
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            });
        }

        internal static void ConfigureIdentity(
           IServiceCollection services, IConfiguration config, IWebHostEnvironment env)

        {

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Tokens.PasswordResetTokenProvider = "ResetPassword";
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddUserManager<UserManager>()
                .AddRoleManager<RoleManager<ApplicationRole>>()
                .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, int,
                    IdentityUserClaim<int>, ApplicationUserRole, IdentityUserLogin<int>, IdentityUserToken<int>,
                    IdentityRoleClaim<int>>>()
                .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, int, ApplicationUserRole,
                    IdentityRoleClaim<int>>>();

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromHours(24));


            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(config.GetValue<int>("Identity:Lockout:TimeSpanMinutes"));
                options.Lockout.MaxFailedAccessAttempts = config.GetValue<int>("Identity:Lockout:MaxFailedAttempts");
                options.Password.RequireDigit = config.GetValue<bool>("Identity:Password:RequireDigit");
                options.Password.RequiredLength = config.GetValue<int>("Identity:Password:RequiredLength");
                options.Password.RequireUppercase = config.GetValue<bool>("Identity:Password:RequireUppercase");
                options.Password.RequireNonAlphanumeric = config.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
            });

            var identityServerBuilder =
                services.AddIdentityServer(options =>
                {
                    var issuerUri = config["Identity:Issuer"];
                    if (!string.IsNullOrEmpty(issuerUri))
                    {
                        options.IssuerUri = issuerUri;
                    }
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                    options.EmitStaticAudienceClaim = true;

                })
                    .AddProfileService<ProfileService>()
                    .AddCorsPolicyService<InMemoryCorsPolicyService>()
                    .AddResourceOwnerValidator<PasswordUserValidator>();

            if (env.IsDevelopment())
            {
                identityServerBuilder
                    .AddDeveloperSigningCredential()
                    .AddInMemoryIdentityResources(Config.Ids)
                    .AddInMemoryApiResources(Config.Apis)
                    .AddInMemoryApiScopes(Config.ApisScopes)
                    .AddInMemoryClients(Config.Clients(config));
            }
            else
            {
                var migrationsAssembly = typeof(DependencyInjection).GetTypeInfo().Assembly.GetName();
                var connectionString = config["ConnectionStrings:IdentityServer"];
                var keyFilePath = config["SigninKeyCredentials:KeyFilePath"];
                var keyFilePassword = config["SigninKeyCredentials:KeyFilePassword"];
                var certificate = new X509Certificate2(keyFilePath, keyFilePassword);
                identityServerBuilder.AddSigningCredential(certificate)
                    .AddConfigurationStore(options =>
                    {
                        options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly.Name));
                    })
                    .AddOperationalStore(options =>
                    {
                        options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly.Name));
                    });
            }
            services.AddLocalApiAuthentication();
        }

        internal static void ConfigureMvcAndRazor(IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
            var mvcBuilder = services.AddRazorPages();

#if DEBUG
            if (env.IsDevelopment())
            {
                mvcBuilder.AddRazorRuntimeCompilation();
            }
#endif
        }
    }
}
