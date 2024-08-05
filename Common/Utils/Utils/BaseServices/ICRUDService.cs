using Utils.Shared;

namespace Utils.BaseServices
{
    public interface ICRUDService<T> where T : class
    {
        Task<T> Get(int entityId);        
        Task<ServiceResponse> Add(T entity);
        Task<ServiceResponse> Update(T entity);
        Task<ServiceResponse> Delete(int entityId);
    }

    public interface ICRUDPaginated<T> : ICRUDService<T> where T :class
    {
        Task<PaginatedItemsViewModel<T>> GetAll(FilterParam fp);
    }

}
