#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MTAEDA.Notification.Command.API/MTAEDA.Notification.Command.API.csproj", "MTAEDA.Notification.Command.API/"]
RUN dotnet restore "MTAEDA.Notification.Command.API/MTAEDA.Notification.Command.API.csproj"
COPY . .
WORKDIR "/src/MTAEDA.Notification.Command.API"
RUN dotnet build "MTAEDA.Notification.Command.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MTAEDA.Notification.Command.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MTAEDA.Notification.Command.API.dll"]