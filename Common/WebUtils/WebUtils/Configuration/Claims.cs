using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace WebUtils.Configuration
{
    public static class Claims
    {
        public static readonly Claim AuthorizationEditPayer
            = new Claim(Names.Permission, Names.AuthorizationEditPayer);

        public static readonly Claim AuthorizationViewPayer
            = new Claim(Names.Permission, Names.AuthorizationViewPayer);

        public static readonly Claim AuthorizationViewPlanBenefitPlan
            = new Claim(Names.Permission, Names.AuthorizationViewPlanBenefitPlan);

        public static readonly Claim AuthorizationEditPlanBenefitPlan
            = new Claim(Names.Permission, Names.AuthorizationEditPlanBenefitPlan);

        public static readonly Claim AuthorizationViewPlanBenefitGroup
            = new Claim(Names.Permission, Names.AuthorizationViewPlanBenefitGroup);

        public static readonly Claim AuthorizationEditPlanBenefitGroup
            = new Claim(Names.Permission, Names.AuthorizationEditPlanBenefitGroup);

        public static readonly Claim AuthorizationViewPlanBenefitPlanGroup
            = new Claim(Names.Permission, Names.AuthorizationViewPlanBenefitPlanGroup);

        public static readonly Claim AuthorizationEditPlanBenefitPlanGroup
            = new Claim(Names.Permission, Names.AuthorizationEditPlanBenefitPlanGroup);

        public static readonly Claim AuthorizationViewClaimSearch
            = new Claim(Names.Permission, Names.AuthorizationViewClaimSearch);

        public static readonly Claim AuthorizationEditClaimSearch
            = new Claim(Names.Permission, Names.AuthorizationEditClaimSearch);

        public static readonly Claim AuthorizationViewClaimDashboard
            = new Claim(Names.Permission, Names.AuthorizationViewClaimDashboard);


        public static readonly Claim AuthorizationViewServiceCategories
            = new Claim(Names.Permission, Names.AuthorizationViewServiceCategories);

        public static readonly Claim AuthorizationEditServiceCategories
            = new Claim(Names.Permission, Names.AuthorizationEditServiceCategories);

        public static readonly Claim AuthorizationViewFeeSchedule
            = new Claim(Names.Permission, Names.AuthorizationViewFeeSchedule);

        public static readonly Claim AuthorizationEditFeeSchedule
            = new Claim(Names.Permission, Names.AuthorizationEditFeeSchedule);

        public static readonly Claim AuthorizationViewSystemCode
            = new Claim(Names.Permission, Names.AuthorizationViewSystemCode);

        public static readonly Claim AuthorizationEditSystemCode
            = new Claim(Names.Permission, Names.AuthorizationEditSystemCode);


        public static readonly Claim AuthorizationViewUserManagement
            = new Claim(Names.Permission, Names.AuthorizationViewUserManagement);

        public static readonly Claim AuthorizationEditUserManagement
            = new Claim(Names.Permission, Names.AuthorizationEditUserManagement);

        public static readonly Claim AuthorizationViewTradingPartner
            = new Claim(Names.Permission, Names.AuthorizationViewTradingPartner);

        public static readonly Claim AuthorizationEditTradingPartner
            = new Claim(Names.Permission, Names.AuthorizationEditTradingPartner);

        public static readonly Claim AuthorizationViewProprietaryFile
            = new Claim(Names.Permission, Names.AuthorizationViewProprietaryFile);

        public static readonly Claim AuthorizationEditProprietaryFile
            = new Claim(Names.Permission, Names.AuthorizationEditProprietaryFile);

        public static readonly Claim AuthorizationViewElegibilityImportProfile
            = new Claim(Names.Permission, Names.AuthorizationViewElegibilityImportProfile);

        public static readonly Claim AuthorizationEditElegibilityImportProfile
            = new Claim(Names.Permission, Names.AuthorizationEditElegibilityImportProfile);

        public static readonly Claim AuthorizationViewProfile
            = new Claim(Names.Permission, Names.AuthorizationViewProfile);

        public static readonly Claim AuthorizationEditProfile
            = new Claim(Names.Permission, Names.AuthorizationEditProfile);

        public static readonly Claim AuthorizationViewRun
            = new Claim(Names.Permission, Names.AuthorizationViewRun);

        public static readonly Claim AuthorizationEditRun
            = new Claim(Names.Permission, Names.AuthorizationEditRun);

        public static readonly Claim AuthorizationViewProvider
            = new Claim(Names.Permission, Names.AuthorizationViewProvider);

        public static readonly Claim AuthorizationEditProvider
            = new Claim(Names.Permission, Names.AuthorizationEditProvider);


        public static readonly Claim AuthorizationEditMember
            = new Claim(Names.Permission, Names.AuthorizationEditMember);

        public static readonly Claim AuthorizationViewMember
            = new Claim(Names.Permission, Names.AuthorizationViewMember);

        public static readonly Claim AuthorizationEditVendor
            = new Claim(Names.Permission, Names.AuthorizationEditVendor);

        public static readonly Claim AuthorizationViewVendor
            = new Claim(Names.Permission, Names.AuthorizationViewVendor);

        public static readonly Claim AuthorizationEditAuthorization
            = new Claim(Names.Permission, Names.AuthorizationEditAuthorization);

        public static readonly Claim AuthorizationViewAuthorization
            = new Claim(Names.Permission, Names.AuthorizationViewAuthorization);


        public static class Names
        {
            public const string Permission = "Permission";

            public const string AuthorizationViewPayer = "Payers.View";
            public const string AuthorizationEditPayer = "Payers.Edit";

            public const string AuthorizationViewProvider = "Providers.View";
            public const string AuthorizationEditProvider = "Providers.Edit";

            public const string AuthorizationViewMember = "Members.View";
            public const string AuthorizationEditMember = "Members.Edit";

            public const string AuthorizationViewVendor = "Vendors.View";
            public const string AuthorizationEditVendor = "Vendors.Edit";


            public const string AuthorizationViewPlanBenefitPlan = "PlanBenefits.Plan.View";
            public const string AuthorizationEditPlanBenefitPlan = "PlanBenefits.Plan.Edit";

            public const string AuthorizationViewPlanBenefitGroup = "PlanBenefits.Group.View";
            public const string AuthorizationEditPlanBenefitGroup = "PlanBenefits.Group.Edit";

            public const string AuthorizationViewPlanBenefitPlanGroup = "PlanBenefits.PlanGroup.View";
            public const string AuthorizationEditPlanBenefitPlanGroup = "PlanBenefits.PlanGroup.Edit";

            public const string AuthorizationViewClaimSearch = "Claims.View";
            public const string AuthorizationEditClaimSearch = "Claims.Edit";

            public const string AuthorizationViewClaimDashboard = "Claims.Dashboard";


            public const string AuthorizationViewServiceCategories = "Configuration.ServiceCategories.View";
            public const string AuthorizationEditServiceCategories = "Configuration.ServiceCategories.Edit";

            public const string AuthorizationViewFeeSchedule = "Configuration.FeeSchedule.View";
            public const string AuthorizationEditFeeSchedule = "Configuration.FeeSchedule.Edit";

            public const string AuthorizationViewSystemCode = "Configuration.SystemCode.View";
            public const string AuthorizationEditSystemCode = "Configuration.SystemCode.Edit";

            public const string AuthorizationViewUserManagement = "Configuration.UserManagement.View";
            public const string AuthorizationEditUserManagement = "Configuration.UserManagement.Edit";

            public const string AuthorizationViewTradingPartner = "EDI.TradingPartner.View";
            public const string AuthorizationEditTradingPartner = "EDI.TradingPartner.Edit";

            public const string AuthorizationViewProprietaryFile = "EDI.ProprietaryFile.View";
            public const string AuthorizationEditProprietaryFile = "EDI.ProprietaryFile.Edit";

            public const string AuthorizationViewElegibilityImportProfile = "EDI.ElegibilityImportProfile.View";
            public const string AuthorizationEditElegibilityImportProfile = "EDI.ElegibilityImportProfile.Edit";

            public const string AuthorizationViewProfile = "Payment Processing.Profile.View";
            public const string AuthorizationEditProfile = "Payment Processing.Profile.Edit";

            public const string AuthorizationViewRun = "Payment Processing.Run.View";
            public const string AuthorizationEditRun = "Payment Processing.Run.Edit";

            public const string AuthorizationViewAuthorization = "Authorization.View";
            public const string AuthorizationEditAuthorization = "Authorization.Edit";

            public static string[] GetAll()
            {
                return typeof(Names)
                    .GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Where(x => x.IsLiteral && x.FieldType == typeof(string))
                    .Select(x => x.GetValue(null)?.ToString())
                    .ToArray();
            }
        }
    }
}
