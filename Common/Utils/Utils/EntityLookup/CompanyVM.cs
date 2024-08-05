namespace Utils.EntityLookup;

public class CompanyVM : BaseEntityVM
{
    public string Email { get; set; }
    public string CommercialIdentifier { get; set; }
    public string SortCode { get; set; }
    public string StatusId { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public string MaintenanceUser { get; set; }
    public string? UpdatedBy { get; set; }
    public string Address { get; set; }
    public AdressTypeVM AddressType { get; set; }
    public bool WithHoldingAgentWithHoldingAgent { get; set; }
    public string LegalRepresentative { get; set; }
    public Guid? ParentCompanyId { get; set; }
    public ZoneVM Zone { get; set; }
    public StoreTypeVM StoreType { get; set; }
    public CountryVM Country { get; set; }
    public PhoneVM Phone { get; set; }
    public List<CompanyServiceDatabaseVM> CompanyServiceDatabase { get; set; }
    public InternetProviderSupplierVM InternetProviderSupplier { get; set; }
    public InternetTypeVM InternetType { get; set; }
    public CoordinateVM Coordinate { get; set; }
    public bool TaxExempt { get; set; }

    public CompanyVM(
        string email, string commercialIdentifier, string sortCode, string statusId, string maintenanceUser,
        string address, AdressTypeVM addressType, bool withHoldingAgentWithHoldingAgent, string legalRepresentative, Guid? parentCompanyId, 
        ZoneVM zone, StoreTypeVM storeType, CountryVM country,PhoneVM phone, List<CompanyServiceDatabaseVM> companyServiceDatabase,
        InternetProviderSupplierVM internetProviderSupplier, InternetTypeVM internetType, CoordinateVM coordinate,bool taxExempt, string updatedBy)
    {
        Email = email;
        CommercialIdentifier = commercialIdentifier;
        SortCode = sortCode;
        StatusId = statusId;
        IsDeleted = false;
        IsActive = true;
        CreatedDate = DateTime.Now.ToUniversalTime();
        MaintenanceUser = maintenanceUser;
        Address = address;
        AddressType = addressType;
        WithHoldingAgentWithHoldingAgent = withHoldingAgentWithHoldingAgent;
        LegalRepresentative = legalRepresentative;
        ParentCompanyId = parentCompanyId;
        Zone = zone;
        StoreType = storeType;
        Country = country;
        Phone = phone;
        CompanyServiceDatabase = companyServiceDatabase;
        InternetProviderSupplier = internetProviderSupplier;
        InternetType = internetType;
        Coordinate = coordinate;
        TaxExempt = taxExempt;
        UpdatedBy = updatedBy;
    }
}

