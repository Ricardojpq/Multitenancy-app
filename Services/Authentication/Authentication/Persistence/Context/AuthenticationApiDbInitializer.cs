using System.Security.Claims;
using Authentication.Configuration;
using Authentication.Data;
using Authentication.Entities;
using Authentication.Models;
using Authentication.Util;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using Constants = Utils.Shared.Constants;

namespace Authentication.Persistence.Context
{
    internal static class AuthenticationApiDbInitializer
    {
        internal static async Task InitializeAsync(IApplicationBuilder app, bool seedTestUser)
        {
            using var scope = app.ApplicationServices
                .GetService<IServiceScopeFactory>()
                ?.CreateScope();

            var configuration = app.ApplicationServices
            .GetService<IConfiguration>()!;

            var tenant_1 = configuration.GetValue<string>("Tenant1Id");
            var tenant_2 = configuration.GetValue<string>("Tenant2Id");


            var applicationDbContext = scope.ServiceProvider
                .GetRequiredService<ApplicationDbContext>();

            if (applicationDbContext.Database.IsSqlServer())
            {
                await applicationDbContext.Database.MigrateAsync();
            }
            else
            {
                await applicationDbContext.Database.EnsureCreatedAsync();
            }

            await SeedRoles(scope.ServiceProvider);
            await SeedRolesClaims(scope.ServiceProvider);

            if (seedTestUser)
            {
                var users = new List<UserModel>()
                {
                    new UserModel()
                    {
                        UserName = "testuser_1@portfolio.com",
                        Password = "4DevOnly!",
                        Country = "VEN",
                        FirstName = "User 1",
                        LastName = "LastName",
                        TenantId = tenant_1!
                    },
                     new UserModel()
                    {
                        UserName = "testuser_2@portfolio.com",
                        Password = "4DevOnly!",
                        Country = "VEN",
                        FirstName = "User 2",
                        LastName = "LastName",
                        TenantId = tenant_2!
                    },
                };

                foreach (var user in users)
                {
                    await SeedUsers(scope.ServiceProvider, user);
                }
            }
        }

        private static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            Log.Information("Seeding user roles");
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            if (roleManager is null)
            {
                Log.Fatal("Impossible to seed roles, RoleManager is null");
                throw new NullReferenceException("roleManager cannot be null");
            }

            var allRoles = Roles.AngularClient.GetAll().ToList();
            foreach (var role in allRoles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    Log.Warning("Roles does not exists, Creating {Role}", role);
                    await roleManager.CreateAsync(new ApplicationRole(role)
                    {
                        ClientId = allRoles.Contains(role)
                            ? Clients.AngularClient
                                : string.Empty
                    });
                }
            }
        }

        private static async Task SeedUsers(IServiceProvider serviceProvider, UserModel userData)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByNameAsync(userData.UserName);
            if (user is null)
            {
                user = new ApplicationUser(userData.UserName)
                {
                    FirstName = userData.FirstName,
                    LastName = userData.LastName,
                    TenantId = userData.TenantId
                };

                user.AddClient(Clients.AngularClient);

                var result = await userManager.CreateAsync(user, userData.Password);
                if (!result.Succeeded)
                {
                    throw new ApplicationException("Impossible to seed user testuser@com, creation failed");
                }
                user.IsActive = true;
                user.EmailConfirmed = true;
                await userManager.UpdateAsync(user);
            }

            var userRoles = await userManager.GetRolesAsync(user);
            var allRoles = Roles.AngularClient.GetAll();
            var rolesToInsert = allRoles.Except(userRoles).ToList();

            if (rolesToInsert.Any())
            {
                await userManager.AddToRolesAsync(user, rolesToInsert);
            }

            var userClaims = (await userManager.GetClaimsAsync(user)).Select(x => x.Value).ToList();
            var allClaims = Claims.Names.AngularClient.GetAll();
            var claimsToInsert = allClaims
                .Except(userClaims)
                .ToList();

            if (claimsToInsert.Any())
            {
                var newClaims = claimsToInsert.Select(claimName => new Claim("Permission", claimName)).ToList();
                newClaims.Add(new Claim(Constants.TenantId, userData.TenantId));
                newClaims.Add(new Claim(Constants.UserId, user.Id.ToString()));
                newClaims.Add(new Claim(Constants.CountryId, userData.Country));
                newClaims.Add(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));
                await userManager.AddClaimsAsync(user, newClaims);
            }
        }

        private static async Task SeedRolesClaims(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            if (roleManager is null)
            {
                Log.Fatal("Impossible to seed roles, RoleManager is null");
                throw new NullReferenceException("roleManager cannot be null");
            }

            async Task GetRoleAndAddClaimsAsync(string roleName, IEnumerable<Claim> claimsToAdd)
            {
                var role = roleManager.Roles.First(x => x.Name == roleName);
                var currentClaims = await roleManager.GetClaimsAsync(role);
                var missingClaims = claimsToAdd.Where(x => currentClaims.All(_ => _.Type != x.Type && _.Value != x.Value));
                foreach (var claim in missingClaims)
                {
                    await roleManager.AddClaimAsync(role, claim);
                }
            }

            //SYS ADMIN
            await GetRoleAndAddClaimsAsync(Roles.AngularClient.SysAdmin, new[]
            {
                Claims.AuthorizationProfile,
                Claims.AuthorizationHttpClient,
                Claims.AuthorizationSwagger
            });

            //ADMINISTRATOR
            await GetRoleAndAddClaimsAsync(Roles.AngularClient.Administrator, new[]
            {
                Claims.AuthorizationProfile,
                Claims.AuthorizationHttpClient,
                Claims.AuthorizationSwagger
            });

            //COMMON USER
            await GetRoleAndAddClaimsAsync(Roles.AngularClient.CommonUser, new[]
            {
                Claims.AuthorizationProfile,
                Claims.AuthorizationHttpClient,
                Claims.AuthorizationSwagger
            });
        }
    }
}
