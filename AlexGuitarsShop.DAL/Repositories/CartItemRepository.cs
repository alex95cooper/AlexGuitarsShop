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

    public async Task<CartItem> FindAsync(int id, string cartId)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        CartItem item = await db.QueryFirstOrDefaultAsync<CartItem>($@"SELECT CartItems.*, Guitars.*
        FROM CartItems 
        LEFT JOIN Guitars ON CartItems.Prod_Id = Guitars.Id
        WHERE CartItems.Cart_Id = '{cartId}' 
        AND CartItems.Prod_Id = @Id AND Guitars.IsDeleted = 0 ", new {id});
        return item;
    }

    public async Task<int> GetProductQuantityAsync(int id, string cartId)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return await db.ExecuteScalarAsync<int>(@$"SELECT Quantity FROM CartItems 
        WHERE Cart_Id = '{cartId}' AND Prod_Id = {id}");
    }

    public async Task<List<CartItem>> GetAllAsync(string cartId)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return (await db.QueryAsync<CartItem, Guitar, CartItem>(@$"SELECT CartItems.*, Guitars.*
        FROM CartItems 
        LEFT JOIN Guitars ON CartItems.Prod_Id = Guitars.Id
        WHERE CartItems.Cart_Id = '{cartId}' AND Guitars.IsDeleted = 0 ", (item, product) =>
        {
            item.Product = product;
            return item;
        })).ToList();
    }

    public async Task CreateAsync(CartItem item, string cartId)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync(@"INSERT INTO CartItems (Cart_Id, Prod_Id, Quantity) 
                VALUES (@Cart_Id, @Prod_Id, @Quantity)",
            new
            {
                Cart_Id = cartId,
                Prod_Id = item.Product.Id,
                item.Quantity
            }
        );
    }

    public async Task UpdateQuantityAsync(int id, int quantity, string cartId)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync($@"UPDATE CartItems SET Quantity = {quantity}
        WHERE Cart_Id = '{cartId}' AND Prod_Id = {id}");
    }

    public async Task DeleteAsync(int id, string cartId)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync($"DELETE FROM CartItems WHERE Cart_ID = '{cartId}' AND Prod_Id = {id}");
    }

    public async Task DeleteAllAsync(string cartId)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync($@"DELETE FROM CartItems WHERE Cart_ID = '{cartId}'");
    }
}