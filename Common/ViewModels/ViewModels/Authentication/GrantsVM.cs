using System;
using System.Collections.Generic;

namespace ViewModels.Authentication
{
    public class GrantsVM
    {
        public IEnumerable<GrantVM> Grants { get; set; }
    }

    public class GrantVM
    {
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientUrl { get; set; }
        public string ClientLogoUrl { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Expires { get; set; }
        public IEnumerable<string> IdentityGrantNames { get; set; }
        public IEnumerable<string> ApiGrantNames { get; set; }
    }

}
