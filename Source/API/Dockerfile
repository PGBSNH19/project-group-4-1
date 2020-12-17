#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

WORKDIR /app

COPY *.csproj ./
RUN dotnet restore 

COPY . ./
RUN dotnet build "API.csproj" -c Release -o /app/build

COPY . ./
RUN dotnet publish "API.csproj" -c Release -o publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]