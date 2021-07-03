#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Hackaton.WebApi/Hackaton.WebApi.csproj", "Hackaton.WebApi/"]
RUN dotnet restore "Hackaton.WebApi/Hackaton.WebApi.csproj"
COPY . .
WORKDIR "/src/Hackaton.WebApi"
RUN dotnet build "Hackaton.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hackaton.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hackaton.WebApi.dll"]