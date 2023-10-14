using AlexGuitarsShop.DAL;
using AlexGuitarsShop.Scripts;
using Microsoft.EntityFrameworkCore;

IConfigurator configurator = new Configurator();
string connectionString = configurator.GetConnectionString();
var optionsBuilder = new DbContextOptionsBuilder<AlexGuitarsShopDbContext>();
optionsBuilder.UseMySQL(connectionString);
SeedDatabase.InitAsync(optionsBuilder.Options);