using SharedKernel.CoreModels.LookupTable;

namespace SharedKernel.CoreModels.DTO.Banking;

public class BankAccountDTO : BaseModel
{
    public BankAccountDTO()
    {
        
    }
    public string AccountNumber { get; set; }
    public Guid BankId { get; set; }
    public LookupTableModel Bank { get; set; }
    public Guid BankAccountTypeId { get; set; }
    public LookupTableModel BankAccountType { get; set; }
    public Guid CurrencyId { get; set; }
    public LookupTableCurrencyModel Currency { get; set; }
    public DateOnly? OpenAt { get; set; }
    public DateOnly? DateOpeningBalance {get; set;}
    public decimal ReconciledBalance {get; set;}
    public decimal BankBalance {get; set;}
    public DateOnly? LastDateReconciliation {get; set;}
    public decimal LastReconciliationBalance {get; set;}
    public LookupTableModel Company { get; set; }
}