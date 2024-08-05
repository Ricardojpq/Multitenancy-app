using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ViewModels.Authentication
{
    public class UserVM : BaseViewModel
    {

        [Display(Name = "First Name")]
        [CustomRequired]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [CustomRequired]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(80)]
        [CustomRequired]
        public string Email { get; set; }

        public bool AllRolesAndClaims { get; set; }
        public IEnumerable<UserRolesVM> UserRoles { get; set; } = new List<UserRolesVM>();        
        
        public string ApplicationClientId { get; set; }
        public bool FromTuMedicoClient { get; set; }
        public List<int> Roles { 
            get
            {
                return UserRoles.Select(u => u.Id).ToList();
            }
        }
        public List<UserClaimsVM> Claims { get; set; }
    }
}
