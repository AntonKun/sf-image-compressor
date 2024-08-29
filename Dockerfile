# Використовуємо офіційний образ .NET SDK як базовий
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# Встановлюємо необхідні інструменти для побудови проекту
RUN apt-get update && \
    apt-get install -y apt-transport-https && \
    apt-get install -y --no-install-recommends \
        libc6-dev \
        libgdiplus \
        libx11-dev \
        libglib2.0-0 \
        libfontconfig1 \
        && rm -rf /var/lib/apt/lists/*

# Створюємо робочу директорію
WORKDIR /app

# Копіюємо файл проекту та відновлюємо залежності
COPY *.csproj ./
RUN dotnet restore

# Встановлення необхідних бібліотек через NuGet
RUN dotnet add package SixLabors.ImageSharp --version 3.0.0
RUN dotnet add package Magick.NET-Q8-AnyCPU
RUN dotnet add package Aspose.Imaging --version 23.8.0
# Для платних бібліотек потрібні окремі ліцензії та залежності, їх необхідно додати у проект вручну.

# Копіюємо всі файли проекту та збираємо додаток
COPY . ./
RUN dotnet publish -c Release -o out

# Використовуємо офіційний .NET runtime як кінцевий образ
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .

# Копіюємо необхідні нативні бібліотеки
COPY --from=build-env /root/.nuget/packages/ /root/.nuget/packages/

# Вказуємо команду запуску контейнера
ENTRYPOINT ["dotnet", "MyApp.dll"]
