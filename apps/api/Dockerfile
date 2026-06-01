# Stage 1: Build the .NET application
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project file(s) and restore dependencies
# This is done first to leverage Docker layer caching if only code changes
COPY ["apps/api/*.csproj", "./"]
RUN dotnet restore

# Copy the rest of the application code
COPY apps/api/. .
WORKDIR /src

# Build and publish the application
RUN dotnet build -c Release -o /app/build
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Create the final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# The 'aspnet' base image already provides a non-root 'app' user listening on port 8080.
# We will leverage this for security and simplicity.
# See: https://learn.microsoft.com/en-us/dotnet/core/docker/building-net-docker-images#publish-to-an-image-with-non-root-user

# Copy published output from the build stage
COPY --from=build /app/publish .

# Set the user to the non-root 'app' user provided by the base image
USER app

EXPOSE 8080
ENTRYPOINT ["dotnet", "apps/api.dll"] # Adjust .dll name if your project has a different output name