# Base image for .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files and restore dependencies
COPY backend/SolutionName.sln ./
COPY backend/src/Domain/Domain.csproj backend/src/Domain/
COPY backend/src/Application/Application.csproj backend/src/Application/
COPY backend/src/Infrastructure/Infrastructure.csproj backend/src/Infrastructure/
COPY backend/src/WebAPI/WebAPI.csproj backend/src/WebAPI/

# Restore dependencies for all projects
RUN dotnet restore backend/SolutionName.sln

# Copy the remaining source code
COPY backend/src/ backend/src/

WORKDIR /src/backend/src/WebAPI
RUN dotnet build "WebAPI.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose ports
EXPOSE 8080
EXPOSE 8081

ENTRYPOINT ["dotnet", "WebAPI.dll"]