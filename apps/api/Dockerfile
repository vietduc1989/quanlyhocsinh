# syntax=docker/dockerfile:1.4
FROM mcr.microsoft.com/dotnet/sdk:10.0-bookworm-slim AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ["apps/api/QuanLyLopHoc.Api.csproj", "apps/api/"]
RUN dotnet restore "apps/api/QuanLyLopHoc.Api.csproj"

# Copy the rest of the API source code
COPY . .
WORKDIR "/src/apps/api"
RUN dotnet build "QuanLyLopHoc.Api.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR "/src/apps/api"
RUN dotnet publish "QuanLyLopHoc.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0-bookworm-slim AS final
WORKDIR /app

# Create a non-root user and group
# GID 1001, UID 1001 are common choices for non-root users in containers.
RUN addgroup --system --gid 1001 appuser && \
    adduser --system --uid 1001 --ingroup appuser appuser

# Install curl for health checks (required for docker-compose healthcheck)
RUN apt-get update && apt-get install -y --no-install-recommends curl && \
    rm -rf /var/lib/apt/lists/*

COPY --from=publish /app/publish .

# Change ownership of the application directory to the non-root user
RUN chown -R appuser:appuser /app

# Switch to the non-root user for running the application
USER appuser

EXPOSE 8080
ENV ASPNETCORE_URLS="http://+:8080"
ENTRYPOINT ["dotnet", "QuanLyLopHoc.Api.dll"]