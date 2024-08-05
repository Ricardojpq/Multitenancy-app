using Utils.Shared;

namespace Utils.BaseServices
{
    public interface ICRUDBulkInsertService<T> : ICRUDService<T> where T : class
    {
        Task<ServiceResponse> Add(ICollection<T> entityCollection);
    }
}
