# Використовуємо офіційний образ .NET SDK як базовий
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# Встановлюємо необхідні інструменти для побудови проекту
RUN apt-get update && \
    apt-get install -y apt-transport-https && \
    apt-get install -y --no-install-recommends \
        libc6-dev \
        libgdiplus \
        libx11-dev \
        libfontconfig1 \
        && rm -rf /var/lib/apt/lists/*

# Створюємо робочу директорію
WORKDIR /app

# Копіюємо файл проекту та відновлюємо залежності
COPY *.csproj ./
RUN dotnet restore

# Встановлення необхідних бібліотек через NuGet
RUN dotnet add package SixLabors.ImageSharp --version 3.0.0
RUN dotnet add package SkiaSharp --version 2.88.3
# Для платних бібліотек потрібні окремі ліцензії та залежності, їх необхідно додати у проект вручну.

# Копіюємо всі файли проекту та збираємо додаток
COPY . ./
RUN dotnet publish -c Release -o out

# Використовуємо офіційний .NET runtime як кінцевий образ
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .

# Вказуємо команду запуску контейнера
ENTRYPOINT ["dotnet", "MyApp.dll"]
