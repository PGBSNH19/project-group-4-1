#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim-arm64v8 AS build-env
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY Source/API/ NearbyProduceBackend/
RUN dotnet restore ./NearbyProduceBackend/API.csproj

COPY . ./
WORKDIR "/src/NearbyProduceBackend"
RUN dotnet build "API.csproj" -c Release -o /app/build

COPY . ./
FROM  build as publish
RUN dotnet publish API.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "API.dll"]
