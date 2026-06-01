# Stage 1: Build the .NET application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build # Using .NET 8 SDK as .NET 10 is not yet released for official images. I'll proceed with 8.0/8.0 as it aligns with modern practices.
WORKDIR /src

# Copy the .csproj file and restore dependencies
COPY ["apps/api/QuanLyDiemHocKy.Api.csproj", "apps/api/"]
RUN dotnet restore "apps/api/QuanLyDiemHocKy.Api.csproj"

# Copy the rest of the application code
COPY . .
WORKDIR "/src/apps/api"
RUN dotnet build "QuanLyDiemHocKy.Api.csproj" -c Release -o /app/build

# Publish the application
RUN dotnet publish "QuanLyDiemHocKy.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Create the final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Create a non-root user and group
# The UID/GID are arbitrary but common choices for non-root users.
# The user 'appuser' is created with no home directory, no shell, and no password.
RUN addgroup --system appgroup && adduser --system --ingroup appgroup appuser

# Copy published application from the build stage
COPY --from=build /app/publish .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "QuanLyDiemHocKy.Api.dll"]

# Expose the port (ASP.NET Core on Linux listens on 8080 by default when running as non-root)
EXPOSE 8080

# Run the container as the non-root user
USER appuser