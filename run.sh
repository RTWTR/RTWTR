cd ./RTWTR
# clean the solution,
dotnet clean

# restore dependencies,
dotnet restore

# build and then
dotnet build

# run the project
dotnet run -p ./RTWTR.MVC
