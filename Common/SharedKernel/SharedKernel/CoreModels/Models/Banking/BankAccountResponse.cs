using SharedKernel.SqlScheme.Response;
using Newtonsoft.Json;

namespace SharedKernel.CoreModels.Models.Banking;

public class BankAccountResponse : BaseResponse
{
    [JsonProperty("accountNumber")] public string AccountNumber;
    [JsonProperty("bankId")] public string BankId;
    [JsonProperty("bankAccountTypeId")] public string BankAccountTypeId;
    [JsonProperty("currencyId")] public string CurrencyId;
    [JsonProperty("openAt")] public DateOnly? OpenAt;
    [JsonProperty("dateOpeningBalance")] public DateOnly? DateOpeningBalance;
    [JsonProperty("reconciledBalance")] public double? ReconciledBalance;
    [JsonProperty("bankBalance")] public double? BankBalance;
    [JsonProperty("lastDateReconciliation")] public DateOnly? LastDateReconciliation;
    [JsonProperty("lastReconciliationBalance")] public double? LastReconciliationBalance;
    [JsonProperty("isActive")] public bool IsActive;
    [JsonProperty("createdDate")] public DateTime CreatedDate;
    [JsonProperty("createdBy")] public string CreatedBy;
    [JsonProperty("isDeleted")] public bool IsDeleted;
    [JsonProperty("updatedDate")] public DateTime UpdatedDate;
    [JsonProperty("updatedBy")] public object UpdatedBy;
}


public class BankAccountMinimalResponse 
{
    [JsonProperty("accountNumber")] public string AccountNumber;
    [JsonProperty("bankId")] public string BankId;
    [JsonProperty("bankAccountTypeId")] public string BankAccountTypeId;
    [JsonProperty("currencyId")] public string CurrencyId;
    [JsonProperty("name")] public string Name;
    [JsonProperty("description")] public string Description;
}
