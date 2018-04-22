cd ./RTWTR
# clean the solution,
dotnet clean

# restore dependencies,
dotnet restore

# build the solution,
dotnet build

cd ./RTWTR.Tests
# run tests
dotnet test