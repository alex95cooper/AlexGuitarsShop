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
    
    public async Task<User> FindAsync(string email)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return await db.QueryFirstOrDefaultAsync<User>($"SELECT * FROM Users WHERE Email = @Email",
            new {Email = email})!;
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
    
    public async Task<List<User>> GetUsersAsync(int offset, int limit)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return (await db.QueryAsync<User>(@$"SELECT * FROM Users WHERE Role = 0 
        LIMIT {limit} OFFSET {offset}")!)!.ToList();
    }

    public async Task<List<User>> GetAdminsAsync(int offset, int limit)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return (await db.QueryAsync<User>(@$"SELECT * FROM Users WHERE Role = 1 
        LIMIT {limit} OFFSET {offset}")!)!.ToList();
    }

    public async Task CreateAsync(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync(@"INSERT INTO Users (Name, Email, Password, Role) 
            VALUES (@Name, @Email, @Password, @Role)", 
            new {user.Name, user.Email, user.Password, user.Role})!;
    }
    
    public async Task UpdateAsync(string email, int role)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync($@"UPDATE Users SET Role = {role} WHERE Email = @Email",
            new{Email = email})!;
    }
}