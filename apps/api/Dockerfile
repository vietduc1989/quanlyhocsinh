# syntax=docker/dockerfile:1.4
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["apps/api/ONENET.StudentManagement.Api.csproj", "apps/api/"]
COPY ["packages/common/ONENET.StudentManagement.Common.csproj", "packages/common/"]
RUN dotnet restore "apps/api/ONENET.StudentManagement.Api.csproj"

COPY . .
WORKDIR "/src/apps/api"
RUN dotnet build "ONENET.StudentManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ONENET.StudentManagement.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Create a non-root user
RUN addgroup --system appgroup && adduser --system --ingroup appgroup appuser
USER appuser

EXPOSE 8080
ENTRYPOINT ["dotnet", "ONENET.StudentManagement.Api.dll"]