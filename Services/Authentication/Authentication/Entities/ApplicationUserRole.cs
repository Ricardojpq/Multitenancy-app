using Microsoft.AspNetCore.Identity;

namespace Authentication.Entities
{
    public class ApplicationUserRole : IdentityUserRole<int>
    {
        public ApplicationUserRole()
        {
            CreatedDate = DateTime.Now;
        }

        public override int UserId { get; set; }
        public ApplicationUser User { get; set; }

        public override int RoleId { get; set; }
        public ApplicationRole Role { get; set; }

        public DateTime CreatedDate { get; private set; }

    }
}
