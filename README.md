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

### Creating the additional projects

dotnet new classlib -n Core


dotnet new classlib -n Infrastructure

dotnet sln add Core

cd API
dotnet add reference ../Infrastructure

cd ..
cd Infrastructure
dotnet add reference ../Core

### Basket
installed stackExchanged.redis nuget package
setup docker to run Redis in the localhost

Goal:
To set up and configure Redis to store the customer basket in server memory and create the supporting repository and controller

### Identity
    - setting up ASP.NET Identity
        * package microsoft.AspNetCore.identity.entityframeworkcore
                microsoft.AspNetCore.identity
                 microsoft.IdentityModel.Tokens
                 System.IdentityModel.Token.Jwt
    - Context boundaries
    - Using the UserManager & signInManager
    - Extension methods
    - JWT Tokens

# Goal:
    To implement ASP.NET identity to allow clients to login and register to our app and receive a JWT Token which can be used to authenticate against certain classes/methods in the API


    # To run identity migration 

    dotnet ef migrations add IdentityInitial -p Infrastructure -s API -c AppIdentityDbContext -o Identity -o Identity/Migrations

    ### Validation
        Goal: 
        To add validation attributes to data we receive from the client to ensure our API does not accept bad data and returns the modelState errors to the client