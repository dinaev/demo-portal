# Introduction 
This microservice provides functionality to work with the documents.

# Database
## setup entity framework
Need to specify version 

```console
dotnet tool install --global dotnet-ef --version 7.0.14
```

## add migration

```console
dotnet ef migrations add InitialMigration --project DemoPortal.Backend.Documents.DataAccess.Sql --startup-project DemoPortal.Backend.Documents.Api
```

## update database to last migration

```console
dotnet ef database update --project DemoPortal.Backend.Documents.DataAccess.Sql --startup-project DemoPortal.Backend.Documents.Api
```

## update database to specified migration

```console
dotnet ef database update MigrationName --project DemoPortal.Backend.Documents.DataAccess.Sql --startup-project DemoPortal.Backend.Documents.Api
```

## update database to remove all migrations

```console
dotnet ef database update 0 --project DemoPortal.Backend.Documents.DataAccess.Sql --startup-project DemoPortal.Backend.Documents.Api
```

## remove last migration
```console
dotnet ef migrations remove --project DemoPortal.Backend.Documents.DataAccess.Sql --startup-project DemoPortal.Backend.Documents.Api
```