FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8088
EXPOSE 8089

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/BS.API/BS.API.csproj", "src/BS.API/"]
COPY ["src/BS.Application/BS.Application.csproj", "src/BS.Application/"]
COPY ["src/BS.Domain/BS.Domain.csproj", "src/BS.Domain/"]
COPY ["src/BS.Infrastructure/BS.Infrastructure.csproj", "src/BS.Infrastructure/"]
RUN dotnet restore "src/BS.API/BS.API.csproj"
COPY . .
WORKDIR "/src/src/BS.API"
RUN dotnet build "./BS.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BS.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BS.API.dll"]
