

using Authentication.Entities;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Authentication.Services.Implementation
{
    public class ProfileService : IProfileService
    {

        private readonly UserManager _userManager;
        public ProfileService(UserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subject = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));
            var subjectId = subject.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            var userId = Convert.ToInt32(subjectId);

            var user = await _userManager.Users
                .Include(x => x.Roles)
                .ThenInclude(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid subject identifier");
            }

            var claims = await GetClaimsFromUser(user, context.Client.ClientId);
            context.IssuedClaims = claims.ToList();
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var subject = context.Subject ??
                          throw new ArgumentNullException(nameof(context.Subject));

            var subjectId = subject.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            var user = await _userManager.FindByIdAsync(subjectId);

            context.IsActive = false;

            if (user != null)
            {
                if (_userManager.SupportsUserSecurityStamp)
                {
                    var securityStamp = subject.Claims.Where(c => c.Type == "security_stamp")
                        .Select(c => c.Value).SingleOrDefault();
                    if (securityStamp != null)
                    {
                        var dbSecurityStamp = await _userManager.GetSecurityStampAsync(user);
                        if (dbSecurityStamp != securityStamp) return;
                    }
                }

                context.IsActive =
                    user.IsActive ||
                    !user.LockoutEnabled ||
                    !user.LockoutEnd.HasValue ||
                    user.LockoutEnd <= DateTime.Now;
            }
        }

        private async Task<IEnumerable<Claim>> GetClaimsFromUser(
            ApplicationUser user, string clientId)
        {
            var claims = new List<Claim>
            {
                // JWT Claim Types
                new Claim(JwtClaimTypes.Subject, user.Id.ToString()),

                // System Claims
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),

                // Identity Claims
                // new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),


                
            };
            // Role Claims
            if (user.Roles.Any())
            {
                foreach (var role in user.Roles.Where(x => x.Role.ClientId == clientId))
                {
                    claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role.Role.Name));
                }
            }

            if (!string.IsNullOrWhiteSpace(user.FirstName))
            {
                claims.Add(new Claim(JwtClaimTypes.Name, user.FirstName));
            }

            if (!string.IsNullOrWhiteSpace(user.TenantId))
                claims.Add(new Claim("tenantId", user.TenantId));

            var userClaims = await _userManager.GetClaimsAsync(user);


            // Validation Identity Claims
            if (_userManager.SupportsUserEmail)
            {
                claims.AddRange(new[]
                {
                    new Claim(JwtClaimTypes.Email, user.Email),
                    new Claim(JwtClaimTypes.EmailVerified, user.EmailConfirmed ? "true" : "false", ClaimValueTypes.Boolean)
                });
            }

            return claims;
        }
    }
}
