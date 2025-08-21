FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Копируем ВСЕ .csproj файлы в соответствующие папки внутри контейнера
COPY ["CRUDproject.API/CRUDproject.API.csproj", "CRUDproject.API/"]
COPY ["CRUDproject.Database/CRUDproject.Database.csproj", "CRUDproject.Database/"]
COPY ["CRUDproject.Domain/CRUDproject.Domain.csproj", "CRUDproject.Domain/"]

# Восстанавливаем зависимости основного проекта
RUN dotnet restore "CRUDproject.API/CRUDproject.API.csproj"

# Копируем ВЕСЬ исходный код
COPY . .

# Сборка
WORKDIR "/src/CRUDproject.API"
RUN dotnet build "CRUDproject.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CRUDproject.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CRUDproject.API.dll"]
ENTRYPOINT ["dotnet", "CRUDproject.API.dll"]
