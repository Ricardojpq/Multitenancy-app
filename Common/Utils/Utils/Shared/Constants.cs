using System.Collections.ObjectModel;

namespace Utils.Shared
{
    public class Constants
    {
        public const string SUCCESS = "Success";
        public const string ERROR = "Error";
        public const string ERRORS = "Errors";
        public const string NOT_FOUND = "Record Not Found";
        public const string NotFound = "Not Found";
        public const string INTERNAL_ERROR = "Internal Error";

        public const string JwtClaimIdentifier_Rol = "rol";
        public const string JwtClaimIdentifier_Id = "id";
        public const string JwtClaims_ApiAccess = "api_access";

        public const string Lookup_Table_IsActive = "IsActive";
        public const string Lookup_Table_UpdatedBy = "UpdatedBy";
        public const string Lookup_Table_CreatedDate = "CreatedDate";
        public const string Lookup_Table_TenantId = "TenantId";

        public const string PropertyEffectiveDate = "EffectiveDate";
        public const string PropertyExpirationDate = "ExpirationDate";
        public const string ASP_Actions_Create = "Create";
        public const string ASP_Actions_Edit = "Edit";

        public const string SearchableFields = "SearchableFields";
        public const string PropertyIsActive = "IsActive";

        public const string TenantId = "TenantId";
        public const string UserId = "UserId";
        public const string CountryId = "CountryId";

        #region Attachemnt Constants

        public const string AttachmentProfilePhoto = "attachmentFile";

        #region Error Messages
        public const string ErrorEntityTypeNotFound = "Entity type not found";
        public const string ErrorSavingAttachment = "Error saving attachment";
        public const string ErrorOnDbUpdateSaving = "Invalid data found on saving";
        #endregion

        #endregion

        #region DATATYPE 
        public const string INT = "INT";
        public const string DECIMAL = "DECIMAL";
        public const string DOUBLE = "DOUBLE";
        public const string DATETIME = "DATETIME";
        public const string DATETIMEIN = "DATETIMEIN";
        public const string DATETIMEON = "DATETIMEON";
        public const string TIME = "TIME";
        public const string STRING = "STRING";
        public const string CHAR = "CHAR";
        public const string BOOL = "BOOL";
        #endregion

        #region Filter types
        public const string UNIQUEFILTER = "UNIQUEFILTER";
        public const string MULTIPLEFILTER = "MULTIPLEFILTER";
        public const string EXACTFILTER = "EXACTFILTER";
        #endregion

        #region Reference Fields
        public const string DAL_Id = "_Id";
        public const string DAL_Name = "Name";
        public const string IsActive = "IsActive";
        #endregion

        #region Reference Models
        public const string DAL_Assembly = "LookupTables.Domain";
        public const string DAL_LookupTable = "LookupTable";
        #endregion

        #region Lookup Table
        public const string LookupTableCategory = "LookupTableCategory";
        public const string LookupTableList = "LookupTableList";

        public const string Lookup_Table_Id = "_id";
        public const string Lookup_Table_IsDeleted = "IsDeleted";
        public const string Lookup_Table_Required = "Required";
        public const string Lookup_Table_Name = "Name";
        public const string Lookup_Table_Code = "Code";
        public const string Lookup_Table_Delimiter = "Delimiter";
        public const string Lookup_Table_Description = "Description";
        public const string Lookup_Table_EffectiveDate = "EffectiveDate";
        public const string Lookup_Table_ExpirationDate = "ExpirationDate";

        public const string Lookup_Table_Details_Partial = "_LookupTableDetails";
        public const string Lookup_Table_Edit_Partial = "_LookupTableEdit";

        public const string Lookup_Table_AddressType = "AddressType";
        public const string Lookup_Table_Bank = "Bank";
        public const string Lookup_Table_BankAccountType = "BankAccountType";
        public const string Lookup_Table_CommunicationType = "CommunicationType";
        public const string Lookup_Table_Company = "Company";
        public const string Lookup_Table_CompanyServiceDatabase = "CompanyServiceDatabase";
        public const string Lookup_Table_County = "County";
        public const string Lookup_Table_Currency = "Currency";
        public const string Lookup_Table_Gender = "Gender";
        public const string Lookup_Table_InternetSupplier = "InternetSupplier";
        public const string Lookup_Table_InternetType = "InternetType";
        public const string Lookup_Table_Municipality = "Municipality";
        public const string Lookup_Table_Nationality = "Nationality";
        public const string Lookup_Table_Parish = "Parish";
        public const string Lookup_Table_PhoneType = "PhoneType";
        public const string Lookup_Table_ProviderType = "ProviderType";
        public const string Lookup_Table_Service = "Service";
        public const string Lookup_Table_Settings = "Settings";
        public const string Lookup_Table_State = "State";
        public const string Lookup_Table_StoreType = "StoreType";
        public const string Lookup_Table_Tenant = "Tenant";
        public const string Lookup_Table_User = "User";
        public const string Lookup_Table_Zone = "Zone";
        
        public const string Lookup_TableVM_ColumnName = "ColumnName";
        public const string Lookup_TableVM_Value = "Value";

        public static string Lookup_Table_Template_Name(string tableName) => $"Template_{tableName ?? ""}.xlsx";
        public const string Lookup_Table_Json_Name = "LookupTablesConfiguration.json";

        #endregion

        public const string SearchableDataTableFields = "SearchableDataTableFields";
        public const string SearchableTextFields = "SearchableTextFields";
        public const string SearchableTypeFields = "SearchableTypeFields";
        public const string ViewModelEntityName = "EntityName";
        public const string ViewModelEntityColumnId = "EntityColumnID";
        public const string ViewModelEntityColumnDescription = "EntityColumnDescription";
        public const string ViewModelEntityDetailsFields = "LookupEntityDetailsFields";
        public const string ViewModelEntityRelations = "EntityRelations";
        public const string ViewModelEntityRelationName = "EntityRelationName";
        public const string ViewModelEntityRelationForeignKey = "EntityRelationForeignKey";
        public const string ViewModelEntityRelationFields = "LookupEntityRelationFields";


        #region Identity Server
        public const string RoleNameClaim = "Role";
        public const string AWSAuth = "AWSAuth";
        public const string CertThumbprint = "CertThumbprint";
        public const string SigninKeyCredentials = "SigninKeyCredentials";
        public const string LeviatanClientId = "LeviatanSaas";
        #endregion

        #region CookieAuth
        public const string CookieName = "idAz";
        public const string CypherPreFix = "ArZ";
        #endregion

        #region Attachments
        public const int Attachment_PayerPro_Payers = 1;
        public const int Attachment_PayerPro_Provider = 2;
        public const int Attachment_PayerPro_Plan = 4;
        #endregion

    }
}
