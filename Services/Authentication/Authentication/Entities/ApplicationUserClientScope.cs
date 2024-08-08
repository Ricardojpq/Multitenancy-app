namespace Authentication.Entities
{
    public class ApplicationUserClientScope
    {
        public ApplicationUserClientScope()
        {
            CreatedDate = DateTime.Now;
        }

        public ApplicationUser User { get; set; }
        public int UserId { get; set; }

        public string ClientId { get; set; }

        public string? MaintenanceUser { get; set; }
        public DateTime CreatedDate { get; private set; }
    }
}
