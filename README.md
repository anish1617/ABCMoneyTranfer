# ABCMoneyTranfer

## dotnet efcore command
- make sure to change appSettings.Json ConnectionString properties for database

- create a database named ABCMoneyTransfer

### Run these commands: 

- dotnet tool install --global dotnet-ef

- dotnet-ef migrations add InitialCreate

- dotnet-ef database update