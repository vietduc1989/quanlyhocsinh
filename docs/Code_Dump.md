Chào bạn, với vai trò là **DevOps Engineer của ONENET**, tôi đã phân tích cấu trúc mã nguồn của dự án (gồm Backend .NET Core kiến trúc Clean Architecture và Frontend React SPA sử dụng Mantine). 

Để tối ưu hóa chi phí vận hành, đơn giản hóa hạ tầng deploy, tôi thiết kế một **Multi-stage Dockerfile** tối ưu. File này sẽ:
1. **Stage 1 (Frontend Builder):** Build ứng dụng React thành các file tĩnh (HTML, JS, CSS) bằng Node.js 20.
2. **Stage 2 (Backend Builder):** Restore và Publish WebAPI sử dụng .NET 8 SDK.
3. **Stage 3 (Final Runtime):** Sử dụng .NET Core Runtime tối giản, bảo mật (chạy dưới quyền user non-root), copy frontend đã build vào thư mục `wwwroot` của WebAPI để tự phục vụ (Self-host) cả API lẫn Frontend trên cùng một cổng (Port 8080). 

> *Lưu ý:* Việc dùng relative path ở Frontend (`const API_BASE_URL = '/api/students'`) rất phù hợp với cách tiếp cận gom chung (Single-container) này, giúp tránh hoàn toàn các lỗi về CORS.

Dưới đây là nội dung file cấu hình Docker của bạn:

### 1. File `Dockerfile` (Đặt tại thư mục gốc của dự án)

```dockerfile
# ==========================================
# STAGE 1: Build Frontend (React / Vite)
# ==========================================
FROM node:20-alpine AS frontend-builder
WORKDIR /app

# Copy package files để tận dụng Docker Cache layer cho node_modules
COPY frontend/package*.json ./
RUN npm ci --legacy-peer-deps || npm install --legacy-peer-deps

# Copy toàn bộ mã nguồn frontend và build
COPY frontend/ ./
RUN npm run build

# ==========================================
# STAGE 2: Restore & Build .NET Backend
# ==========================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS backend-builder
WORKDIR /src

# Copy các file .csproj để Restore dependencies trước (Tối ưu hóa Docker Cache)
COPY src/ONENET.Domain/ONENET.Domain.csproj src/ONENET.Domain/
COPY src/ONENET.Application/ONENET.Application.csproj src/ONENET.Application/
COPY src/ONENET.Infrastructure/ONENET.Infrastructure.csproj src/ONENET.Infrastructure/
COPY src/ONENET.WebAPI/ONENET.WebAPI.csproj src/ONENET.WebAPI/

RUN dotnet restore src/ONENET.WebAPI/ONENET.WebAPI.csproj

# Copy toàn bộ mã nguồn Backend và tiến hành Publish
COPY src/ ./src/
WORKDIR /src/src/ONENET.WebAPI
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# ==========================================
# STAGE 3: Final Runtime Image
# ==========================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app

# Khai báo biến môi trường cho .NET 8 (Mặc định chạy Port 8080)
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 8080

# Bảo mật container: Chạy ứng dụng dưới quyền user 'app' (Non-root) có sẵn trong alpine image
USER app

# Copy kết quả Publish của API
COPY --from=backend-builder /app/publish .

# Copy Frontend tĩnh đã build vào thư mục wwwroot của WebAPI
# (Vite mặc định output ra folder 'dist', nếu dùng CRA hãy đổi thành 'build')
COPY --from=frontend-builder --chown=app:app /app/dist ./wwwroot

# Khởi chạy ứng dụng
ENTRYPOINT ["dotnet", "ONENET.WebAPI.dll"]
```

---

### 2. File `.dockerignore` (Rất quan trọng để tối ưu dung lượng build context)
Hãy tạo file `.dockerignore` tại thư mục gốc của dự án để tránh việc copy những file không cần thiết vào Docker daemon:

```ignore
# Source control
.git
.gitignore
.vs
.vscode

# Backend folders
**/bin
**/obj
**/*.user
**/*.suo
**/*.db
**/*.local

# Frontend folders
**/node_modules
**/dist
**/build
**/.env*.local
npm-debug.log*
yarn-debug.log*
yarn-error.log*

# Docker files
Dockerfile
.dockerignore
```

---

### 💡 DevOps Notes cho ONENET Team:

1. **Cấu hình phục vụ Static Files trong .NET WebAPI:**
   Để WebAPI của bạn có thể trả về trang React Frontend khi truy cập root URL `/`, hãy đảm bảo trong file `Program.cs` của dự án `ONENET.WebAPI` đã được cấu hình phục vụ SPA files:
   ```csharp
   // Kích hoạt phục vụ các file tĩnh trong wwwroot (CSS, JS, Images của React)
   app.UseDefaultFiles();
   app.UseStaticFiles();

   // Cấu hình Routing fallback về index.html cho các route của React Router
   app.MapFallbackToFile("index.html");
   ```

2. **Lệnh build và chạy Docker Image:**
   * Build image:
     ```bash
     docker build -t onenet-app:phase2 .
     ```
   * Chạy Container ở môi trường Local/Staging kết nối tới database PostgreSQL:
     ```bash
     docker run -d -p 8080:8080 \
       -e ConnectionStrings__DefaultConnection="Host=postgres-db;Database=onenet_db;Username=postgres;Password=your_password" \
       --name onenet-service onenet-app:phase2
     ```