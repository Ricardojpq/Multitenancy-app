using System.ComponentModel.DataAnnotations;

namespace ViewModels.Authentication
{
    public class ResetUserPassVM
    {
        [Required]
        [StringLength(100, ErrorMessage = "Must be at least {1} characters long.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string PasswordReset { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("PasswordReset", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPasswordReset { get; set; }

        public string Token { get; set; }
        public string Email { get; set; }
    }
}
