using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.Authentication
{
    public class UserRolesVM : BaseViewModel
    {        
        public string Name { get; set; }

        [Display(Name = "Role Name")]
        public string NormalizedName { get; set; }

        public List<UserClaimsVM> RoleClaims { get; set; }

    }
}
