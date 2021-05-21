dotnet ef migrations add Grants_v5 -c PersistedGrantDbContext -o Migrations/PersistedGrantDb
dotnet ef migrations add Config_v5 -c ConfigurationDbContext -o Migrations/ConfigurationDb

dotnet ef migrations script -c PersistedGrantDbContext -o Migrations/PersistedGrantDb_v5.sql
dotnet ef migrations script -c ConfigurationDbContext -o Migrations/ConfigurationDb_v5.sql
