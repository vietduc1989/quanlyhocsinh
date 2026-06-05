# QUAN-20260604-153038
# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY apps/api/src/ONENET.Domain/ONENET.Domain.csproj apps/api/src/ONENET.Domain/
COPY apps/api/src/ONENET.Application/ONENET.Application.csproj apps/api/src/ONENET.Application/
COPY apps/api/src/ONENET.Infrastructure/ONENET.Infrastructure.csproj apps/api/src/ONENET.Infrastructure/
COPY apps/api/src/ONENET.WebAPI/ONENET.WebAPI.csproj apps/api/src/ONENET.WebAPI/

# Restore direct project references first
RUN dotnet restore apps/api/src/ONENET.WebAPI/ONENET.WebAPI.csproj

# Copy all source code
COPY . .

# Build the project
WORKDIR /src/apps/api/src/ONENET.WebAPI
RUN dotnet build "ONENET.WebAPI.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "ONENET.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=publish /app/publish .

# MinIO related environment variables - for future file storage features
# If MinIO is external, these would be configured in deployment
# ENV MINIO_ENDPOINT="http://minio:9000"
# ENV MINIO_ACCESS_KEY="minioadmin"
# ENV MINIO_SECRET_KEY="minioadmin"
# ENV MINIO_BUCKET_NAME="onenet-attachments"

ENTRYPOINT ["dotnet", "ONENET.WebAPI.dll"]