using Authentication.Models.UserManagement;

namespace Authentication.Services.Interfaces
{
    public interface IRolesRepository
    {

        IEnumerable<RoleClaimDto> GetClaims(int id);
        IEnumerable<RoleDto> GetAll(string clientId);
    }
}
