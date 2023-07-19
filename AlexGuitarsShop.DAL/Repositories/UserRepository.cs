using System.Data;
using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.Domain.Models;
using Dapper;
using MySqlConnector;

namespace AlexGuitarsShop.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<User> Get(int id)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return await db.QueryFirstOrDefaultAsync<User>($"SELECT * FROM Users WHERE Id = {id}");
    }

    public async Task<User> GetUserByEmail(string email)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return await db.QueryFirstOrDefaultAsync<User>($"SELECT * FROM Users WHERE Email = @Email",
            new {Email = email});
    }

    public async Task Add(User user)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync(@"INSERT INTO Users (Name, Email, Password, Role) 
        VALUES (@Name, @Email, @Password, @Role)",
            new {user.Name, user.Email, user.Password, user.Role});
    }

    public async Task<List<User>> Select()
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return (await db.QueryAsync<User>(@$"SELECT * FROM Users")).ToList();
    }

    public async Task<List<User>> SelectByLimit(int offset, int limit)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return (await db.QueryAsync<User>(@$"SELECT * FROM Users 
        LIMIT {limit} OFFSET {offset}")).ToList();
    }

    public async Task Update(User entity)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(int id)
    {
        throw  new NotImplementedException();
    }

    public async Task<int> GetCount()
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return await db.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM Users");
    }
    
    public async Task<int> GetUsersCount()
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return await db.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM Users WHERE Role = 0");
    }

    public async Task<int> GetAdminsCount()
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return await db.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM Users WHERE Role = 1");
    }

    public async Task<List<User>> GetUsersByLimit(int offset, int limit)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return (await db.QueryAsync<User>(@$"SELECT * FROM Users WHERE Role = 0 
        LIMIT {limit} OFFSET {offset}")).ToList();
    }

    public async Task<List<User>> GetAdminsByLimit(int offset, int limit)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return (await db.QueryAsync<User>(@$"SELECT * FROM Users WHERE Role = 1 
        LIMIT {limit} OFFSET {offset}")).ToList();
    }

    public async Task SetAdminRights(string email)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync($@"UPDATE Users SET Role = 1 WHERE Email = @Email",
            new{Email = email});
    }

    public async Task RemoveAdminRights(string email)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync($@"UPDATE Users SET Role = 0 WHERE Email = @Email",
            new{Email = email});
    }
}