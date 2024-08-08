using Microsoft.AspNetCore.Identity;

namespace Authentication.Entities
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ApplicationRole() : base() { }

        public ApplicationRole(string name) : base(name)
        {
            CreatedDate = DateTime.Now;
        }

        public string ClientId { get; set; }
        public DateTime CreatedDate { get; private set; }

        public virtual ICollection<ApplicationUserRole> Users { get; }
            = new List<ApplicationUserRole>();

    }
}
