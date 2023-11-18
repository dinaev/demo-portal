FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DemoPortal.Backend.GateWay.Web/DemoPortal.Backend.GateWay.Web.csproj", "DemoPortal.Backend.GateWay.Web/"]
RUN dotnet restore "DemoPortal.Backend.GateWay.Web/DemoPortal.Backend.GateWay.Web.csproj"
COPY . .
WORKDIR "/src/DemoPortal.Backend.GateWay.Web"
RUN dotnet build "DemoPortal.Backend.GateWay.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DemoPortal.Backend.GateWay.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DemoPortal.Backend.GateWay.Web.dll"]
