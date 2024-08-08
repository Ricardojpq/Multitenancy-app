using Authentication.Data;
using Authentication.Models.UserManagement;
using Authentication.Services.Interfaces;

namespace Authentication.Services
{
    public class RolesRepository : IRolesRepository
    {
        private readonly ApplicationDbContext _context;

        public RolesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<RoleClaimDto> GetClaims(int id)
        {
            return _context.RoleClaims
                .Where(x => x.RoleId == id)
                .Select(x => new RoleClaimDto
                {
                    Id = x.Id,
                    RoleId = x.RoleId,
                    ClaimType = x.ClaimType,
                    ClaimValue = x.ClaimValue
                }).ToList();
        }

        public IEnumerable<RoleDto> GetAll(string clientId)
        {
            return _context.Roles
                .Where(x => string.IsNullOrEmpty(clientId) || x.ClientId == clientId)
                .Select(x => new RoleDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    NormalizedName = x.NormalizedName,
                    RoleClaims = _context.RoleClaims.Where(r => r.RoleId == x.Id).Select(rc => new UserClaimDto
                    {
                        Id = rc.Id,
                        Type = rc.ClaimType,
                        Value = rc.ClaimValue
                    }).ToList()
                })
                .ToList();
        }
    }
}
