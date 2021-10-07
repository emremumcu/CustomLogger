# Commands

PM> Install-Package Microsoft.EntityFrameworkCore.SQLite -ProjectName AppLib
PM> Install-Package Microsoft.EntityFrameworkCore.Tools -ProjectName AppLib
CustomLogger\AppLib> dotnet ef migrations add Mig0 -o EFCore/Migrations
CustomLogger\AppLib>dotnet ef database update [Mig0] 