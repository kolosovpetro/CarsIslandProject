FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CarsIsland.WebApp/CarsIsland.WebApp.csproj", "CarsIsland.WebApp/"]
RUN dotnet restore "CarsIsland.WebApp/CarsIsland.WebApp.csproj"
COPY . .
WORKDIR "/src/CarsIsland.WebApp"
RUN dotnet build "CarsIsland.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarsIsland.WebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarsIsland.WebApp.dll"]