namespace Utils.EntityLookup;

public class CompanyServiceDatabaseVM : BaseEntityVM
{
    public string IPVP { get; set; }
    public string Instance { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
    public string DatabaseName { get; set; }
    public ServiceVM Service { get; set; }
}