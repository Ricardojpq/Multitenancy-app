namespace Utils.EntityLookup;

public class BaseEntityVM
{
    public Guid _Id { get; set; }
    public string? Name { get; set; } 
    public string? Description { get; set; }

    public BaseEntityVM()
    {
        _Id = Guid.NewGuid();
        Name = "";
        Description = "";
    }
}