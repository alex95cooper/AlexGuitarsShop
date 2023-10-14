using AlexGuitarsShop.DAL;
using AlexGuitarsShop.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace AlexGuitarsShop.Scripts;

public static class SeedDatabase
{
    private static AlexGuitarsShopDbContext _db;

    public static async void InitAsync(DbContextOptions<AlexGuitarsShopDbContext> options)
    {
        _db = new AlexGuitarsShopDbContext(options);
        ModelBuilder builder = new ModelBuilder();
        await _db.Database.EnsureDeletedAsync();
        await _db.Database.EnsureCreatedAsync();
        InitGuitars(builder);
        InitAccounts(builder);
        InitCartItems(builder);
        await FillTables();
    }

    private static void InitGuitars(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Guitar>(builder =>
        {
            builder.ToTable("Guitar").HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(600).IsRequired();
        });
    }

    private static void InitAccounts(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(builder =>
        {
            builder.ToTable("Account").HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasMaxLength(15).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Role).IsRequired();
        });
    }

    private static void InitCartItems(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CartItem>(builder =>
        {
            builder.ToTable("CartItem").HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Quantity).IsRequired();
        });

        modelBuilder.Entity<CartItem>()
            .HasOne(cartItem => cartItem.Product)
            .WithMany(guitar => guitar.CartItems)
            .HasForeignKey(cartItem => cartItem.ProductId);

        modelBuilder.Entity<CartItem>()
            .HasOne(cartItem => cartItem.Account)
            .WithMany(account => account.CartItems)
            .HasForeignKey(cartItem => cartItem.AccountId);
    }

    private static async Task FillTables()
    {
        _db.Guitar.AddRange(await DataFiller.GetGuitarsAsync());
        _db.Account.AddRange(DataFiller.GetAccounts());
        await _db.SaveChangesAsync();
    }
}