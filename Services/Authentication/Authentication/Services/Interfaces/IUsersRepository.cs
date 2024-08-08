using Authentication.Models.UserManagement;

namespace Authentication.Services.Interfaces
{
    public interface IUsersRepository
    {

        IEnumerable<ApplicationUserDto> GetAll(int? roleId, string clientId, bool includeInactive);
        ApplicationUserDto GetSingle(int id);
        ApplicationUserDto GetByEmail(string email);
        IEnumerable<ApplicationUserDto> SearchUsers(string query, bool includeInactive);

    }
}
