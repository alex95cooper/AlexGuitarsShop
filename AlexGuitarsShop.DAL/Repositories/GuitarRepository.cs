using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace AlexGuitarsShop.DAL.Repositories;

public class GuitarRepository : IGuitarRepository
{
    private readonly AlexGuitarsShopDbContext _db;

    public GuitarRepository(AlexGuitarsShopDbContext db)
    {
        _db = db;
    }

    public async Task<Guitar> GetAsync(int id)
    {
        return await _db.Guitar.FirstOrDefaultAsync(guitar => guitar.Id == id);
    }

    public async Task<int> GetCountAsync()
    {
        return await _db.Guitar.CountAsync(guitar => guitar.IsDeleted == 0);
    }

    public async Task<List<Guitar>> GetAllAsync(int offset, int limit)
    {
        return await _db.Guitar
            .Where(guitar => guitar.IsDeleted == 0)
            .Skip(offset).Take(limit).ToListAsync();
    }

    public async Task AddAsync(Guitar guitar)
    {
        await _db.Guitar.AddAsync(guitar);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Guitar guitar)
    {
        Guitar guitarToUpdate = await _db.Guitar.FirstOrDefaultAsync(x => x.Id == guitar.Id);
        if (guitarToUpdate != null)
        {
            guitarToUpdate.Name = guitar.Name;
            guitarToUpdate.Price = guitar.Price;
            guitarToUpdate.Image = guitar.Image;
            guitarToUpdate.Description = guitar.Description;
            _db.Guitar.Update(guitarToUpdate);
            await _db.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        Guitar guitar = await _db.Guitar.FirstOrDefaultAsync(guitar => guitar.Id == id);
        if (guitar != null)
        {
            guitar.IsDeleted = 1;
            await _db.SaveChangesAsync();
        }
    }
}