using System.ComponentModel.DataAnnotations;

namespace ViewModels.Authentication
{
    public class LoginInputVM
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(80)]
        public string EmailAddress { get; set; }

        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; }
    }
}
