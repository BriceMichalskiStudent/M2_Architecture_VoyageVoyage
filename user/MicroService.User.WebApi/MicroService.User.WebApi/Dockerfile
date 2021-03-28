#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MicroService.User.WebApi/MicroService.User.WebApi.csproj", "MicroService.User.WebApi/"]
RUN dotnet restore "MicroService.User.WebApi/MicroService.User.WebApi.csproj"
COPY . .
WORKDIR "/src/MicroService.User.WebApi"
RUN dotnet build "MicroService.User.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MicroService.User.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroService.User.WebApi.dll"]