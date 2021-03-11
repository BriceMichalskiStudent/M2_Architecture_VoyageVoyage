#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MicroService.Travel.WebApi/MicroService.Travel.WebApi.csproj", "MicroService.Travel.WebApi/"]
RUN dotnet restore "MicroService.Travel.WebApi/MicroService.Travel.WebApi.csproj"
COPY . .
WORKDIR "/src/MicroService.Travel.WebApi"
RUN dotnet build "MicroService.Travel.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MicroService.Travel.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroService.Travel.WebApi.dll"]