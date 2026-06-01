# Stage 1: Build the .NET application
FROM mcr.microsoft.com/dotnet/sdk:10.0-bookworm-slim AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src
# Copy the .csproj and restore dependencies
COPY ["apps/api/YourApiProject.csproj", "apps/api/"]
RUN dotnet restore "apps/api/YourApiProject.csproj"

# Copy the remaining application code
COPY . .
WORKDIR "/src/apps/api"
RUN dotnet build "YourApiProject.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage 2: Publish the .NET application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR "/src/apps/api"
RUN dotnet publish "YourApiProject.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 3: Create the final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0-bookworm-slim AS final

# Create a non-root user for security
RUN adduser --system --group --no-create-home appuser && chown -R appuser:appuser /app
USER appuser

WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "YourApiProject.dll"]