# FigureApp
FigureApp is a simple web service with WebApi that stores and calculates area for geometrical figures.
## Getting started
1. Ensure that .NET Core 3.1 SDK is installed
2. Execute `dotnet build` to build the solution
3. To make database preparations, please execute

    `dotnet tool install --global dotnet-ef`
    
    `dotnet ef migrations add InitialCreate --project .\Figure.Db\`
    
    `dotnet ef database update --project .\Figure.Db\`
4. To run the application use `dotnet run` 
