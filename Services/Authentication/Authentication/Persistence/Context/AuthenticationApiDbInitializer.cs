using System.Security.Claims;
using Authentication.Configuration;
using Authentication.Data;
using Authentication.Entities;
using Authentication.Util;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Constants = Utils.Shared.Constants;

namespace Authentication.Persistence.Context
{
    internal static class AuthenticationApiDbInitializer
    {
        internal static async Task InitializeAsync(IApplicationBuilder app, bool seedTestUser, string tenandIdTestUser)
        {
            using var scope = app.ApplicationServices
                .GetService<IServiceScopeFactory>()
                ?.CreateScope();

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
                await SeedUsers(scope.ServiceProvider, tenandIdTestUser);
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

        private static async Task SeedUsers(IServiceProvider serviceProvider, string tenandIdTestUser)
        {
            const string userName = "testuser@portfolio.com";
            const string userPwd = "4DevOnly!";
            const string CountryId = "VEN";


            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByNameAsync(userName);
            if (user is null)
            {
                user = new ApplicationUser(userName)
                {
                    FirstName = "Test User FirstName",
                    LastName = "Test User LastName",
                    TenantId = tenandIdTestUser
                };

                user.AddClient(Clients.AngularClient);

                var result = await userManager.CreateAsync(user, userPwd);
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
                newClaims.Add(new Claim(Constants.TenantId, tenandIdTestUser));
                newClaims.Add(new Claim(Constants.UserId,user.Id.ToString()));
                newClaims.Add(new Claim(Constants.CountryId,CountryId));
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
