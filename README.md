# ecommerce-app-api-backend


### setting up entity framework core
nuget package installed
microsoft.entityframeworkcore.sqlite
microsoft.entityframeworkcore.design


### adding an entity framework migration
dotnet new tool-manifest # if you are setting up this repo
dotnet tool install --local dotnet-ef --version 7.0.16

dotnet ef migrations add InitialCreate -o Data/Migrations

### updating the database
dotnet ef database update

