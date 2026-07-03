# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["WordWise.Prez/WordWise.Prez.csproj", "WordWise.Prez/"]
COPY ["WordWise.Application/WordWise.Application.csproj", "WordWise.Application/"]
COPY ["WordWise.Core/WordWise.Core.csproj", "WordWise.Core/"]
COPY ["WordWise.Framework/WordWise.Framework.csproj", "WordWise.Framework/"]
COPY ["WordWise.Infra/WordWise.Infra.csproj", "WordWise.Infra/"]
RUN dotnet restore "./WordWise.Prez/WordWise.Prez.csproj"
COPY . .
WORKDIR "/src/WordWise.Prez"
RUN dotnet build "./WordWise.Prez.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WordWise.Prez.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WordWise.Prez.dll"]