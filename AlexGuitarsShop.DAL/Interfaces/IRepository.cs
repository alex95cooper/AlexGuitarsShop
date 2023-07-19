namespace AlexGuitarsShop.DAL.Interfaces;

public interface IRepository<T>
{
    Task<T> Get(int id);
    
    Task Add(T entity);
    
    Task<List<T>> Select();
    
    Task<List<T>> SelectByLimit(int offset, int limit);
    
    Task Update(T entity);
    
    Task Delete(int id);
    
    Task<int> GetCount();
}