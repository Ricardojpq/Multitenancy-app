namespace Utils.EntityLookup;

public class PhoneVM : BaseEntityVM
{
    public string Number { get; set; }
    public PhoneTypeVM PhoneType { get; set; }
}