#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MTAEDA.Passenger.Query.API/MTAEDA.Passenger.Query.API.csproj", "MTAEDA.Passenger.Query.API/"]
RUN dotnet restore "MTAEDA.Passenger.Query.API/MTAEDA.Passenger.Query.API.csproj"
COPY . .
WORKDIR "/src/MTAEDA.Passenger.Query.API"
RUN dotnet build "MTAEDA.Passenger.Query.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MTAEDA.Passenger.Query.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MTAEDA.Passenger.Query.API.dll"]