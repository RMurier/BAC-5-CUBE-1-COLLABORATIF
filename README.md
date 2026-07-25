# GoodFood

An open-source food delivery platform inspired by Uber Eats, built with a microservices architecture. Each microservice lives in its own GitHub repository, pulled into this repo as a git submodule.

## Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Implementation Status](#implementation-status)
- [Tech Stack](#tech-stack)
- [Quick Start](#quick-start)
- [Development Setup](#development-setup)
- [Production Setup](#production-setup)
- [Services & Ports](#services--ports)
- [Project Structure](#project-structure)
- [Microservices](#microservices)
- [API Documentation](#api-documentation)
- [CI/CD Pipeline](#cicd-pipeline)
- [Security](#security)
- [Contributing](#contributing)
- [License](#license)

## Overview

GoodFood is a food delivery ecosystem being built as a microservices learning/demo project. The target scope covers:

- Customer-facing web and mobile applications
- Restaurant management system
- Real-time order tracking
- Payment processing (Stripe)
- Inventory and supplier management
- Customer support ticketing system
- Centralized logging and notifications

Not every service is fully built yet — see [Implementation Status](#implementation-status) for what's real today versus what's scaffolding waiting to be filled in. What **is** fully built out, regardless of feature scope, is the CI/CD and security pipeline shared by every service — see [CI/CD Pipeline](#cicd-pipeline) and [Security](#security).

## Architecture

The platform is organized into 12 independent services, each in its own repository:

| Service | Repository | Description |
|---------|------------|--------------|
| Mobile App | [goodfood-mobile](https://github.com/RMurier/goodfood-mobile) | React Native customer app |
| Front Web | [goodfood-front-web](https://github.com/RMurier/goodfood-front-web) | React web application |
| Auth | [goodfood-ms-auth](https://github.com/RMurier/goodfood-ms-auth) | Authentication & authorization |
| Restaurant | [goodfood-ms-restaurant](https://github.com/RMurier/goodfood-ms-restaurant) | Restaurant, menu & item management |
| Paiement | [goodfood-ms-paiement](https://github.com/RMurier/goodfood-ms-paiement) | Payment processing (Stripe) |
| Commandes | [goodfood-ms-commandes](https://github.com/RMurier/goodfood-ms-commandes) | Order management |
| Tracking | [goodfood-ms-tracking](https://github.com/RMurier/goodfood-ms-tracking) | Real-time delivery tracking |
| SAV | [goodfood-ms-sav](https://github.com/RMurier/goodfood-ms-sav) | Customer support & ticketing |
| Notifications | [goodfood-ms-notifications](https://github.com/RMurier/goodfood-ms-notifications) | Push & email notifications |
| Logs | [goodfood-ms-logs](https://github.com/RMurier/goodfood-ms-logs) | Centralized logging |
| Stock | [goodfood-ms-stock](https://github.com/RMurier/goodfood-ms-stock) | Inventory management |
| Fournisseurs | [goodfood-ms-fournisseurs](https://github.com/RMurier/goodfood-ms-fournisseurs) | Supplier management |

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

Each microservice repository ships its own `Dockerfile` (production) and `Dockerfile.dev` (hot-reload dev image), and gets built, scanned, tested and published independently by the [CI/CD pipeline](#cicd-pipeline) in this parent repo.

## Implementation Status

Being upfront about what's actually implemented versus scaffolding, so you don't go looking for business logic that isn't there yet:

| Service | Status | Notes |
|---------|--------|-------|
| [goodfood-ms-auth](https://github.com/RMurier/goodfood-ms-auth) | ✅ Implemented | Register / login / refresh / logout, JWT + BCrypt — see [its README](https://github.com/RMurier/goodfood-ms-auth#readme) |
| [goodfood-front-web](https://github.com/RMurier/goodfood-front-web) | ✅ Implemented | Home + Auth pages, wired to ms-auth — see [its README](https://github.com/RMurier/goodfood-front-web#readme) |
| [goodfood-ms-restaurant](https://github.com/RMurier/goodfood-ms-restaurant) | 🚧 Scaffold | Flask app with only a `/health` route so far |
| [goodfood-ms-paiement](https://github.com/RMurier/goodfood-ms-paiement) | 🚧 Scaffold | Default `create-next-app` output, no Stripe integration yet |
| [goodfood-ms-commandes](https://github.com/RMurier/goodfood-ms-commandes) | 🚧 Scaffold | Default ASP.NET Core Web API template |
| [goodfood-ms-tracking](https://github.com/RMurier/goodfood-ms-tracking) | 🚧 Scaffold | Default ASP.NET Core Web API template |
| [goodfood-ms-sav](https://github.com/RMurier/goodfood-ms-sav) | 🚧 Scaffold | Default ASP.NET Core Web API template |
| [goodfood-ms-notifications](https://github.com/RMurier/goodfood-ms-notifications) | 🚧 Scaffold | Default ASP.NET Core Web API template |
| [goodfood-ms-logs](https://github.com/RMurier/goodfood-ms-logs) | 🚧 Scaffold | Default ASP.NET Core Web API template |
| [goodfood-ms-stock](https://github.com/RMurier/goodfood-ms-stock) | 🚧 Scaffold | Default ASP.NET Core Web API template |
| [goodfood-ms-fournisseurs](https://github.com/RMurier/goodfood-ms-fournisseurs) | 🚧 Scaffold | Default ASP.NET Core Web API template |
| [goodfood-mobile](https://github.com/RMurier/goodfood-mobile) | 🚧 Scaffold | Default Expo template, not wired into CI/CD yet |

All 12 services (mobile included) already have: a `Dockerfile`/`Dockerfile.dev`, a test project, and a place in `docker-compose.dev.yml`/`docker-compose.prod.yml` — the plumbing is ready for business logic to land in any of them at any time. The 11 wired into CI (everything but mobile) are already built, scanned and gated on every push, whether they contain three lines of code or three hundred.

## Tech Stack

| Component | Technology |
|-----------|------------|
| Frontend Web | React 19, TypeScript, Vite |
| Mobile | React Native, Expo |
| Backend (.NET services) | C# / .NET 9, ASP.NET Core |
| Backend (Restaurant) | Python, Flask, Gunicorn |
| Backend (Paiement) | Next.js, Stripe API (planned) |
| SQL Database | Microsoft SQL Server 2022 |
| NoSQL Database | MongoDB 7 |
| Containerization | Docker, Docker Compose |
| Orchestration | Kubernetes, Helm |
| CI/CD | GitHub Actions |
| Code quality | Self-hosted [SonarQube](https://sonar.romainmurier.com) |
| Security scanning | GitGuardian (secrets), Trivy (container images), OWASP Dependency-Check (dependencies) |

## Quick Start

### Prerequisites

- [Docker](https://docs.docker.com/get-docker/) (v20.10+)
- [Docker Compose](https://docs.docker.com/compose/install/) (v2.0+)
- [Git](https://git-scm.com/downloads)

### 1. Clone the repository

Every microservice is a git submodule — clone with `--recurse-submodules`, or the service directories will be empty:

```bash
git clone --recurse-submodules https://github.com/RMurier/BAC-5-CUBE-1-COLLABORATIF.git
cd BAC-5-CUBE-1-COLLABORATIF
```

Already cloned without it? Fetch the submodules after the fact:

```bash
git submodule update --init --recursive
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
| `STRIPE_SECRET_KEY` | Stripe API secret key (ms-paiement, not yet consumed by code) | `sk_test_...` |
| `STRIPE_WEBHOOK_SECRET` | Stripe webhook secret (ms-paiement, not yet consumed by code) | `whsec_...` |

`docker-compose.dev.yml` also hardcodes a `Jwt__Key` for ms-auth and default SQL/Mongo passwords directly in the compose file — that's fine for local dev, but never reuse those values anywhere real; see [Security](#security).

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
- Separate network isolation
- Persistent volumes for databases

Actual cluster deployment (K3s via Helm, see [`helm/goodfood`](helm/goodfood)) is scaffolded in the CI/CD pipeline but not yet wired up — see [CI/CD Pipeline](#cicd-pipeline).

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
BAC-5-CUBE-1-COLLABORATIF/
├── .github/workflows/           # CI/CD pipeline (see CI/CD Pipeline below)
│   ├── ci-cd.yml                 # Main pipeline: build, scan, test, publish, deploy
│   └── bump-submodule.yml        # Reusable workflow called by each microservice repo
├── goodfood-mobile/             # React Native mobile app (submodule)
├── goodfood-front-web/          # React web frontend (submodule)
├── goodfood-ms-auth/            # Authentication service, .NET (submodule)
├── goodfood-ms-restaurant/      # Restaurant service, Flask (submodule)
├── goodfood-ms-paiement/        # Payment service, Next.js (submodule)
├── goodfood-ms-commandes/       # Orders service, .NET (submodule)
├── goodfood-ms-tracking/        # Tracking service, .NET (submodule)
├── goodfood-ms-sav/             # Customer support service, .NET (submodule)
├── goodfood-ms-notifications/   # Notifications service, .NET (submodule)
├── goodfood-ms-logs/            # Logging service, .NET (submodule)
├── goodfood-ms-stock/           # Stock service, .NET (submodule)
├── goodfood-ms-fournisseurs/    # Suppliers service, .NET (submodule)
├── helm/                        # Kubernetes Helm charts
├── docker-compose.dev.yml       # Development environment
├── docker-compose.prod.yml      # Production environment
├── .env.example                 # Environment variables template
└── README.md                    # This file
```

## Microservices

Each service has its own README with its actual endpoints, environment variables, run instructions and service-specific security notes:

- [goodfood-mobile](https://github.com/RMurier/goodfood-mobile#readme)
- [goodfood-front-web](https://github.com/RMurier/goodfood-front-web#readme)
- [goodfood-ms-auth](https://github.com/RMurier/goodfood-ms-auth#readme)
- [goodfood-ms-restaurant](https://github.com/RMurier/goodfood-ms-restaurant#readme)
- [goodfood-ms-paiement](https://github.com/RMurier/goodfood-ms-paiement#readme)
- [goodfood-ms-commandes](https://github.com/RMurier/goodfood-ms-commandes#readme)
- [goodfood-ms-tracking](https://github.com/RMurier/goodfood-ms-tracking#readme)
- [goodfood-ms-sav](https://github.com/RMurier/goodfood-ms-sav#readme)
- [goodfood-ms-notifications](https://github.com/RMurier/goodfood-ms-notifications#readme)
- [goodfood-ms-logs](https://github.com/RMurier/goodfood-ms-logs#readme)
- [goodfood-ms-stock](https://github.com/RMurier/goodfood-ms-stock#readme)
- [goodfood-ms-fournisseurs](https://github.com/RMurier/goodfood-ms-fournisseurs#readme)

## API Documentation

Each .NET microservice exposes Swagger documentation in development:

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

Only `ms-auth` currently has real endpoints behind that Swagger page — see [its README](https://github.com/RMurier/goodfood-ms-auth#readme) for the full list. The rest currently only expose the default `/weatherforecast` sample endpoint from the ASP.NET Core template.

## CI/CD Pipeline

Defined in [`.github/workflows/ci-cd.yml`](.github/workflows/ci-cd.yml), triggered on every push to `develop` and `master`. The design goal: **nothing reaches the container registry or gets deployed until every check has passed** — not the other way around.

```
push to develop/master
        │
        ▼
┌─────────────────────┐
│   detect-changes     │  dorny/paths-filter diffs the push and works out which
│                       │  submodule pointers actually moved. A push that only
│                       │  touches goodfood-ms-auth only triggers work for
│                       │  ms-auth — the other 10 services are skipped entirely.
│                       │  A change to ci-cd.yml itself forces a full run on
│                       │  every service, as a safety net.
└──────────┬───────────┘
           │
   ┌───────┴────────────────────────────────────────┐
   │           (all run in parallel)                  │
   ▼               ▼              ▼             ▼      ▼
┌─────────┐  ┌───────────┐  ┌──────────┐  ┌────────┐ ┌───────────┐
│  build   │  │ sonarqube │  │dependency│  │gitguard│ │unit-tests │
│ + trivy  │  │           │  │  -check  │  │  ian   │ │           │
└────┬─────┘  └─────┬─────┘  └────┬─────┘  └───┬────┘ └─────┬─────┘
     │              │             │            │            │
     └──────────────┴─────────────┴────────────┴────────────┘
                              │
                              ▼  (only if ALL of the above succeeded)
                       ┌─────────────┐
                       │   publish    │  pushes the image to GHCR
                       └──────┬──────┘
                              ▼
                       ┌─────────────┐
                       │   deploy     │  develop → dev namespace, master → prod
                       │ (TODO stub)  │  Helm/kubeconfig wiring not done yet
                       └─────────────┘
```

**Why build-then-scan-before-push, not scan-after-push:** the previous design ran the image scan *after* publishing to GHCR, meaning a vulnerable image could already be public before anyone found out. `build` now builds the image locally (`load: true`, never pushed) and runs the Trivy scan against that local image immediately — if it fails, nothing about that image ever leaves the runner.

Jobs, in short:

| Job | What it checks | Blocks publish? |
|-----|------------------|:---:|
| `build` | Builds the Docker image, then scans it with **Trivy** for `HIGH`/`CRITICAL` fixable CVEs in the OS layer and dependencies | ✅ |
| `sonarqube` | Static code analysis against the self-hosted SonarQube instance (one project per service) | ✅ |
| `dependency-check` | **OWASP Dependency-Check** — known CVEs in application dependencies (npm/NuGet/pip), fails above CVSS 7 | ✅ |
| `gitguardian` | Scans the pushed commits for leaked secrets across the whole repo | ✅ |
| `unit-tests` | Runs each service's test suite (dotnet test / pytest / vitest / jest) | ✅ |
| `publish` | Pushes the already-scanned image to GHCR (`ghcr.io/rmurier/goodfood-<service>`) | — |
| `deploy-dev` / `deploy-prod` | K3s deployment via Helm — currently a `TODO` stub, not yet live | — |

### Cross-repo trigger: pushing to a microservice repo

Since each microservice lives in its own repository with its own CI, pushing directly to e.g. `goodfood-ms-auth` does **not** by itself trigger anything here — GitHub Actions workflows don't cross repository boundaries automatically. The chain that makes it work:

1. You push to a microservice repo's `main` branch.
2. That repo's own `.github/workflows/bump-parent.yml` calls the reusable workflow [`bump-submodule.yml`](.github/workflows/bump-submodule.yml) hosted here, authenticated with a fine-grained `PARENT_REPO_PAT` (Contents: Read & write, scoped to this repo only).
3. It fast-forwards that one submodule pointer and pushes the bump commit to `develop` here.
4. That push is what actually triggers `ci-cd.yml`, and `detect-changes` picks up exactly that one service.

So: pushing to a microservice repo → automatic pointer bump here → automatic scoped CI run. No manual submodule bump needed day-to-day.

## Security

### Application-level

- **Passwords**: hashed with BCrypt (`BCrypt.Net-Next`) in `ms-auth`, never stored or logged in plaintext.
- **Sessions**: short-lived JWT access tokens (HMAC-SHA256, 60 min default) plus a separate cryptographically random refresh token (7-day expiry, single active token per user, revoked on logout). See [goodfood-ms-auth](https://github.com/RMurier/goodfood-ms-auth#readme) for the full flow.
- **Known tradeoff**: `goodfood-front-web` currently stores tokens in `localStorage` (readable by any script on the page, i.e. vulnerable to token theft via XSS) rather than an `httpOnly` cookie. Acceptable for a dev/demo build; worth revisiting before anything resembling production traffic.
- Local dev credentials (`docker-compose.dev.yml` SQL/Mongo passwords, JWT signing key) are hardcoded placeholders — fine for `localhost`, must never be reused in a real environment.

### Pipeline / supply-chain level

Every push is checked by four independent, purpose-built tools before an image can be published — see [CI/CD Pipeline](#cicd-pipeline) for how they're wired together:

| Tool | Catches |
|------|---------|
| [SonarQube](https://sonar.romainmurier.com) (self-hosted) | Code smells, bugs, static-analysis-detectable vulnerabilities — one project per service |
| [GitGuardian](https://dashboard.gitguardian.com) | Secrets committed to git history (API keys, passwords, tokens) |
| [Trivy](https://trivy.dev) | Known CVEs in the container image: OS packages and application dependencies |
| [OWASP Dependency-Check](https://owasp.org/www-project-dependency-check/) | Known CVEs in application dependency manifests (npm/NuGet/pip), CVSS ≥ 7 fails the build |

GitHub's native **secret scanning** and **push protection** are also enabled on every repository (all public), catching credential leaks at push time — before they even land in git history, ahead of GitGuardian's CI-time scan.

### Secrets management

- All CI credentials (SonarQube tokens, GitGuardian API key, GHCR token, `PARENT_REPO_PAT`) live in GitHub Actions repo secrets — never committed, never logged.
- Each microservice has its own scoped SonarQube analysis token (`SONAR_TOKEN_MS_*`) rather than one shared token, so a leaked token only exposes one project's analysis rights.
- `PARENT_REPO_PAT` (used by the [cross-repo bump mechanism](#cross-repo-trigger-pushing-to-a-microservice-repo)) is a fine-grained PAT scoped to exactly one repository with `Contents: Read and write` — nothing else. It cannot touch any other repo or setting.

### Known gaps

- Checks currently only run on `push` to `develop`/`master`, not on pull requests — a PR can be merged before any scan has run against it. Branch protection requiring these checks as required status checks is a natural next step.
- `deploy-dev`/`deploy-prod` are stubs; once real, secrets like `KUBECONFIG` will need the same scoped-secret treatment as everything above.

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
- Ensure all CI checks pass before requesting review (see [CI/CD Pipeline](#cicd-pipeline))

## License

This project is open source and available under the [MIT License](LICENSE).

---

Made with :heart: by the GoodFood team
