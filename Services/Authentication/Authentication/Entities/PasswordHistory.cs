using Microsoft.AspNetCore.Identity;

namespace Authentication.Entities
{
    public class PasswordHistory
    {

        private PasswordHistory()
        {
        }

        public PasswordHistory(int userId, string passwordHash)
        {
            UserId = userId;
            PasswordHash = passwordHash;
            SetOn = DateTimeOffset.Now;
            CreatedDate = DateTime.Now;
        }

        public PasswordHistory(ApplicationUser user, string passwordHash)
        {
            User = user;
            PasswordHash = passwordHash;
            SetOn = DateTimeOffset.Now;
            CreatedDate = DateTime.Now;
        }

        public int Id { get; private set; }

        public int UserId { get; private set; }
        public ApplicationUser User { get; private set; }

        public string PasswordHash { get; private set; }
        public DateTimeOffset SetOn { get; private set; }

        public DateTime CreatedDate { get; private set; }

        public void UpdateHash(
            IPasswordHasher<ApplicationUser> passwordHasher, ApplicationUser user, string password)
        {
            PasswordHash = passwordHasher.HashPassword(user, password);
        }

    }
}
