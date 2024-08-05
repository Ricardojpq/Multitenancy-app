using System.ComponentModel.DataAnnotations;

namespace ViewModels.Authentication
{
    public class UserCodesVM : BaseViewModel
    {
        [Required]
        [Display(Name = "Code")]
        [MaxLength(6)]
        public string Code { get; set; }
        public int UserId { get; set; }
        public int MaxCodeFailedCount { get; set; } = 0;
    }
}
