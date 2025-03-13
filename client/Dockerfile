# Build Stage
FROM node:18-alpine AS build-image

# Set working directory
WORKDIR /app

# Copy package.json and package-lock.json for npm install
COPY ./package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of the React app
COPY ./ ./

# Build the React app
RUN npm run build

# Production Stage
FROM nginx:stable-alpine AS production

# Copy the built React app from the build stage to Nginx's HTML directory
COPY --from=build-image /app/dist /usr/share/nginx/html

# Expose port 80 for the container
EXPOSE 80

# Start Nginx in the foreground
CMD ["nginx", "-g", "daemon off;"]
