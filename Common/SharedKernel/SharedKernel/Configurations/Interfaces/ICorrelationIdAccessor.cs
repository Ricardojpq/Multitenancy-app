namespace SharedKernel.Configurations.Interfaces;

public interface ICorrelationIdAccessor
{
    string GetCorrelationId();
    void SetCorrelationId(string correlationId);
}