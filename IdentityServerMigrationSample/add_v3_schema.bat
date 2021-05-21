rmdir /S /Q Migrations

dotnet ef migrations add Grants_v3 -c PersistedGrantDbContext -o Migrations/PersistedGrantDb
dotnet ef migrations add Config_v3 -c ConfigurationDbContext -o Migrations/ConfigurationDb

dotnet ef migrations script -c PersistedGrantDbContext -o Migrations/PersistedGrantDb_v3.sql
dotnet ef migrations script -c ConfigurationDbContext -o Migrations/ConfigurationDb_v3.sql
