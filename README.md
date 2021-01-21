# FigureApp
FigureApp is a simple web service with WebApi that stores and calculates the area for geometrical figures. Sqlite is used as the DB storage.
## Getting started
1. Ensure that .NET Core 3.1 SDK is installed.
2. Execute `dotnet build` to build the solution.
3. To make database preparations, execute

    `dotnet tool install --global dotnet-ef`
    
    `dotnet ef migrations add InitialCreate --project .\Figure.Db\`
    
    `dotnet ef database update --project .\Figure.Db\`
4. To run the application use `dotnet run`. 

## Limitations

1. The geometrical figures are defined using the simplest way:
- circle is defined by radius;
- triangle is defined by three sides.
2. ConnectionString is a hardcoded constant.
3. Console output logging is only available.
4. No minimum length constraint on a column that stores figure params.
5. {id} for saved figures is incremental.

# Docs
1. Postman collection is [here](Docs/FigureApp.postman_collection.json).
2. Available methods are described [here](Docs/Api.md).
