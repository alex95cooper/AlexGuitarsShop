namespace AlexGuitarsShop.DAL.Interfaces;

public interface IRepository<T>
{
    Task<T> GetAsync(int id);
    
    Task AddAsync(T entity);
    
    Task<List<T>> SelectAsync();
    
    Task<List<T>> SelectByLimitAsync(int offset, int limit);
    
    Task UpdateAsync(T entity);
    
    Task DeleteAsync(int id);
    
    Task<int> GetCountAsync();
}