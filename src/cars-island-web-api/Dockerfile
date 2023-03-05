FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CarsIsland.API/CarsIsland.API.csproj", "CarsIsland.API/"]
COPY ["CarsIsland.Core/CarsIsland.Core.csproj", "CarsIsland.Core/"]
COPY ["CarsIsland.Infrastructure/CarsIsland.Infrastructure.csproj", "CarsIsland.Infrastructure/"]
RUN dotnet restore "CarsIsland.API/CarsIsland.API.csproj"
COPY . .
WORKDIR "/src/CarsIsland.API"
RUN dotnet build "CarsIsland.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarsIsland.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarsIsland.API.dll"]