using System.Data;
using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using Dapper;
using MySqlConnector;

namespace AlexGuitarsShop.DAL.Repositories;

public class CartItemRepository : ICartItemRepository
{
    private readonly string _connectionString;
    private readonly Cart _cart;

    public CartItemRepository(string connectionString, Cart cart)
    {
        _connectionString = connectionString;
        _cart = cart;
    }

    public async Task<CartItem> GetAsync(int id)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return await db.QueryFirstAsync<CartItem>(@"SELECT Cart_Id, Prod_Id, Guitars.Name, Guitars.Price, 
        Guitars.Image, Guitars.IsDeleted, Quantity 
        FROM CartItems
        INNER JOIN Guitars WHERE CartItems.Prod_Id = @Id
        AND Guitars.IsDeleted = 0", new {id})!;
    }

    public async Task<List<CartItem>> SelectAsync()
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return (await db.QueryAsync<CartItem, Guitar, CartItem>(@$"SELECT CartItems.*, Guitars.*
        FROM CartItems 
        LEFT JOIN Guitars ON CartItems.Prod_Id = Guitars.Id
        WHERE CartItems.Cart_Id = '{_cart!.Id}' AND Guitars.IsDeleted = 0 ", (item, product) =>
        {
            item!.Product = product!;
            return item;
        })!)!.ToList();
    }

    public async Task<List<CartItem>> SelectByLimitAsync(int offset, int limit)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return (await db.QueryAsync<CartItem, Guitar, CartItem>(@$"SELECT CartItems.*, Guitars.*
        FROM CartItems 
        LEFT JOIN Guitars ON CartItems.Prod_Id = Guitars.Id
        WHERE CartItems.Cart_Id = '{_cart!.Id}' AND Guitars.IsDeleted = 0 
        LIMIT {limit} OFFSET {offset}", (item, product) =>
        {
            item!.Product = product;
            return item;
        })!)!.ToList();
    }

    public async Task AddAsync(CartItem item)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync(@"INSERT INTO CartItems (Cart_Id, Prod_Id, Quantity) 
            VALUES (@Cart_Id, @Prod_Id, @Quantity)",
            new
            {
                Cart_Id = _cart!.Id,
                Prod_Id = item!.Product!.Id,
                item.Quantity
            }
        )!;
    }

    public async Task UpdateAsync(CartItem item)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync(@"UPDATE CartItems SET Quantity = @Quantity
        WHERE Cart_Id = @Cart_Id AND Prod_Id = @Prod_Id",
            new
            {
                Cart_Id = _cart!.Id,
                Prod_Id = item!.Product!.Id,
                item.Quantity
            }
        )!;
    }

    public async Task DeleteAsync(int id)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync($@"DELETE FROM CartItems WHERE Cart_ID = @Cart_ID AND Prod_Id = @Prod_Id",
            new {Cart_Id = _cart!.Id, Prod_Id = id})!;
    }

    public async Task DeleteAllAsync()
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync($@"DELETE FROM CartItems WHERE Cart_ID = @Cart_ID",
            new {Cart_Id = _cart!.Id})!;
    }

    public async Task<int> GetProductQuantityAsync(int id)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return await db.ExecuteScalarAsync<int>($"SELECT Quantity FROM CartItems WHERE Cart_Id = '{_cart!.Id}'")!;
    }

    public async Task ChangeQuantityAsync(int id, int quantity)
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync($@"UPDATE CartItems SET Quantity = {quantity}
        WHERE Cart_Id = @Cart_Id AND Prod_Id = {id}", new {Cart_Id = _cart!.Id})!;
    }

    public async Task<int> GetCountAsync()
    {
        using IDbConnection db = new MySqlConnection(_connectionString);
        return await db.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM CartItems WHERE Cart_Id = '{_cart!.Id}'")!;
    }
}