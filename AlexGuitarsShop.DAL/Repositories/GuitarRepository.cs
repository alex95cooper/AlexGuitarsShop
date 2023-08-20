using System.Data;
using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using Dapper;
using MySqlConnector;

namespace AlexGuitarsShop.DAL.Repositories;

public class GuitarRepository : IGuitarRepository
{
    private readonly string _connectionString;

    public GuitarRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<Guitar> GetAsync(int id)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return await db.QueryFirstOrDefaultAsync<Guitar>($"SELECT * FROM Guitars WHERE Id = {id}");
    }
    
    public async Task<int> GetCountAsync()
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Guitars WHERE IsDeleted = 0");
    }
    
    public async Task<List<Guitar>> GetAllAsync(int offset, int limit)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return (await db.QueryAsync<Guitar>(@$"SELECT * FROM Guitars WHERE IsDeleted = 0 
        LIMIT {limit} OFFSET {offset}")).ToList();
    }

    public async Task AddAsync(Guitar guitar)
    {
        guitar = guitar ?? throw new ArgumentNullException(nameof(guitar));
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync(@"INSERT INTO Guitars (Name, Price, Description, Image, IsDeleted) 
        VALUES (@Name, @Price, @Description, @Image, 0)",
            new {guitar.Name, guitar.Price, guitar.Description, guitar.Image});
    }

    public async Task UpdateAsync(Guitar guitar)
    {
        guitar = guitar ?? throw new ArgumentNullException(nameof(guitar));
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync(@"UPDATE Guitars SET Name = @Name, Price = @Price, 
                   Description = @Description, Image = @Image WHERE Id = @ID",
            new {guitar.Name, guitar.Price, guitar.Description, guitar.Image, guitar.Id});
    }

    public async Task DeleteAsync(int id)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync($@"UPDATE Guitars SET IsDeleted = 1 WHERE Id = {id}");
    }
}