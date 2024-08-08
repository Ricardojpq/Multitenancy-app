namespace Authentication.Models.UserManagement
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }

        public List<UserClaimDto> RoleClaims { get; set; }
    }
}
