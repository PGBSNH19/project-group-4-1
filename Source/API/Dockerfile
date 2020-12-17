#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

WORKDIR /src

COPY ["API/API.csproj" "NearbyProduceBackend/"]
RUN dotnet restore ./API

COPY . .
WORKDIR "/src/NearbyProduceBackend"
RUN dotnet build "API.csproj" -c Release -o /app/build

COPY . .
RUN dotnet publish API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]
