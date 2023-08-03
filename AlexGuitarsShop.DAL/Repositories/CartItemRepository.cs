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

    public async Task<CartItem> FindAsync(int id)
    {
        EnsureCartNotNull();
        using IDbConnection db = new MySqlConnection(_connectionString);
        return await db.QueryFirstAsync<CartItem>($@"SELECT CartItems.*, Guitars.*
        FROM CartItems 
        LEFT JOIN Guitars ON CartItems.Prod_Id = Guitars.Id
        WHERE CartItems.Cart_Id = '{_cart!.Id}' 
        AND CartItems.Prod_Id = @Id AND Guitars.IsDeleted = 0 ", new {id})!;
    }

    public async Task<int> GetProductQuantityAsync(int id)
    {
        if (_cart == null) throw new ArgumentNullException(nameof(_cart));
        using IDbConnection db = new MySqlConnection(_connectionString);
        return await db.ExecuteScalarAsync<int>(@$"SELECT Quantity FROM CartItems 
        WHERE Cart_Id = '{_cart.Id}' AND Prod_Id = {id}")!;
    }

    public async Task<List<CartItem>> GetAllAsync()
    {
        EnsureCartNotNull();
        using IDbConnection db = new MySqlConnection(_connectionString);
        return (await db.QueryAsync<CartItem, Guitar, CartItem>(@$"SELECT CartItems.*, Guitars.*
        FROM CartItems 
        LEFT JOIN Guitars ON CartItems.Prod_Id = Guitars.Id
        WHERE CartItems.Cart_Id = '{_cart!.Id}' AND Guitars.IsDeleted = 0 ", (item, product) =>
        {

            item!.Product = product;
            return item;
        })!)!.ToList();
    }

    public async Task CreateAsync(CartItem item)
    {
        EnsureCartNotNull();
        item = item ?? throw new ArgumentNullException(nameof(item));
        item.Product = item.Product ?? throw new ArgumentNullException(nameof(item.Product));
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync(@"INSERT INTO CartItems (Cart_Id, Prod_Id, Quantity) 
                VALUES (@Cart_Id, @Prod_Id, @Quantity)",
            new
            {
                Cart_Id = _cart!.Id,
                Prod_Id = item.Product.Id,
                item.Quantity
            }
        )!;
    }
    
    public async Task UpdateQuantityAsync(int id, int quantity)
    {
        EnsureCartNotNull();
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync($@"UPDATE CartItems SET Quantity = {quantity}
        WHERE Cart_Id = {_cart!.Id} AND Prod_Id = {id}")!;
    }

    public async Task DeleteAsync(int id)
    {
        EnsureCartNotNull();
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync($"DELETE FROM CartItems WHERE Cart_ID = {_cart!.Id} AND Prod_Id = {id}")!;
    }

    public async Task DeleteAllAsync()
    {
        EnsureCartNotNull();
        using IDbConnection db = new MySqlConnection(_connectionString);
        await db.ExecuteAsync($@"DELETE FROM CartItems WHERE Cart_ID = {_cart!.Id}")!;
    }

    private void EnsureCartNotNull()
    {
        if (_cart == null)
        {
            throw new ArgumentNullException(nameof(_cart));
        }
    }
}