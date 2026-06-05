# syntax=docker/dockerfile:1.4
FROM node:20-alpine AS build
WORKDIR /app
COPY apps/web/package.json apps/web/package-lock.json ./
RUN npm ci

COPY apps/web/ ./
RUN npm run build

FROM nginx:stable-alpine AS final
WORKDIR /etc/nginx/conf.d
COPY --from=build /app/dist /usr/share/nginx/html
# Remove default nginx config
RUN rm default.conf
# Add a custom nginx configuration to run as a non-root user
COPY apps/web/nginx.conf .

# Switch to non-root user, 'nginx' user is typically pre-configured in nginx-alpine
USER nginx

EXPOSE 80
ENTRYPOINT ["nginx", "-g", "daemon off;"]