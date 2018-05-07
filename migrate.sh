# since I am very lazy :D

# Go to the folder with DbContext
cd ./RTWTR/RTWTR.Data

# Remove the Migrations
rm -r ./Migrations

# Drop the Base
dotnet ef database drop

# Give me a new migration
dotnet ef migrations add Initial