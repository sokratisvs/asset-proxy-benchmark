# Asset Benchmark App

This project consists of a Vue-based frontend application powered by Vite and a backend service built with .NET 8.

## Prerequisites

- Docker installed ([Get Docker](https://docs.docker.com/get-docker/))

## Running the Project with Docker

To run the project using Docker, follow these steps:

### 1. Navigate to the `containers` Directory
```sh
cd containers
```

### 2. Build and Start the Containers
```sh
docker-compose up --build
```

The application will be available at [http://localhost:5173](http://localhost:5173).

## Running the Frontend Locally

### Prerequisite
Ensure that you have at least Node.js 18 installed. You can check your version with:
```sh
node -v
```
If you need to install or update Node.js, download it from [here](https://nodejs.org/).

If you prefer to run the frontend without Docker:

### 1. Navigate to the Frontend Folder
```sh
cd client
```

### 2. Install Dependencies
```sh
npm install
```

### 3. Start the Development Server
```sh
npm run dev
```

The application will be accessible at [http://localhost:5173](http://localhost:5173).

## Running the Backend Locally

If you need to run the backend separately:

### 1. Install .NET SDK 8
Ensure that you have .NET 8 installed. You can download it from [here](https://dotnet.microsoft.com/en-us/download/dotnet/8.0).

### 2. Navigate to the Backend Folder
```sh
cd SvgDemoApi
```

### 3. Restore Dependencies
```sh
dotnet restore
```

### 4. Build the Backend
```sh
dotnet build
```

### 5. Start the Backend Server
```sh
dotnet run
```

The backend will be running on the configured port (check your `.env` or default to `http://localhost:5011`).

## Backend Docker Setup

If you want to run the backend inside a Docker container:

### 1. Build the Docker Image
```sh
docker build -t asset-benchmark-backend -f containers/backend.Dockerfile .
```

### 2. Run the Backend Container
```sh
docker run -p 5011:5011 asset-benchmark-backend
```

## Notes
- Ensure port `5173` (frontend) and the backend port (e.g., `5011` or `80` in Docker) are available on your machine.
- If you encounter issues, verify that Docker is running and that your project dependencies are installed correctly.

---
