namespace ViewModels.Authentication
{
    public class UserClaimsVM : BaseViewModel
    {
        public bool IsGranted { get; set; }
        public string Type { get; set; } 
        public string Value { get; set; }
    }
}