# Project Title

This repository contains the full-stack scaffolding for our new project, featuring a modern frontend built with Vite, React, and TypeScript, and a robust backend powered by .NET 8 Clean Architecture.

## Table of Contents

-   [Project Title](#project-title)
-   [Table of Contents](#table-of-contents)
-   [Architecture Overview](#architecture-overview)
    -   [Frontend](#frontend)
    -   [Backend](#backend)
-   [Prerequisites](#prerequisites)
-   [Getting Started](#getting-started)
    -   [1. Clone the repository](#1-clone-the-repository)
    -   [2. Install pnpm](#2-install-pnpm)
    -   [3. Install Frontend Dependencies](#3-install-frontend-dependencies)
    -   [4. Run Frontend Development Server](#4-run-frontend-development-server)
    -   [5. Configure Backend](#5-configure-backend)
    -   [6. Run Backend API](#6-run-backend-api)
    -   [7. Access the Application](#7-access-the-application)
-   [Project Structure](#project-structure)
-   [Development Guidelines](#development-guidelines)
-   [Contributing](#contributing)
-   [License](#license)

## Architecture Overview

### Frontend

The frontend is built with:
-   **Vite**: A fast build tool that provides an extremely quick development experience.
-   **React**: A declarative, component-based JavaScript library for building user interfaces.
-   **TypeScript**: A strongly typed superset of JavaScript that enhances code quality and maintainability.
-   **Tailwind CSS**: A utility-first CSS framework for rapidly building custom designs.
-   **Headless UI**: Completely unstyled, fully accessible UI components for React.
-   **React Router DOM**: For declarative routing.
-   **AppShell Pattern**: Implemented with a responsive layout, including a persistent Sidebar and Header, promoting a consistent and premium user experience.

### Backend

The backend follows the **Clean Architecture** principles and is developed with:
-   **.NET 8**: The latest Long Term Support (LTS) version of .NET.
-   **Solution Structure**: Divided into logical layers:
    -   `Domain`: Core business entities, value objects, and domain events.
    -   `Application`: Business logic, application-specific services, commands, and queries (often using MediatR). Defines interfaces for infrastructure concerns.
    -   `Infrastructure`: Concrete implementations of persistence (e.g., Entity Framework Core), external services, and identity management.
    -   `WebAPI`: The entry point for HTTP requests, controllers, DTOs, and API-specific configurations.
-   **Entity Framework Core**: For object-relational mapping (ORM) and database interactions.
-   **Swagger/OpenAPI**: For API documentation and testing.

## Prerequisites

Before you begin, ensure you have the following installed:

-   [Node.js](https://nodejs.org/en/) (LTS recommended)
-   [pnpm](https://pnpm.io/installation) (Our chosen package manager)
-   [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
-   [Git](https://git-scm.com/downloads)
-   (Optional) [Docker Desktop](https://www.docker.com/products/docker-desktop/) if you plan to use Docker.

## Getting Started

Follow these steps to set up and run the project locally.

### 1. Clone the repository

```bash
git clone <your-repository-url>
cd <your-repository-name>
```

### 2. Install pnpm

If you don't have pnpm installed globally:

```bash
npm install -g pnpm
```

### 3. Install Frontend Dependencies

Navigate to the `frontend` directory and install dependencies using pnpm:

```bash
cd frontend
pnpm install
cd .. # Go back to the root
```

### 4. Run Frontend Development Server

From the `frontend` directory:

```bash
cd frontend
pnpm dev
```

This will start the Vite development server, usually accessible at `http://localhost:5173`.

### 5. Configure Backend

Navigate to the `backend/src/WebAPI` directory.
-   **Database Configuration**: The `WebAPI` project uses an in-memory database by default for easy startup. For a persistent database (e.g., SQL Server, PostgreSQL), update the connection string in `appsettings.Development.json` and configure `ApplicationDbContext` in `Infrastructure/Data/ApplicationDbContext.cs` and `Program.cs` accordingly.
-   **Migrations**: If using a persistent database, you'll need to create and apply migrations:
    ```bash
    cd backend/src/Infrastructure
    dotnet ef migrations add InitialCreate --output-dir Data/Migrations
    cd ../WebAPI
    dotnet ef database update
    ```
    (Note: `dotnet ef` tools need to be installed: `dotnet tool install --global dotnet-ef`)

### 6. Run Backend API

From the `backend/src/WebAPI` directory:

```bash
cd backend/src/WebAPI
dotnet watch run
```

This will start the ASP.NET Core API server, usually accessible at `https://localhost:7001` or `http://localhost:5001`. The Swagger UI will be available at `https://localhost:7001/swagger`.

### 7. Access the Application

Once both the frontend and backend servers are running, open your web browser and navigate to the frontend's development URL (e.g., `http://localhost:5173`).

## Project Structure

```
├── .github/                       # CI/CD workflows (optional)
├── frontend/                      # Frontend application
│   ├── public/                    # Static assets
│   ├── src/                       # Frontend source code
│   │   ├── api/                   # API clients/services
│   │   ├── assets/                # Static assets (images, icons)
│   │   ├── components/            # Reusable UI components
│   │   │   ├── AppShell/          # Core layout components (Sidebar, Header, Layout)
│   │   │   └── ui/                # Generic UI primitives
│   │   ├── contexts/              # React Contexts for global state
│   │   ├── hooks/                 # Custom React Hooks
│   │   ├── pages/                 # Route-specific components/pages
│   │   ├── routes/                # React Router setup
│   │   ├── styles/                # Global styles, Tailwind config
│   │   ├── types/                 # TypeScript type definitions
│   │   ├── utils/                 # Utility functions
│   │   ├── App.tsx                # Main application component
│   │   └── main.tsx               # Entry point for React application
│   ├── package.json               # Frontend dependencies and scripts
│   ├── tsconfig.json              # TypeScript configuration
│   ├── vite.config.ts             # Vite build configuration
│   └── tailwind.config.js         # Tailwind CSS configuration
├── backend/                       # Backend solution
│   ├── SolutionName.sln           # Visual Studio Solution file
│   ├── src/
│   │   ├── Domain/                # Core business entities, value objects, events
│   │   │   ├── Domain.csproj
│   │   │   ├── Entities/
│   │   │   └── Common/
│   │   ├── Application/           # Business logic, commands, queries, application services
│   │   │   ├── Application.csproj
│   │   │   ├── Common/
│   │   │   ├── Features/          # Use Cases (e.g., Example Feature Commands/Queries)
│   │   │   └── Interfaces/
│   │   ├── Infrastructure/        # Data access, external services, identity implementation
│   │   │   ├── Infrastructure.csproj
│   │   │   ├── Data/              # EF Core DbContext, Migrations, Seeders
│   │   │   └── Services/
│   │   └── WebAPI/                # API entry point, controllers, DTOs, API configurations
│   │       ├── WebAPI.csproj
│   │       ├── Controllers/
│   │       ├── Program.cs         # Application entry point and configuration
│   │       ├── appsettings.json
│   │       └── Properties/
│   │           └── launchSettings.json
│   ├── tests/                     # Unit, Integration, and Acceptance Tests (optional, but recommended)
│   ├── .dockerignore              # Files/folders to ignore in Docker builds
│   └── Dockerfile                 # Dockerfile for backend
├── .gitignore                     # Git ignore rules
├── pnpm-workspace.yaml            # pnpm workspace configuration
└── README.md                      # Project documentation
```

## Development Guidelines

-   **Clean Code**: Emphasize readability, maintainability, and testability.
-   **Component-Driven Development (Frontend)**: Build UI in isolated, reusable components.
-   **Layered Architecture (Backend)**: Adhere strictly to Clean Architecture principles. Dependencies should flow inwards (WebAPI -> Application -> Domain, Infrastructure depends on Application).
-   **Type Safety**: Utilize TypeScript and C# strong typing rigorously.
-   **API Design**: Follow RESTful principles for backend API endpoints.
-   **Testing**: Write unit and integration tests for critical logic.

## Contributing

Please refer to `CONTRIBUTING.md` (to be created) for guidelines on how to contribute to this project.

## License

This project is licensed under the MIT License. See the `LICENSE` file (to be created) for details.