using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookupTables.Database
{
    public class AppSettings
    {
        public bool RunDbInitializer { get; set; }
        public string LongTimeCache { get; set; }
        public bool SeedGeneralData { get; set; }
        public bool SeedCountries { get; set; }
        public bool RequiredSwagger { get; set; }
        public string SwaggerStyle { get; set; }
        public string TermsOfService { get; set; }
        public string PortfolioUrl { get; set; }
        public string SharedMail { get; set; }
        public string MicroserviceName { get; set; }
        public string ApiName { get; set; }
        public string ApiSecret { get; set; }
        public bool RequiredControllers { get; set; }
        public bool RequiredAuthorization { get; set; }
        public string IdentityServerUrl { get; set; }
        public bool SeedTenants { get; set; }
    }
}
