Chào bạn, tôi là **DevOps Engineer của ONENET**. Tôi đã tiếp thu toàn bộ cấu trúc mã nguồn của **Phase 2: CRUD Học sinh** mà bạn đã cung cấp. 

Để triển khai hệ thống này một cách chuẩn chỉnh theo mô hình **Cloud-Native / Containerization**, đảm bảo tính tối giản dung lượng image, bảo mật cao và dễ dàng CI/CD, tôi xin gửi tới bạn toàn bộ cấu trúc file Docker hóa (`Dockerfile`, `.dockerignore`, `nginx.conf` và `docker-compose.yml`) cho cả Backend, Frontend và Database PostgreSQL.

---

# BẢN PHÂN BỔ THƯ MỤC TRIỂN KHAI (DEVOPS ARTIFACTS)

```text
onenet-project/
│
├── ONENET.Domain/
├── ONENET.Application/
├── ONENET.Infrastructure/
├── ONENET.WebAPI/
│   ├── Dockerfile                 <-- Dockerfile cho Backend (.NET 10)
│   └── .dockerignore              <-- Dockerignore cho Backend
│
├── frontend/ (tương ứng mã nguồn React của bạn)
│   ├── src/
│   ├── package.json
│   ├── Dockerfile                 <-- Dockerfile cho Frontend (React/Vite)
│   ├── .dockerignore              <-- Dockerignore cho Frontend
│   └── nginx.conf                 <-- Cấu hình Routing SPA cho Nginx
│
└── docker-compose.yml             <-- File điều phối toàn bộ hệ thống
```

---

# PHẦN 1: DOCKERIZATION CHO BACKEND (.NET 10)

Tôi sử dụng kỹ thuật **Multi-stage Build** để tách biệt môi trường Build (chứa SDK nặng) và môi trường Run (chỉ chứa Runtime tối giản), giúp kích thước Image cuối cùng cực kỳ nhẹ (~150MB) và bảo mật.

### 1. `ONENET.WebAPI/Dockerfile`
```dockerfile
# Stage 1: Base Runtime (Chỉ chứa môi trường chạy ứng dụng để bảo mật và tối ưu dung lượng)
FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS base
WORKDIR /app
EXPOSE 5115
ENV ASPNETCORE_URLS=http://+:5115
# Cài đặt văn hóa/ngôn ngữ tiếng Việt (nếu cần xử lý định dạng ngày tháng/tiền tệ)
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Stage 2: SDK Build (Môi trường chứa đầy đủ công cụ build code)
FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /src

# Copy toàn bộ file .csproj để Restore Dependencies trước (Tận dụng Docker Layer Caching)
COPY ["ONENET.WebAPI/ONENET.WebAPI.csproj", "ONENET.WebAPI/"]
COPY ["ONENET.Domain/ONENET.Domain.csproj", "ONENET.Domain/"]
COPY ["ONENET.Application/ONENET.Application.csproj", "ONENET.Application/"]
COPY ["ONENET.Infrastructure/ONENET.Infrastructure.csproj", "ONENET.Infrastructure/"]

RUN dotnet restore "ONENET.WebAPI/ONENET.WebAPI.csproj"

# Copy toàn bộ mã nguồn còn lại và Build
COPY . .
WORKDIR "/src/ONENET.WebAPI"
RUN dotnet build "ONENET.WebAPI.csproj" -c Release -o /app/build

# Stage 3: Publish (Biên dịch mã nguồn ra file DLL trung gian tối ưu)
FROM build AS publish
RUN dotnet publish "ONENET.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 4: Final Image (Chạy ứng dụng bằng cách copy kết quả từ stage publish)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Tạo user non-root để chạy ứng dụng (An toàn thông tin theo chuẩn CIS Benchmark)
USER $APP_UID

ENTRYPOINT ["dotnet", "ONENET.WebAPI.dll"]
```

### 2. `ONENET.WebAPI/.dockerignore`
```ignore
**/.git
**/.gitmodules
**/.gitignore
**/bin
**/obj
**/.vs
**/.vscode
**/*.user
**/*.suo
**/out
docker-compose*
LICENSE
README.md
```

---

# PHẦN 2: DOCKERIZATION CHO FRONTEND (React & Nginx)

Sử dụng Nginx phiên bản stable trên nhân Alpine Linux siêu nhẹ để phục vụ các file tĩnh được build từ React (Vite/CRA), giải quyết triệt để lỗi reload trang `404 Not Found` đặc trưng của Single Page Application (SPA).

### 1. `frontend/Dockerfile`
```dockerfile
# Stage 1: Build React App bằng Node LTS Alpine
FROM node:20-alpine AS build
WORKDIR /app

# Sao chép package.json và lockfile để cài đặt thư viện trước
COPY package*.json ./
RUN npm ci

# Sao chép mã nguồn và thực hiện build static files
COPY . .
# Biến môi trường truyền vào lúc build để chỉ định API Endpoint (mặc định trỏ về máy Client gọi cổng backend)
ARG VITE_API_BASE_URL=http://localhost:5115/api
ENV VITE_API_BASE_URL=$VITE_API_BASE_URL

RUN npm run build

# Stage 2: Khởi chạy Production Server bằng Nginx Alpine
FROM nginx:1.25-alpine

# Sao chép kết quả build từ Stage 1 sang thư mục của Nginx
COPY --from=build /app/dist /usr/share/nginx/html

# Ghi đè cấu hình Nginx mặc định để hỗ trợ React Router SPA
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
```

### 2. `frontend/nginx.conf`
```nginx
server {
    listen 80;
    server_name localhost;

    # Cấu hình nén Gzip tối ưu hóa hiệu năng tải trang cho Client
    gzip on;
    gzip_vary on;
    gzip_min_length 10240;
    gzip_proxied any;
    gzip_types text/plain text/css text/xml text/javascript application/x-javascript application/xml application/json;

    location / {
        root /usr/share/nginx/html;
        index index.html index.htm;
        # Dòng cực kỳ quan trọng: Định tuyến mọi request về index.html để React Router xử lý
        try_files $uri $uri/ /index.html;
    }

    # Cache tài sản tĩnh (hình ảnh, fonts, css, js) để tăng tốc độ tải trang
    location ~* \.(?:ico|css|js|gif|jpe?g|png|woff2?|eot|ttf|svg)$ {
        root /usr/share/nginx/html;
        expires 6M;
        access_log off;
        add_header Cache-Control "public, max-age=15552000, immutable";
    }

    error_page 500 502 503 504 /50x.html;
    location = /50x.html {
        root /usr/share/nginx/html;
    }
}
```

### 3. `frontend/.dockerignore`
```ignore
node_modules
build
dist
.env
.env.local
.env.development.local
.env.test.local
.env.production.local
.git
.gitignore
README.md
```

---

# PHẦN 3: ĐIỀU PHỐI HỆ THỐNG (DOCKER COMPOSE)

Tôi thiết lập một file `docker-compose.yml` hoàn chỉnh kết nối 3 service: **PostgreSQL database**, **ASP.NET Core Web API**, và **React Frontend App** vào chung một mạng nội bộ (`onenet-network`). Đồng thời tích hợp cơ chế `healthcheck` để đảm bảo Backend chỉ khởi động khi Postgres đã sẵn sàng kết nối.

### `docker-compose.yml`
```yaml
version: '3.8'

services:
  # ---------------------------------------------------------------------
  # 1. DATABASE SERVICE (PostgreSQL)
  # ---------------------------------------------------------------------
  db:
    image: postgres:16-alpine
    container_name: onenet_postgres_db
    restart: always
    environment:
      POSTGRES_DB: onenet_db
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: SecretPassword123! # Hãy thay đổi mật khẩu này khi lên Production
    ports:
      - "5432:5432"
    volumes:
      - onenet_postgres_data:/var/lib/postgresql/data
    networks:
      - onenet-network
    healthcheck:
      test: ["CMD-SHELL", "pg_isready -U postgres -d onenet_db"]
      interval: 5s
      timeout: 5s
      retries: 5

  # ---------------------------------------------------------------------
  # 2. BACKEND API SERVICE (.NET 10)
  # ---------------------------------------------------------------------
  backend:
    image: onenet-backend:latest
    build:
      context: .
      dockerfile: ONENET.WebAPI/Dockerfile
    container_name: onenet_backend_api
    restart: always
    ports:
      - "5115:5115"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development # Đổi sang Production khi deploy thật
      - ConnectionStrings__DefaultConnection=Host=db;Database=onenet_db;Username=postgres;Password=SecretPassword123!
    depends_on:
      db:
        condition: service_healthy # Chỉ chạy Backend sau khi Database đã kiểm tra sức khỏe thành công
    networks:
      - onenet-network

  # ---------------------------------------------------------------------
  # 3. FRONTEND UI SERVICE (React 18 & Mantine UI v7)
  # ---------------------------------------------------------------------
  frontend:
    image: onenet-frontend:latest
    build:
      context: ./frontend  # Trỏ về folder chứa code Frontend của bạn
      dockerfile: Dockerfile
      args:
        - VITE_API_BASE_URL=http://localhost:5115/api # URL Client của người dùng gọi API
    container_name: onenet_frontend_app
    restart: always
    ports:
      - "5173:80" # Map cổng 80 trong container Nginx ra cổng 5173 trên máy Host (Khớp với CORS Backend)
    depends_on:
      - backend
    networks:
      - onenet-network

# Định nghĩa Volume để lưu trữ dữ liệu PostgreSQL không bị mất khi Restart Container
volumes:
  onenet_postgres_data:
    driver: local

# Định nghĩa mạng nội bộ của ứng dụng ONENET
networks:
  onenet-network:
    driver: bridge
```

---

# PHẦN 4: HƯỚNG DẪN VẬN HÀNH & TRIỂN KHAI

Để khởi chạy toàn bộ hệ thống dự án, chỉ cần thực hiện các bước đơn giản sau:

### Bước 1: Chuẩn bị môi trường
Cài đặt sẵn **Docker** và **Docker Compose** trên máy tính/máy chủ triển khai.

### Bước 2: Khởi động hệ thống
Mở Terminal tại thư mục gốc chứa file `docker-compose.yml` và chạy lệnh:
```bash
# Build lại tất cả các images và khởi chạy container dưới chế độ background (detached)
docker-compose up --build -d
```

### Bước 3: Kiểm tra trạng thái hệ thống
Xem các container có hoạt động bình thường hay không:
```bash
docker-compose ps
```

Nếu thành công, bạn sẽ thấy cả 3 container `onenet_postgres_db`, `onenet_backend_api`, và `onenet_frontend_app` ở trạng thái **Up (healthy)**.

### Bước 4: Truy cập ứng dụng
*   **Giao diện người dùng (React):** Truy cập `http://localhost:5173`
*   **Tài liệu Swagger (Backend):** Truy cập `http://localhost:5115/swagger` để kiểm tra tài liệu API và test API trực tiếp.
*   **Tự động Migration:** Khi Backend khởi động, `Program.cs` sẽ tự động chạy `MigrateAsync()` để tạo bảng trong Database và gọi `SeedSampleDataAsync()` để nạp sẵn dữ liệu lớp học, học sinh mẫu ("Nguyễn Văn A", "Trần Thị B") vào PostgreSQL.

---

### 🛡️ Ý kiến đóng góp từ góc độ DevOps:
1.  **Cấu hình CORS:** Hiện tại cấu hình CORS trong `Program.cs` của Backend đang hardcode cứng `http://localhost:5173`. Khi deploy lên các môi trường Staging/Production khác nhau, tôi khuyến nghị chuyển cấu hình này thành biến môi trường (Environment Variable) để DevOps có thể linh động thay đổi URL mà không cần can thiệp Re-build code.
2.  **Mật khẩu bảo mật:** Tránh lưu các mật khẩu như `SecretPassword123!` trong file docker-compose khi đẩy lên Github/Gitlab. Hãy sử dụng Docker Secrets hoặc file `.env` nằm ngoài git để tối ưu an toàn thông tin!

Nhiệm vụ đóng gói Docker & DevOps của **Phase 2: CRUD Học sinh** đã hoàn thành xuất sắc! Chúc đội ngũ phát triển và QA ONENET nghiệm thu thành công!