using Authentication.Data;
using Authentication.Models.UserManagement;
using Authentication.Services.Interfaces;

namespace Authentication.Services
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _context;

        public UsersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUserDto> GetAll(int? roleId, string clientId, bool includeInactive)
        {
            return _context.Users
                .Where(x => (includeInactive || x.IsActive) &&
                            (!roleId.HasValue || x.Roles.Any(r => r.RoleId == roleId)) &&
                            (string.IsNullOrEmpty(clientId) || x.Clients.Any(c => c.ClientId == clientId)))
                .Select(ApplicationUserDto.FromApplicationUser(clientId))
                .ToList();
        }

        public ApplicationUserDto GetSingle(int id)
        {
            return _context.Users
                .Where(x => x.Id == id)
                .Select(ApplicationUserDto.FromApplicationUser())
                .FirstOrDefault();
        }

        public ApplicationUserDto GetByEmail(string email)
        {
            return _context.Users
                .Where(x => x.Email == email)
                .Select(ApplicationUserDto.FromApplicationUser())
                .FirstOrDefault();
        }

        public IEnumerable<ApplicationUserDto> SearchUsers(string query, bool includeInactive)
        {
            var Users = _context.Users.Where(u => u.Clients.Any(c => c.ClientId == Configuration.Clients.AngularClient))
                                .Select(user => new ApplicationUserDto
                                {
                                    Id = user.Id,
                                    FirstName = user.FirstName,
                                    LastName = user.LastName,
                                    Email = user.Email,
                                    UserName = user.UserName
                                }).ToList();

            return Users;
        }
    }
}
