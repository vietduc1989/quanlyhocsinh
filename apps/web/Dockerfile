# Stage 1: Build the React Vite application
FROM node:20-alpine AS build

WORKDIR /app

# Copy package.json and package-lock.json and install dependencies
COPY ["apps/web/package.json", "apps/web/package-lock.json*", "./"]
RUN npm install --frozen-lockfile

# Copy the rest of the application code
COPY . .
WORKDIR "/app/apps/web"

# Build the React Vite application
# Set the VITE_API_BASE_URL during build for production, or handle at runtime if preferred
# For simplicity, assuming runtime env var will be handled by Nginx config or JS itself
RUN npm run build

# Stage 2: Serve the application with Nginx
FROM nginx:alpine AS final

# Create a non-root user for security. Nginx's default config often runs as `nginx` user.
# Ensure the nginx user has access to the /var/cache/nginx directory.
RUN addgroup -S appgroup && adduser -S appuser -G appgroup
RUN chown -R appuser:appgroup /var/cache/nginx /var/run /var/log/nginx
# Use the nginx user if default config allows, or switch to 'appuser' if more control needed.
# For simplicity and standard Nginx setup, we'll let Nginx manage its user.
# If you need full custom user control, you would configure Nginx to run as 'appuser'.
# For now, let's just make sure the content is accessible.

# Remove default Nginx configuration
RUN rm /etc/nginx/conf.d/default.conf

# Copy custom Nginx configuration
COPY apps/web/nginx.conf /etc/nginx/conf.d/default.conf

# Copy the built React app to the Nginx public directory
COPY --from=build /app/apps/web/dist /usr/share/nginx/html

# Ensure Nginx serves files owned by the Nginx user
RUN chown -R nginx:nginx /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]