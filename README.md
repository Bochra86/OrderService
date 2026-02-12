# OrderService

A production-grade .NET microservice built using Clean Architecture, SOLID, and DDD principles.

## Architecture

The solution is structured into the following layers:

- **API** – ASP.NET Core Web API (HTTP entry point)
- **Worker** – Background service for async processing
- **Application** – Use cases and application logic
- **Domain** – Core business rules and entities
- **Infrastructure** – Database, caching, messaging, and logging implementations

## Tech Stack (planned)

- .NET 10
- PostgreSQL (EF Core)
- Redis
- Kafka
- Serilog
- Elasticsearch + Kibana
- Docker & Docker Compose

## Status

	 Project is under active development.
