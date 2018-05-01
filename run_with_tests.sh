cd ./RTWTR
# clean the solution,
dotnet clean

# restore dependencies, 
dotnet restore

# build the project, 
dotnet build

# run all unit tests,
dotnet test ./RTWTR.Tests

# run if all is well
dotnet run -p ./RTWTR.MVC