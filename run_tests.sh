# clean the solution,
# restore dependencies, 
# build the project, 
# run all unit tests,
# run if all is well

cd ./RTWTR
dotnet clean
dotnet restore
dotnet build
dotnet test ./RTWTR.Tests
dotnet run -p ./RTWTR.MVC