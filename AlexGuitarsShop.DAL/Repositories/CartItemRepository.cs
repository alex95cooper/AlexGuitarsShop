using System.Data;
using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using Dapper;
using MySqlConnector;

namespace AlexGuitarsShop.DAL.Repositories;

public class CartItemRepository : ICartItemRepository
{
    private readonly string _connectionString;

    public CartItemRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<CartItem> FindAsync(int id, int accountId)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        CartItem item = await db.QueryFirstOrDefaultAsync<CartItem>($@"SELECT CartItem.*, Guitar.*
        FROM CartItem 
        LEFT JOIN Guitar ON CartItem.ProductId = Guitar.Id
        WHERE CartItem.AccountId = '{accountId}' 
        AND CartItem.ProductId = @Id AND Guitar.IsDeleted = 0 ", new {id});
        return item;
    }

    public async Task<int> GetProductQuantityAsync(int id, int accountId)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return await db.ExecuteScalarAsync<int>(@$"SELECT Quantity FROM CartItem 
        WHERE AccountId = '{accountId}' AND ProductId = {id}");
    }

    public async Task<List<CartItem>> GetAllAsync(int accountId)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return (await db.QueryAsync<CartItem, Guitar, CartItem>(@$"SELECT CartItem.*, Guitar.*
        FROM CartItem 
        LEFT JOIN Guitar ON CartItem.ProductId = Guitar.Id
        WHERE CartItem.AccountId = '{accountId}' AND Guitar.IsDeleted = 0 ", (item, product) =>
        {
            item.Product = product;
            return item;
        })).ToList();
    }

    public async Task CreateAsync(CartItem item, int accountId)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync(@"INSERT INTO CartItem (AccountId, ProductId, Quantity) 
                VALUES (@AccountId, @ProdId, @Quantity)",
            new
            {
                AccountId = accountId,
                ProdId = item.Product.Id,
                item.Quantity
            }
        );
    }

    public async Task UpdateQuantityAsync(int id, int accountId, int quantity)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync($@"UPDATE CartItem SET Quantity = {quantity}
        WHERE AccountId = '{accountId}' AND ProductId = {id}");
    }

    public async Task DeleteAsync(int id, int accountId)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync($"DELETE FROM CartItem WHERE AccountId = '{accountId}' AND ProductId = {id}");
    }

    public async Task DeleteAllAsync(int accountId)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync($@"DELETE FROM CartItem WHERE AccountId = '{accountId}'");
    }
}