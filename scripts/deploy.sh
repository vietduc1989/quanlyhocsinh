#!/bin/bash

# This script is a placeholder for a production deployment.
# In a real-world scenario, this would involve deploying to a Kubernetes cluster,
# an EC2 instance with Docker, or another cloud service.

# Exit immediately if a command exits with a non-zero status.
set -e

echo "Starting deployment script..."

# Define your Docker Hub username and image names
DOCKER_USERNAME="onenet" # Replace with actual Docker Hub username or registry host
API_IMAGE="${DOCKER_USERNAME}/student-api:latest"
WEB_IMAGE="${DOCKER_USERNAME}/student-web:latest"

# Pull the latest Docker images
echo "Pulling latest Docker images..."
docker pull "$API_IMAGE"
docker pull "$WEB_IMAGE"
echo "Images pulled successfully."

# Stop and remove existing containers defined in docker-compose (if any)
echo "Stopping and removing existing Docker Compose services..."
docker compose down || true # '|| true' to prevent script from failing if containers don't exist

# Start new containers using docker-compose
echo "Starting new Docker Compose services..."
docker compose up -d
echo "Deployment successful! Services are now running."

# You might want to add post-deployment checks here, e.g.,
# curl http://localhost:8080/health
# curl http://localhost:80/