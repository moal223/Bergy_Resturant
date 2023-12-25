namespace Restro.Service
{
    public interface IDataHelper<T>
    {
        Task<T> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task<IList<T>> GetByAllAsync();
    }
}
