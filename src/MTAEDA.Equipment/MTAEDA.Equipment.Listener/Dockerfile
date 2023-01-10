#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MTAEDA.Equipment.Listener/MTAEDA.Equipment.Listener.csproj", "MTAEDA.Equipment.Listener/"]
COPY ["MTAEDA.Equipment.Infrastructure/MTAEDA.Equipment.Infrastructure.csproj", "MTAEDA.Equipment.Infrastructure/"]
COPY ["MTAEDA.Equipment.Domain/MTAEDA.Equipment.Domain.csproj", "MTAEDA.Equipment.Domain/"]
RUN dotnet restore "MTAEDA.Equipment.Listener/MTAEDA.Equipment.Listener.csproj"
COPY . .
WORKDIR "/src/MTAEDA.Equipment.Listener"
RUN dotnet build "MTAEDA.Equipment.Listener.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MTAEDA.Equipment.Listener.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MTAEDA.Equipment.Listener.dll"]