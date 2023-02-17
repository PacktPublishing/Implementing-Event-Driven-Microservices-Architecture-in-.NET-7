#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MTAEDA.Passenger.Command.API/MTAEDA.Passenger.Command.API.csproj", "MTAEDA.Passenger.Command.API/"]
RUN dotnet restore "MTAEDA.Passenger.Command.API/MTAEDA.Passenger.Command.API.csproj"
COPY . .
WORKDIR "/src/MTAEDA.Passenger.Command.API"
RUN dotnet build "MTAEDA.Passenger.Command.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MTAEDA.Passenger.Command.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MTAEDA.Passenger.Command.API.dll"]