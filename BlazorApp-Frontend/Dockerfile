#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["BlazorApp-Frontend/BlazorApp-Frontend.csproj", "BlazorApp-Frontend/"]
RUN dotnet restore "BlazorApp-Frontend/BlazorApp-Frontend.csproj"
COPY . .
WORKDIR "/src/BlazorApp-Frontend"
RUN dotnet build "BlazorApp-Frontend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorApp-Frontend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorApp-Frontend.dll"]