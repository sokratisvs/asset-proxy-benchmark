# Step 1: Use Node.js for building the client
FROM node:18 AS build

# Step 2: Install client dependencies
WORKDIR /app
COPY ../client/package.json ../client/package-lock.json ./
RUN npm install

# Step 3: Copy files and build client
COPY ../client /app
RUN npm run build

# Step 4: Configure Vue development server
ENV VUE_APP_HOST=0.0.0.0
ENV VUE_APP_PORT=5173

# Step 4: Serve static files using a simple web server (e.g., `lite-server` or `http-server`)
FROM nginx:alpine
COPY --from=build /app/dist /usr/share/nginx/html

# Expose default port
EXPOSE 5173
