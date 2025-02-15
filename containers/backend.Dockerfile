# Step 1: Use official .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project files into the container
COPY ../SvgDemoApi/SvgDemoApi.csproj ./SvgDemoApi/

# Restore the project dependencies
RUN dotnet restore ./SvgDemoApi/SvgDemoApi.csproj

# Copy the rest of the application and files, including wwwroot folder
COPY ../SvgDemoApi ./SvgDemoApi

# Publish the project
RUN dotnet publish ./SvgDemoApi/SvgDemoApi.csproj -c Release -o /app

# Step 2: Use the official .NET runtime image for running the backend
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Set the working directory inside the container
WORKDIR /app

# Copy the published files from the build stage
COPY --from=build /app .

# Expose the port that the application will run on
EXPOSE 5011

# Set the entry point for the application
ENTRYPOINT ["dotnet", "SvgDemoApi.dll"]