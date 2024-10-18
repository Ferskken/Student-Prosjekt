# Student-Prosjekt
Intro project at Fiizk used as skill development.

This is a full-stack web application built using the following technologies:

- Frontend: Vue.js (Vite)
- Backend: .NET 8 (C#)
- Database: PostgreSQL (Dockerized)


# Database - PostgreSQL
This project uses PostgreSQL as the database, running inside a Docker container.

## Project Setup
To start the PostgreSQL database, run the following command:
```sh
docker compose up --build
```

Ensure that you have set up the required environment variables in a .env file for configuring the database connection. For example:
 ```sh
POSTGRES_USER=your_db_user
POSTGRES_PASSWORD=your_db_password
POSTGRES_DB=your_db_name
```

Once the container is running, the database should be accessible and ready for connections from the backend.

# Backend - .NET 8

Restore the necessary dependencies by running the following command inside the backend project directory:
```sh
dotnet restore
```
To start the backend server locally, use the following command:
```sh
dotnet run
```

The backend will start running at http://localhost:<port> 

```sh
dotnet ef database update
```
This ensures that your database schema matches the current state of your entity models.

# Frontend - Vue.js

The frontend is built with Vue 3 using Vite for a modern development experience. Vite provides fast builds and hot-reloading for quicker iteration during development.




## Type Support for `.vue` Imports in TS

TypeScript cannot handle type information for `.vue` imports by default, so we replace the `tsc` CLI with `vue-tsc` for type checking. In editors, we need [Volar](https://marketplace.visualstudio.com/items?itemName=Vue.volar) to make the TypeScript language service aware of `.vue` types.

## Customize configuration

See [Vite Configuration Reference](https://vite.dev/config/).

## Project Setup

```sh
npm install
```

### Compile and Hot-Reload for Development

```sh
npm run dev
```

### Type-Check, Compile and Minify for Production

```sh
npm run build
```

### Run Unit Tests with [Vitest](https://vitest.dev/)

```sh
npm run test:unit
```

### Lint with [ESLint](https://eslint.org/)

```sh
npm run lint
```

# Docker Setup

Docker is used to containerize the PostgreSQL database. The database is defined in the docker-compose.yml file. To manage the database with Docker, you can use the following commands:

## Build and Start Docker Containers
```sh
docker compose up --build
```

This command will build the containers and start all services (e.g., PostgreSQL) as defined in the docker-compose.yml file.

## Stop Docker Containers
```sh
docker compose down
```

## Useful Docker Commands
### Check running containers:
```sh
docker ps
```

### Access the PostgreSQL container:
```sh
docker exec -it <container_id> /bin/bash
```

### View Docker logs:
```sh
docker logs <container_id>
```


# Running the Project 

## Start the Database:
```sh
docker compose up --build
```

## Run the Backend
```sh
dotnet run
```

## Start the Frontend
```sh
npm run dev
```

Once all services are up, the frontend should be available at http://localhost:<frontend_port>, and the backend APIs will be accessible at http://localhost:<backend_port>.



# Development Tools 
## Recomended Tools

- **JetBrains Rider** for backend (.NET) development.
- **WebStorm or VSCode** for frontend (Vue.js) development.
- **Vue.js Devtools** for inspecting Vue components during development.
- **JavaScript Debug** for frontend debugging in Chrome.
- **Postman** for testing API endpoints.

## Debuging 

For debugging the frontend, use the JavaScript Debug configuration WebStorm or the JavaScript Extention for VScode
You can also inspect Vue components using the Vue.js Devtools browser extension.