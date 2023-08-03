using AlexGuitarsShop.DAL.Models;

namespace AlexGuitarsShop.DAL.Interfaces;

public interface IGuitarRepository
{
    Task<Guitar> GetAsync(int id);
    
    Task<int> GetCountAsync();
    
    Task<List<Guitar>> GetAllAsync(int offset, int limit);
    
    Task AddAsync(Guitar guitar);
    
    Task UpdateAsync(Guitar guitar);
    
    Task DeleteAsync(int id);
    
    
}