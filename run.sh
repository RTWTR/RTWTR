# clean the solution,
# restore dependencies,
# build and then
# run the project

cd ./RTWTR
dotnet clean
dotnet restore
dotnet build
dotnet run -p ./RTWTR.MVC