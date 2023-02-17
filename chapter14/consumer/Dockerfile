#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

# <add commands here to modify both the debug and release container image>
RUN echo "This is my base image" > /tmp/image_type

FROM base as mydebug
RUN echo "This is my debug image" > /tmp/image_type

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["consumer/consumer.csproj", "consumer/"]
RUN dotnet restore "consumer/consumer.csproj"
COPY . .
WORKDIR "/src/consumer"
RUN dotnet build "consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "consumer.csproj" -c Release -o /app/publish

FROM base AS final
RUN apt update
RUN apt-get install curl iproute2 dnsutils -y
WORKDIR /app
COPY --from=publish /app/publish .
COPY ./consumer/entrypoint.sh .
ENTRYPOINT ["dotnet", "consumer.dll"]