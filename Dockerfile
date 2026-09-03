# syntax=docker/dockerfile:1.7-labs

# --- Node frontend build stage ---
FROM node:26-alpine AS frontend
WORKDIR /app/client
COPY client/package*.json ./
RUN npm ci
COPY client ./
ARG AUTH0DOMAINENV
ARG AUTH0CLIENTIDENV
ENV AUTH0DOMAIN=${AUTH0DOMAINENV}
ENV AUTH0CLIENTID=${AUTH0CLIENTIDENV}
RUN npm run generate

# --- .NET build stage ---
FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /source

# Install .NET tools
RUN dotnet tool install --tool-path /tools dotnet-gcdump \
    && dotnet tool install --tool-path /tools dotnet-trace \
    && dotnet tool install --tool-path /tools dotnet-dump \
    && dotnet tool install --tool-path /tools dotnet-counters

# Copy project files and restore
COPY api/*.csproj ./api/
RUN dotnet restore ./api/sample-app.csproj

# Copy source + frontend build
COPY api ./api
COPY --from=frontend /app/client/dist ./api/wwwroot

# Publish backend
RUN dotnet publish ./api/sample-app.csproj -c Release -o /app

# --- Runtime stage ---
FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine
RUN apk --no-cache add curl

WORKDIR /tools
COPY --from=build /tools .

WORKDIR /app
COPY --from=build /app .

USER app
EXPOSE 8000
EXPOSE 8001

ENTRYPOINT ["dotnet", "sample-app.dll"]
