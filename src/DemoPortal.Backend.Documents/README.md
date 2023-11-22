# Introduction 
This microservice provides functionality to work with the documents.

# Database
## setup entity framework
Need to specify version 

```shell
dotnet tool install --global dotnet-ef --version 7.0.14
```

## add migration

```shell
dotnet ef migrations add InitialMigration --project DemoPortal.Backend.Documents.DataAccess.Sql --startup-project DemoPortal.Backend.Documents.Api
```

## update database to last migration

```shell
dotnet ef database update --project DemoPortal.Backend.Documents.DataAccess.Sql --startup-project DemoPortal.Backend.Documents.Api
```

## update database to specified migration

```shell
dotnet ef database update MigrationName --project DemoPortal.Backend.Documents.DataAccess.Sql --startup-project DemoPortal.Backend.Documents.Api
```

## update database to remove all migrations

```shell
dotnet ef database update 0 --project DemoPortal.Backend.Documents.DataAccess.Sql --startup-project DemoPortal.Backend.Documents.Api
```

## remove last migration
```shell
dotnet ef migrations remove --project DemoPortal.Backend.Documents.DataAccess.Sql --startup-project DemoPortal.Backend.Documents.Api
```

# API Client
## setup refit generator

```shell
dotnet tool install --global Refitter
```

## generate a refit interface
```shell
refitter http://localhost:5244/swagger/v1/swagger.json --namespace "DemoPortal.Backend.Documents.Api.Client" --output ./DemoPortal.Backend.Documents.Api.Client/IDocumentsApi.cs --no-accept-headers --interface-only
```