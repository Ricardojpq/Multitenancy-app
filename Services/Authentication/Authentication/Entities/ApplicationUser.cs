using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Authentication.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<int>
    {
        private ApplicationUser(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

        private ILazyLoader LazyLoader { get; set; }

        public ApplicationUser(string email)
        {
            UserName = email;
            Email = email;
            CreatedDate = DateTime.Now;
            OldPasswords = new List<PasswordHistory>();
        }
        
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool IsActive { get; set; } = true;
        public string? ProviderId { get; set; }
        
        public string? MaintenanceUser { get; set; }
        public DateTime CreatedDate { get; private set; }

        public string? TenantId { get; set; }

        public ICollection<ApplicationUserRole> Roles { get; private set; } = new List<ApplicationUserRole>();

        public ICollection<IdentityUserClaim<int>> Claims { get; private set; } = new List<IdentityUserClaim<int>>();

        public IEnumerable<ApplicationUserClientScope> Clients { get; private set; } = new List<ApplicationUserClientScope>();

        private ICollection<PasswordHistory> _oldPasswords;
        public ICollection<PasswordHistory> OldPasswords
        {
            get => LazyLoader.Load(this, ref _oldPasswords);
            set => _oldPasswords = value;
        }

        public const int MaxRememberedPasswords = 3;

        public void AddCurrentPasswordHashToHistory()
        {
            if (OldPasswords.Count == MaxRememberedPasswords)
            {
                var oldest = OldPasswords.OrderBy(x => x.SetOn).First();
                _oldPasswords.Remove(oldest);
            }

            _oldPasswords.Add(new PasswordHistory(this, PasswordHash));
        }

        public void AddClient(string clientId)
        {
            // Do not try to add already existing clients
            if (Clients.Any(x => x.ClientId == clientId)) return;

            (Clients as List<ApplicationUserClientScope>)?.Add(new ApplicationUserClientScope
            {
                User = this,
                ClientId = clientId,
                MaintenanceUser = this.MaintenanceUser
            });
        }

        public void RemoveClient(string clientId)
        {
            var userClient = Clients.FirstOrDefault(x => x.ClientId == clientId);
            if (userClient != null)
            {
                (Clients as List<ApplicationUserClientScope>)?.Remove(userClient);
            }
        }
    }
}
