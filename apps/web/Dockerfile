# syntax=docker/dockerfile:1.4
FROM node:20-alpine AS build

WORKDIR /app

# Copy package.json and package-lock.json first to leverage Docker cache
COPY ["apps/web/package.json", "apps/web/package-lock.json", "./"]

# Install dependencies
RUN npm ci

# Copy the rest of the web source code
COPY ["apps/web", "./"]

# Build the React Vite application
RUN npm run build

FROM nginx:1.25-alpine AS final

# Remove default Nginx config to replace with our own (optional, but good practice)
RUN rm /etc/nginx/conf.d/default.conf

# Copy a custom Nginx configuration to serve the static files
# This assumes you have a custom nginx.conf in apps/web/nginx.conf
# For simplicity, we'll create a basic one directly here.
RUN echo "server { \
    listen 80; \
    location / { \
        root /usr/share/nginx/html; \
        try_files \$uri \$uri/ /index.html; \
    } \
    location /api/ { \
        proxy_pass http://api:8080/; \
        proxy_set_header Host \$host; \
        proxy_set_header X-Real-IP \$remote_addr; \
        proxy_set_header X-Forwarded-For \$proxy_add_x_forwarded_for; \
        proxy_set_header X-Forwarded-Proto \$scheme; \
    } \
}" > /etc/nginx/conf.d/quanlylophoch.conf


# Copy the built static files from the build stage
COPY --from=build /app/dist /usr/share/nginx/html

# Nginx runs as a non-root 'nginx' user by default, which is good for security.
# The files copied will be owned by root, but the 'nginx' user has read access.

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]