# GoodFood

An open-source food delivery platform inspired by Uber Eats, built with a microservices architecture.

## Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Tech Stack](#tech-stack)
- [Quick Start](#quick-start)
- [Development Setup](#development-setup)
- [Production Setup](#production-setup)
- [Services & Ports](#services--ports)
- [Project Structure](#project-structure)
- [API Documentation](#api-documentation)
- [Contributing](#contributing)
- [License](#license)

## Overview

GoodFood is a complete food delivery ecosystem featuring:

- Customer-facing web and mobile applications
- Restaurant management system
- Real-time order tracking
- Payment processing with Stripe
- Inventory and supplier management
- Customer support ticketing system
- Centralized logging and notifications

## Architecture

The platform is organized into 12 independent clusters:

| Cluster | Service | Description |
|---------|---------|-------------|
| 0 | Mobile App | React Native customer app |
| 1 | Front Web | React web application |
| 2 | Auth | Authentication & authorization |
| 3 | Restaurant | Restaurant, menu & item management |
| 4 | Paiement | Payment processing (Stripe) |
| 5 | Commandes | Order management |
| 6 | Tracking | Real-time delivery tracking |
| 7 | SAV | Customer support & ticketing |
| 8 | Notifications | Push & email notifications |
| 9 | Logs | Centralized logging |
| 10 | Stock | Inventory management |
| 11 | Fournisseurs | Supplier management |

```
┌─────────────────────────────────────────────────────────────────┐
│                         CLIENTS                                  │
│  ┌─────────────┐  ┌─────────────┐                               │
│  │  Mobile App │  │   Web App   │                               │
│  │   (Expo)    │  │   (React)   │                               │
│  └──────┬──────┘  └──────┬──────┘                               │
└─────────┼────────────────┼──────────────────────────────────────┘
          │                │
          ▼                ▼
┌─────────────────────────────────────────────────────────────────┐
│                      API GATEWAY                                 │
└─────────────────────────────────────────────────────────────────┘
          │
          ▼
┌─────────────────────────────────────────────────────────────────┐
│                     MICROSERVICES                                │
│  ┌────────┐ ┌────────────┐ ┌─────────┐ ┌───────────┐           │
│  │  Auth  │ │ Restaurant │ │Paiement │ │ Commandes │           │
│  └────────┘ └────────────┘ └─────────┘ └───────────┘           │
│  ┌────────┐ ┌────────────┐ ┌─────────┐ ┌───────────┐           │
│  │Tracking│ │    SAV     │ │  Notif  │ │   Logs    │           │
│  └────────┘ └────────────┘ └─────────┘ └───────────┘           │
│  ┌────────┐ ┌────────────┐                                      │
│  │ Stock  │ │Fournisseurs│                                      │
│  └────────┘ └────────────┘                                      │
└─────────────────────────────────────────────────────────────────┘
          │
          ▼
┌─────────────────────────────────────────────────────────────────┐
│                       DATABASES                                  │
│  ┌─────────────────────────┐  ┌─────────────────────────┐       │
│  │     SQL Server          │  │       MongoDB           │       │
│  │  (Auth, Orders, etc.)   │  │  (Tracking, Logs)       │       │
│  └─────────────────────────┘  └─────────────────────────┘       │
└─────────────────────────────────────────────────────────────────┘
```

## Tech Stack

| Component | Technology |
|-----------|------------|
| Frontend Web | React 19, TypeScript, Vite |
| Mobile | React Native, Expo |
| Backend (.NET) | C# / .NET 9, ASP.NET Core |
| Backend (Restaurant) | Python, Flask, Gunicorn |
| Backend (Paiement) | Next.js, Stripe API |
| SQL Database | Microsoft SQL Server 2022 |
| NoSQL Database | MongoDB 7 |
| Containerization | Docker, Docker Compose |
| Orchestration | Kubernetes, Helm |
| CI/CD | GitHub Actions, SonarCloud |

## Quick Start

### Prerequisites

- [Docker](https://docs.docker.com/get-docker/) (v20.10+)
- [Docker Compose](https://docs.docker.com/compose/install/) (v2.0+)
- [Git](https://git-scm.com/downloads)

### 1. Clone the repository

```bash
git clone https://github.com/your-org/goodfood.git
cd goodfood
```

### 2. Configure environment variables

```bash
cp .env.example .env
# Edit .env with your configuration
```

### 3. Start the development environment

```bash
docker compose -f docker-compose.dev.yml up -d
```

### 4. Access the application

- **Web App**: http://localhost:3000
- **API Auth**: http://localhost:3001
- **API Restaurant**: http://localhost:3002

That's it! The entire platform is now running locally.

## Development Setup

### Environment Configuration

Create a `.env` file from the example:

```bash
cp .env.example .env
```

Required environment variables:

| Variable | Description | Example |
|----------|-------------|---------|
| `SQL_SA_PASSWORD` | SQL Server password | `YourStrongPassword123!` |
| `MONGO_PASSWORD` | MongoDB password | `YourStrongPassword123!` |
| `STRIPE_SECRET_KEY` | Stripe API secret key | `sk_test_...` |
| `STRIPE_WEBHOOK_SECRET` | Stripe webhook secret | `whsec_...` |

### Starting Development Environment

```bash
# Start all services
docker compose -f docker-compose.dev.yml up -d

# Start specific services only
docker compose -f docker-compose.dev.yml up -d front-web-dev ms-auth-dev ms-restaurant-dev

# View logs
docker compose -f docker-compose.dev.yml logs -f

# View logs for specific service
docker compose -f docker-compose.dev.yml logs -f ms-auth-dev

# Stop all services
docker compose -f docker-compose.dev.yml down

# Stop and remove volumes (clean reset)
docker compose -f docker-compose.dev.yml down -v
```

### Development Features

- Hot reload enabled where supported
- Debug-friendly configurations
- Swagger/OpenAPI available on each .NET service (`/swagger`)
- Development database with sample data seeding

## Production Setup

### Starting Production Environment

```bash
# Start all services
docker compose -f docker-compose.prod.yml up -d

# Build and start (force rebuild)
docker compose -f docker-compose.prod.yml up -d --build

# Stop all services
docker compose -f docker-compose.prod.yml down
```

### Production Features

- Optimized builds (Release configuration)
- Automatic restart policies
- Health checks on all services
- Separate network isolation
- Persistent volumes for databases

### Running Both Environments Simultaneously

You can run both development and production environments at the same time for testing:

```bash
# Start both environments
docker compose -f docker-compose.dev.yml up -d
docker compose -f docker-compose.prod.yml up -d

# Stop both environments
docker compose -f docker-compose.dev.yml down
docker compose -f docker-compose.prod.yml down
```

## Services & Ports

Both environments can run simultaneously using different port ranges.

| App | Dev | Prod |
|-----|-----|------|
| **Front Web** | [localhost:3000](http://localhost:3000) | [localhost:4000](http://localhost:4000) |
| **MS Auth** | [localhost:3001](http://localhost:3001) | [localhost:4001](http://localhost:4001) |
| **MS Restaurant** | [localhost:3002](http://localhost:3002) | [localhost:4002](http://localhost:4002) |
| **MS Paiement** | [localhost:3003](http://localhost:3003) | [localhost:4003](http://localhost:4003) |
| **MS Commandes** | [localhost:3004](http://localhost:3004) | [localhost:4004](http://localhost:4004) |
| **MS Tracking** | [localhost:3005](http://localhost:3005) | [localhost:4005](http://localhost:4005) |
| **MS SAV** | [localhost:3006](http://localhost:3006) | [localhost:4006](http://localhost:4006) |
| **MS Notifications** | [localhost:3007](http://localhost:3007) | [localhost:4007](http://localhost:4007) |
| **MS Logs** | [localhost:3008](http://localhost:3008) | [localhost:4008](http://localhost:4008) |
| **MS Stock** | [localhost:3009](http://localhost:3009) | [localhost:4009](http://localhost:4009) |
| **MS Fournisseurs** | [localhost:3010](http://localhost:3010) | [localhost:4010](http://localhost:4010) |
| **SQL Server** | localhost:1433 | localhost:1434 |
| **MongoDB** | localhost:27017 | localhost:27018 |

## Project Structure

```
goodfood/
├── .github/                    # GitHub Actions workflows
│   └── workflows/
├── goodfood-mobile/            # Cluster 0 - React Native mobile app
├── goodfood-front-web/         # Cluster 1 - React web frontend
├── goodfood-ms-auth/           # Cluster 2 - Authentication service (.NET)
├── goodfood-ms-restaurant/     # Cluster 3 - Restaurant service (Flask)
├── goodfood-ms-paiement/       # Cluster 4 - Payment service (Next.js)
├── goodfood-ms-commandes/      # Cluster 5 - Orders service (.NET)
├── goodfood-ms-tracking/       # Cluster 6 - Tracking service (.NET)
├── goodfood-ms-sav/            # Cluster 7 - Customer support service (.NET)
├── goodfood-ms-notifications/  # Cluster 8 - Notifications service (.NET)
├── goodfood-ms-logs/           # Cluster 9 - Logging service (.NET)
├── goodfood-ms-stock/          # Cluster 10 - Stock service (.NET)
├── goodfood-ms-fournisseurs/   # Cluster 11 - Suppliers service (.NET)
├── helm/                       # Kubernetes Helm charts
├── docker-compose.dev.yml      # Development environment
├── docker-compose.prod.yml     # Production environment
├── .env.example                # Environment variables template
└── README.md                   # This file
```

## API Documentation

Each .NET microservice exposes Swagger documentation:

| Service | Swagger URL (Dev) |
|---------|-------------------|
| Auth | http://localhost:3001/swagger |
| Commandes | http://localhost:3004/swagger |
| Tracking | http://localhost:3005/swagger |
| SAV | http://localhost:3006/swagger |
| Notifications | http://localhost:3007/swagger |
| Logs | http://localhost:3008/swagger |
| Stock | http://localhost:3009/swagger |
| Fournisseurs | http://localhost:3010/swagger |

## Contributing

We welcome contributions! Please follow these steps:

1. **Fork** the repository
2. **Create** a feature branch (`git checkout -b feature/amazing-feature`)
3. **Commit** your changes (`git commit -m 'Add amazing feature'`)
4. **Push** to the branch (`git push origin feature/amazing-feature`)
5. **Open** a Pull Request

### Development Guidelines

- Follow the existing code style and conventions
- Write meaningful commit messages
- Add tests for new features
- Update documentation as needed
- Ensure all CI checks pass before requesting review

### Code Quality

This project uses:
- **SonarCloud** for code quality analysis
- **GitHub Actions** for CI/CD pipelines

## License

This project is open source and available under the [MIT License](LICENSE).

---

Made with :heart: by the GoodFood team
