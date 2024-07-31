#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MinRenovasjonProxy.sln", "./"]
COPY ["./MinRenovasjonProxy/MinRenovasjonProxy.csproj", "./MinRenovasjonProxy/"]
COPY ["./MinRenovasjonProxy.Core/MinRenovasjonProxy.Core.csproj", "./MinRenovasjonProxy.Core/"]
COPY ["./MinRenovasjonProxy.Services/MinRenovasjonProxy.Services.csproj", "./MinRenovasjonProxy.Services/"]
COPY ["./MinRenovasjonProxy.Services.Tests/MinRenovasjonProxy.Services.Tests.csproj", "./MinRenovasjonProxy.Services.Tests/"]
RUN dotnet restore "./MinRenovasjonProxy.sln"
COPY . .
WORKDIR "/src"
RUN dotnet build "./MinRenovasjonProxy.sln" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MinRenovasjonProxy/MinRenovasjonProxy.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
RUN TZ="Europe/Oslo"
# TODO: Replace these environment variables with docker-compose
ENV ASPNETCORE_ENVIRONMENT=Development
ENV DOTNET_ENVIRONMENT=Development
ENV MinRenovasjonProxy__NorkartRenovasjon__AppKey=<secret>
ENV MinRenovasjonProxy__NorkartRenovasjon__Kommunenr=<kommunenr>
ENV MinRenovasjonProxy__NorkartRenovasjon__Gatenavn=<gatenavn>
ENV MinRenovasjonProxy__NorkartRenovasjon__Gatekode=<gatekode>
ENV MinRenovasjonProxy__NorkartRenovasjon__Husnr=<husnr>
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MinRenovasjonProxy.dll"]