FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DemoPortal.Backend.GateWay.Api/DemoPortal.Backend.GateWay.Api.csproj", "DemoPortal.Backend.GateWay.Api/"]
RUN dotnet restore "DemoPortal.Backend.GateWay.Api/DemoPortal.Backend.GateWay.Api.csproj"
COPY . .
WORKDIR "/src/DemoPortal.Backend.GateWay.Api"
RUN dotnet build "DemoPortal.Backend.GateWay.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DemoPortal.Backend.GateWay.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DemoPortal.Backend.GateWay.Api.dll"]
