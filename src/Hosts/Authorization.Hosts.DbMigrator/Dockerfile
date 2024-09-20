FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Hosts/Authorization.Hosts.DbMigrator/Authorization.Hosts.DbMigrator.csproj", "src/Hosts/Authorization.Hosts.DbMigrator/"]
COPY ["src/Infrastructure/Authorization.Infrastructure.DataAccess/Authorization.Infrastructure.DataAccess.csproj", "src/Infrastructure/Authorization.Infrastructure.DataAccess/"]
COPY ["src/Application/Authorization.Application.AppServices/Authorization.Application.AppServices.csproj", "src/Application/Authorization.Application.AppServices/"]
COPY ["src/Contracts/Authorization.Contracts/Authorization.Contracts.csproj", "src/Contracts/Authorization.Contracts/"]
COPY ["src/Domain/Authorization.Domain/Authorization.Domain.csproj", "src/Domain/Authorization.Domain/"]
COPY ["src/Infrastructure/Authorization.Infrastructure/Authorization.Infrastructure.csproj", "src/Infrastructure/Authorization.Infrastructure/"]
RUN dotnet restore "src/Hosts/Authorization.Hosts.DbMigrator/Authorization.Hosts.DbMigrator.csproj"
COPY . .
WORKDIR "/src/src/Hosts/Authorization.Hosts.DbMigrator"
RUN dotnet build "Authorization.Hosts.DbMigrator.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Authorization.Hosts.DbMigrator.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Authorization.Hosts.DbMigrator.dll"]
