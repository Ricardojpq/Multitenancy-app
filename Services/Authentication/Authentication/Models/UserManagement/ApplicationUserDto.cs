using System.Linq.Expressions;
using Authentication.Entities;

namespace Authentication.Models.UserManagement
{
    public class ApplicationUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }

        public IEnumerable<RoleDto> UserRoles { get; set; }
        public IEnumerable<UserClaimDto> Claims { get; set; }

        public static Expression<Func<ApplicationUser, ApplicationUserDto>> FromApplicationUser(
            string clientId = null)
        {
            if (string.IsNullOrEmpty(clientId))
            {
                return user => new ApplicationUserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserName = user.UserName,
                    IsActive = user.IsActive,
                    EmailConfirmed = user.EmailConfirmed,
                    PhoneNumber = user.PhoneNumber,
                    TwoFactorEnabled = user.TwoFactorEnabled,
                    UserRoles = user.Roles.Select(x => new RoleDto
                    {
                        Id = x.RoleId,
                        Name = x.Role.Name,
                        NormalizedName = x.Role.NormalizedName
                    }).ToList(),
                    Claims = user.Claims.Select(x => new UserClaimDto
                    {
                        Id = x.Id,
                        Type = x.ClaimType,
                        Value = x.ClaimValue
                    }).ToList()
                };
            }

            return user => new ApplicationUserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                IsActive = user.IsActive,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber,
                TwoFactorEnabled = user.TwoFactorEnabled,
                UserRoles = user.Roles
                    .Where(x => x.Role.ClientId == clientId)
                    .Select(x => new RoleDto
                    {
                        Id = x.RoleId,
                        Name = x.Role.Name,
                        NormalizedName = x.Role.NormalizedName
                    })
                    .ToList(),
                Claims = user.Claims.Select(x => new UserClaimDto
                {
                    Id = x.Id,
                    Type = x.ClaimType,
                    Value = x.ClaimValue
                }).ToList()
            };
        }
    }
}
