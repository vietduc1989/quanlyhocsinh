# QUAN-20260604-153038
# Stage 1: Build the React application
FROM node:20-alpine AS build

# Set working directory
WORKDIR /app

# Install pnpm globally
RUN npm install -g pnpm@11.0.0-0 --force

# Copy package.json, pnpm-lock.yaml, and pnpm-workspace.yaml
COPY pnpm-workspace.yaml ./
COPY apps/web/package.json apps/web/package.json
COPY apps/web/pnpm-lock.yaml apps/web/pnpm-lock.yaml

# Install dependencies (including dev dependencies)
RUN pnpm install --frozen-lockfile

# Copy the rest of the frontend source code
COPY apps/web/src apps/web/src
COPY apps/web/index.html apps/web/index.html
COPY apps/web/tsconfig.json apps/web/tsconfig.json
COPY apps/web/vite.config.ts apps/web/vite.config.ts

# Build the application
# Use the correct build command for your project. 'build' is defined in package.json.
RUN cd apps/web && pnpm run build

# Stage 2: Serve the application with Nginx
FROM nginx:alpine AS production

# Copy Nginx configuration
# If you have a custom nginx.conf, place it here.
# For a simple SPA, default Nginx config often works with correct root.
COPY --from=build /app/apps/web/dist /usr/share/nginx/html

# Expose port 80
EXPOSE 80

# Start Nginx
CMD ["nginx", "-g", "daemon off;"]