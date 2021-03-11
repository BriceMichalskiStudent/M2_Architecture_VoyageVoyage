#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MicroService.Image.WebApi/MicroService.Image.WebApi.csproj", "MicroService.Image.WebApi/"]
RUN dotnet restore "MicroService.Image.WebApi/MicroService.Image.WebApi.csproj"
COPY . .
WORKDIR "/src/MicroService.Image.WebApi"
RUN dotnet build "MicroService.Image.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MicroService.Image.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroService.Image.WebApi.dll"]