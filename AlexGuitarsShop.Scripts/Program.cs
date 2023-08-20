using AlexGuitarsShop.Scripts;

IConfigurator configurator = new Configurator();
string connectionString = configurator.GetConnectionString();
SeedDatabase.Init(connectionString);

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Tables are created successfully!");
Console.ReadKey();