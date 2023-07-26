using System.Data;
using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
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

    public async Task<User> GetAsync(int id)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return await db.QueryFirstOrDefaultAsync<User>($"SELECT * FROM Users WHERE Id = {id}")!;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return await db.QueryFirstOrDefaultAsync<User>($"SELECT * FROM Users WHERE Email = @Email",
            new {Email = email})!;
    }

    public async Task AddAsync(User user)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync(@"INSERT INTO Users (Name, Email, Password, Role) 
        VALUES (@Name, @Email, @Password, @Role)",
            new {user!.Name, user.Email, user.Password, user.Role})!;
    }

    public async Task<List<User>> SelectAsync()
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return (await db.QueryAsync<User>(@$"SELECT * FROM Users")!)!.ToList();
    }

    public async Task<List<User>> SelectByLimitAsync(int offset, int limit)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return (await db.QueryAsync<User>(@$"SELECT * FROM Users 
        LIMIT {limit} OFFSET {offset}")!)!.ToList();
    }

    public async Task UpdateAsync(User user)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync($@"UPDATE Users SET Role = @Role WHERE Email = @Email",
            new{user!.Role, user.Email})!;
    }

    public async Task DeleteAsync(int id)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync($@"DELETE FROM Users WHERE Id = @id", new{id})!;
    }

    public async Task<int> GetCountAsync()
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Users")!;
    }
    
    public async Task<int> GetUsersCountAsync()
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Users WHERE Role = 0")!;
    }

    public async Task<int> GetAdminsCountAsync()
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Users WHERE Role = 1")!;
    }

    public async Task<List<User>> GetUsersByLimitAsync(int offset, int limit)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return (await db.QueryAsync<User>(@$"SELECT * FROM Users WHERE Role = 0 
        LIMIT {limit} OFFSET {offset}")!)!.ToList();
    }

    public async Task<List<User>> GetAdminsByLimitAsync(int offset, int limit)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return (await db.QueryAsync<User>(@$"SELECT * FROM Users WHERE Role = 1 
        LIMIT {limit} OFFSET {offset}")!)!.ToList();
    }

    public async Task SetAdminRightsAsync(string email)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync($@"UPDATE Users SET Role = 1 WHERE Email = @Email",
            new{Email = email})!;
    }

    public async Task RemoveAdminRightsAsync(string email)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync($@"UPDATE Users SET Role = 0 WHERE Email = @Email",
            new{Email = email})!;
    }
}