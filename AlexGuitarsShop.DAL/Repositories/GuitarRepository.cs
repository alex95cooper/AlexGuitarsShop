using System.Data;
using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.Domain.Models;
using Dapper;
using MySqlConnector;

namespace AlexGuitarsShop.DAL.Repositories;

public class GuitarRepository : IRepository<Guitar>
{
    private readonly string _connectionString;

    public GuitarRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<Guitar> Get(int id)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return await db.QueryFirstOrDefaultAsync<Guitar>($"SELECT * FROM guitars WHERE Id = {id}");
    }

    public async Task<List<Guitar>> Select()
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return (await db.QueryAsync<Guitar>(@$"SELECT * FROM guitars WHERE IsDeleted = 0 ")).ToList();
    }


    public async Task<List<Guitar>> SelectByLimit(int offset, int limit)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return (await db.QueryAsync<Guitar>(@$"SELECT * FROM guitars WHERE IsDeleted = 0 
        LIMIT {limit} OFFSET {offset}")).ToList();
    }

    public async Task Add(Guitar guitar)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync(@"INSERT INTO Guitars (Name, Price, Description, Image, IsDeleted) 
        VALUES (@Name, @Price, @Description, @Image, 0)",
            new {guitar.Name, guitar.Price, guitar.Description, guitar.Image});
    }

    public async Task Update(Guitar guitar)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync(@"UPDATE Guitars SET Name = @Name, Price = @Price, 
                   Description = @Description, Image = @Image WHERE Id = @ID",
            new {guitar.Name, guitar.Price, guitar.Description, guitar.Image, guitar.Id});
    }

    public async Task Delete(int id)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync($@"UPDATE Guitars SET IsDeleted = 1 WHERE Id = {id}");
    }

    public async Task<int> GetCount()
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return await db.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM Guitars WHERE IsDeleted = 0");
    }
}