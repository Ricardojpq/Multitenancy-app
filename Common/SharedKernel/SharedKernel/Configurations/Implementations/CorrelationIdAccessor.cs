using SharedKernel.Configurations.Interfaces;

namespace SharedKernel.Configurations.Implementations;

public class CorrelationIdAccessor: ICorrelationIdAccessor
{
    private string _correlationId = Guid.NewGuid().ToString();
    
    public string GetCorrelationId() => _correlationId;

    public void SetCorrelationId(string correlationId) { 
        _correlationId = correlationId;
    }
}

