Chào các thành viên trong đội ngũ phát triển ONENET. Với vai trò là DevOps Engineer, tôi đã thiết kế cấu trúc Docker hóa tối ưu, bảo mật và sẵn sàng cho môi trường Production cho dự án **"Quản lý học sinh"**.

Để đảm bảo tính linh hoạt, tôi giả định hệ thống này sử dụng kiến trúc chuẩn Microservices/Three-tier bao gồm:
1. **Frontend:** ReactJS / Angular / Vue (Chạy trên Nginx)
2. **Backend API:** Java Spring Boot / Node.js (Trong hướng dẫn này tôi demo với **Spring Boot** - công nghệ phổ biến của ONENET, bạn có thể dễ dàng map sang Node.js/Python nếu cần).
3. **Database:** PostgreSQL (Quản lý dữ liệu học sinh an toàn, toàn vẹn).

Dưới đây là cấu trúc thư mục dự án và các file cấu hình chi tiết.

---

### 1. Cấu trúc thư mục dự án khuyến nghị (Directory Structure)
```text
quan-ly-hoc-sinh/
│
├── backend/
│   ├── src/
│   ├── pom.xml
│   └── Dockerfile
│
├── frontend/
│   ├── src/
│   ├── package.json
│   ├── nginx.conf
│   └── Dockerfile
│
├── docker-compose.yml
└── .env
```

---

### 2. Backend Dockerfile (`backend/Dockerfile`)
Sử dụng **Multi-stage build** để giảm tối đa kích thước Image và đảm bảo an toàn thông tin (không lộ mã nguồn trong production image).

```dockerfile
# --- Stage 1: Build ứng dụng ---
FROM maven:3.9.6-eclipse-temurin-17-alpine AS builder
WORKDIR /app

# Cache dependencies trước để tối ưu hóa thời gian build sau này
COPY pom.xml .
RUN mvn dependency:go-offline -B

# Copy source code và build file jar
COPY src ./src
RUN mvn clean package -DskipTests

# --- Stage 2: Chạy ứng dụng (Chỉ giữ lại môi trường JRE tinh gọn) ---
FROM eclipse-temurin:17-jre-alpine
WORKDIR /app

# Tạo user 'onenet' để chạy app (Tránh chạy bằng quyền root để bảo mật)
RUN addgroup -S onenetgroup && adduser -S onenetuser -G onenetgroup
USER onenetuser

# Copy file jar từ Stage 1 sang
COPY --from=builder /app/target/*.jar app.jar

# Khai báo Port và thiết lập cấu hình JVM tối ưu cho Docker container
EXPOSE 8080
ENV JAVA_OPTS="-XX:+UseG1GC -XX:+UseContainerSupport -XX:MaxRAMPercentage=75.0"

ENTRYPOINT ["sh", "-c", "java $JAVA_OPTS -jar app.jar"]
```

---

### 3. Frontend Dockerfile (`frontend/Dockerfile`)
Sử dụng Nginx alpine làm web server phục vụ file tĩnh.

```dockerfile
# --- Stage 1: Build static files ---
FROM node:20-alpine AS builder
WORKDIR /app

COPY package*.json ./
RUN npm ci

COPY . .
RUN npm run build

# --- Stage 2: Serve bằng Nginx ---
FROM nginx:1.25-alpine

# Copy file build từ Stage 1
COPY --from=builder /app/dist /usr/share/nginx/html

# Copy cấu hình Nginx riêng (để handle Single Page Application - SPA routing)
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
```

#### *Lưu ý:* Tạo thêm file `frontend/nginx.conf` để tránh lỗi 404 khi reload trang (React/Angular Router):
```nginx
server {
    listen 80;
    server_name localhost;

    location / {
        root /usr/share/nginx/html;
        index index.html index.htm;
        try_files $uri $uri/ /index.html;
    }

    # Proxy API requests lên backend container
    location /api/ {
        proxy_pass http://backend-service:8080/;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
    }
}
```

---

### 4. File cấu hình môi trường (`.env`)
Chứa các biến môi trường nhạy cảm, không nên hardcode vào docker-compose.

```env
DB_USER=onenet_dev
DB_PASSWORD=SecurePassword2024
DB_NAME=quan_ly_hoc_sinh_db
PORT_FRONTEND=80
PORT_BACKEND=8080
PORT_DB=5432
```

---

### 5. Docker Compose (`docker-compose.yml`)
File điều phối toàn bộ hệ thống ứng dụng "Quản lý học sinh", tối ưu hóa với Healthcheck đảm bảo Backend chỉ chạy khi Database đã sẵn sàng.

```yaml
version: '3.8'

services:
  # 1. Database Service (PostgreSQL)
  database:
    image: postgres:15-alpine
    container_name: qlhs-postgres-db
    restart: always
    environment:
      POSTGRES_USER: ${DB_USER}
      POSTGRES_PASSWORD: ${DB_PASSWORD}
      POSTGRES_DB: ${DB_NAME}
    ports:
      - "${PORT_DB}:5432"
    volumes:
      - qlhs_postgres_data:/var/lib/postgresql/data
    networks:
      - onenet-network
    healthcheck:
      test: ["CMD-SHELL", "pg_isready -U $$POSTGRES_USER -d $$POSTGRES_DB"]
      interval: 10s
      timeout: 5s
      retries: 5

  # 2. Backend Service (Spring Boot API)
  backend:
    build:
      context: ./backend
      dockerfile: Dockerfile
    container_name: qlhs-backend-api
    restart: always
    ports:
      - "${PORT_BACKEND}:8080"
    environment:
      SPRING_DATASOURCE_URL: jdbc:postgresql://database:5432/${DB_NAME}
      SPRING_DATASOURCE_USERNAME: ${DB_USER}
      SPRING_DATASOURCE_PASSWORD: ${DB_PASSWORD}
      SPRING_JPA_HIBERNATE_DDL_AUTO: update
    depends_on:
      database:
        condition: service_healthy # Đợi DB khỏe rồi mới khởi động backend
    networks:
      - onenet-network

  # 3. Frontend Service (React/Nginx)
  frontend:
    build:
      context: ./frontend
      dockerfile: Dockerfile
    container_name: qlhs-frontend-web
    restart: always
    ports:
      - "${PORT_FRONTEND}:80"
    depends_on:
      - backend
    networks:
      - onenet-network

networks:
  onenet-network:
    driver: bridge

volumes:
  qlhs_postgres_data:
    driver: local
```

---

### 6. Hướng dẫn vận hành hệ thống (Runbook)

**Bước 1:** Khởi động toàn bộ hệ thống (lần đầu sẽ build image):
```bash
docker-compose up -d --build
```

**Bước 2:** Kiểm tra trạng thái hoạt động của các Container:
```bash
docker-compose ps
```

**Bước 3:** Xem logs thời gian thực nếu xảy ra lỗi:
```bash
docker-compose logs -f [tên_service] # Ví dụ: docker-compose logs -f backend
```

**Bước 4:** Dừng hệ thống:
```bash
docker-compose down
```
*(Nếu muốn xóa sạch dữ liệu database cũ để test lại từ đầu: `docker-compose down -v`)*

---
*Mọi yêu cầu điều chỉnh liên quan đến CI/CD (GitHub Actions/GitLab CI) hoặc deploy lên K8s (Kubernetes) cho dự án ONENET này, vui lòng báo lại tôi để được hỗ trợ kịp thời!*