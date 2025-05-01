FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Server/MovieRental.API/MovieRental.API.csproj", "Server/MovieRental.API/"]
COPY ["Server/MovieRental.Application/MovieRental.Application.csproj", "Server/MovieRental.Application/"]
COPY ["Server/MovieRental.Domain/MovieRental.Domain.csproj", "Server/MovieRental.Domain/"]
COPY ["Server/MovieRental.Infrastructure/MovieRental.Infrastructure.csproj", "Server/MovieRental.Infrastructure/"]
RUN dotnet restore "./Server/MovieRental.API/MovieRental.API.csproj"
COPY . .
WORKDIR "/src/Server/MovieRental.API"
RUN dotnet build "./MovieRental.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MovieRental.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MovieRental.API.dll"]