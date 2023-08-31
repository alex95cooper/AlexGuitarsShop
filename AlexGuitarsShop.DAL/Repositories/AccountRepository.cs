using System.Data;
using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using Dapper;
using MySqlConnector;

namespace AlexGuitarsShop.DAL.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly string _connectionString;

    public AccountRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<Account> FindAsync(string email)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return await db.QueryFirstOrDefaultAsync<Account>($"SELECT * FROM Account WHERE Email = @Email",
            new {Email = email});
    }

    public async Task<int> GetUsersCountAsync()
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Account WHERE Role = 0");
    }

    public async Task<int> GetAdminsCountAsync()
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Account WHERE Role = 1");
    }

    public async Task<List<Account>> GetUsersAsync(int offset, int limit)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return (await db.QueryAsync<Account>(@$"SELECT * FROM Account WHERE Role = 0 
        LIMIT {limit} OFFSET {offset}")).ToList();
    }

    public async Task<List<Account>> GetAdminsAsync(int offset, int limit)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return (await db.QueryAsync<Account>(@$"SELECT * FROM Account WHERE Role = 1 
        LIMIT {limit} OFFSET {offset}")).ToList();
    }

    public async Task CreateAsync(Account account)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync(@"INSERT INTO Account (Name, Email, Password, Role) 
            VALUES (@Name, @Email, @Password, @Role)",
            new {account.Name, account.Email, account.Password, account.Role});
    }

    public async Task UpdateAsync(string email, int role)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync($@"UPDATE Account SET Role = {role} WHERE Email = @Email",
            new {Email = email});
    }
}