#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
# <add commands here to modify both the debug and release container image>
RUN echo "This is my base image" > /tmp/image_type

FROM base as mydebug
RUN echo "This is my debug image" > /tmp/image_type

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["producer/producer.csproj", "producer/"]
RUN dotnet restore "producer/producer.csproj"
COPY . .
WORKDIR "/src/producer"
RUN dotnet build "producer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "producer.csproj" -c Release -o /app/publish

FROM base AS final
RUN apt update
RUN apt-get install curl iproute2 dnsutils -y
WORKDIR /app
COPY --from=publish /app/publish .
COPY ./producer/entrypoint.sh .
ENTRYPOINT ["dotnet", "producer.dll"]