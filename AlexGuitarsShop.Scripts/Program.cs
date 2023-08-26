using AlexGuitarsShop.Scripts;

IConfigurator configurator = new Configurator();
string connectionString = configurator.GetConnectionString();
SeedDatabase.Init(connectionString);