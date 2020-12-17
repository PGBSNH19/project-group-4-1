#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

WORKDIR /app

COPY Source/API/API.csproj ./Source/API
RUN dotnet restore 

COPY . ./
RUN dotnet build "API.csproj" -c Release -o /app/build

COPY . ./
RUN dotnet publish "API.csproj" -c Release -o publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]