# Stage 1: Build the .NET application
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src
# Copy everything
COPY apps/api/ ./apps/api/

WORKDIR /src/apps/api
RUN dotnet restore src/ONENET.WebAPI/ONENET.WebAPI.csproj

# Build and Publish
RUN dotnet publish src/ONENET.WebAPI/ONENET.WebAPI.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 2: Create the final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

# Use the built-in app user
USER app

WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "ONENET.WebAPI.dll"]
