# Stage 1: Build the React application
FROM node:20-alpine AS build
WORKDIR /app

# Copy package.json and lock file(s) to install dependencies
# This is done first to leverage Docker layer caching if only dependencies change
COPY apps/web/package*.json ./
# Assuming npm for package management, adjust to yarn or pnpm if needed
RUN npm install --frozen-lockfile # Use --frozen-lockfile for CI/CD if package-lock.json exists

# Copy the rest of the application code
COPY apps/web/. .

# Build the React app
# Assuming 'npm run build' generates static files in a 'dist' directory
RUN npm run build

# Stage 2: Serve the application with Nginx
FROM nginx:alpine AS final
# Create a non-root user and group for Nginx
RUN addgroup --system appgroup && adduser --system --ingroup appgroup appuser

# Copy custom Nginx configuration
# This config will instruct Nginx to run as 'appuser' and serve files from /usr/share/nginx/html
COPY apps/web/nginx.conf /etc/nginx/conf.d/default.conf

# Copy static assets from the build stage
COPY --from=build /app/dist /usr/share/nginx/html

# Ensure Nginx's cache and log directories are writable by the appuser
RUN mkdir -p /var/cache/nginx /var/log/nginx \
    && chown -R appuser:appgroup /var/cache/nginx /var/log/nginx /usr/share/nginx/html \
    && chmod -R 755 /usr/share/nginx/html

# Nginx listens on port 80 by default
EXPOSE 80

# The Nginx base image already defines an ENTRYPOINT, just ensure it runs
# Our custom nginx.conf will specify the user directive.
CMD ["nginx", "-g", "daemon off;"]