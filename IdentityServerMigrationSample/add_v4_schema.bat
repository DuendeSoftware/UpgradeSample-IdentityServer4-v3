dotnet ef migrations add Grants_v4 -c PersistedGrantDbContext -o Migrations/PersistedGrantDb
dotnet ef migrations add Config_v4 -c ConfigurationDbContext -o Migrations/ConfigurationDb

dotnet ef migrations script -c PersistedGrantDbContext -o Migrations/PersistedGrantDb_v4.sql
dotnet ef migrations script -c ConfigurationDbContext -o Migrations/ConfigurationDb_v4.sql
