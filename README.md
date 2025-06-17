# CustomerAPI (.NET 9 Minimal API)

A lightweight .NET 9 RESTful API for managing customers, built with **Minimal APIs**, **CQRS**, and **MediatR**, complete with containerization, Kubernetes deployment, and a client CLI integration.


---

## 📌 Features

- ✅ Minimal APIs using ASP.NET Core
- ✅ Entity Framework Core with SQLite
- ✅ CQRS pattern with MediatR
- ✅ Integration and unit testing with xUnit
- ✅ Docker containerization
- ✅ Kubernetes manifests
- ✅ GitHub Actions CI/CD pipeline
- ✅ CLI consumer app with HttpClient

---

## 📁 Project Structure

```
CustomerAPI/             # Main .NET API project
CustomerAPI.Domain/      # Domain layer (DbContext, models)
CustomerApi.Tests/       # xUnit integration test project
CustomerApiClient/       # Console app (CLI consumer)
.github/workflows/       # CI pipeline
k8s/                     # Kubernetes YAML files
```

---

## ✅ Step-by-Step Instructions

### 1️⃣ Build & Run the API

```bash
cd CustomerAPI
dotnet restore
dotnet ef database update
dotnet run
```

SQLite DB is auto-created as `customer.db`.

---

### 2️⃣ Integration & Acceptance Testing

```bash
cd CustomerApi.Tests
dotnet test
```

Tests include full CRUD with real in-memory SQLite and a custom `WebApplicationFactory`.

---

### 3️⃣ Observability (Basic)

- Health check: `GET /health` (if enabled)
- Logging: Built-in `ILogger<T>` support
- Optionally integrate OpenTelemetry/Prometheus exporters

---

### 4️⃣ Docker Containerization

**Dockerfile path:** `CustomerAPI/Dockerfile`

```bash
docker build -t customerapi:latest -f CustomerAPI/Dockerfile .
docker run -p 5000:80 customerapi
```

---

### 5️⃣ Kubernetes Deployment

**YAML files:**
- `k8s/deployment.yaml`
- `k8s/service.yaml`

Apply:

```bash
kubectl apply -f k8s/
```

If using Minikube:

```bash
minikube service customerapi-service
```

---

### 6️⃣ CI/CD (GitHub Actions)

CI pipeline: `.github/workflows/dotnet.yml`

- Restores dependencies
- Runs build and tests
- Builds Docker image from `CustomerAPI/Dockerfile`

```yaml
dotnet test
docker build -t customerapi:latest -f CustomerAPI/Dockerfile .
```

Secrets like DockerHub credentials can be added if you want to push.

---

### 7️⃣ API Consumer CLI (CustomerApiClient)

A simple .NET console app that performs:

- `GET /api/customers`
- `POST /api/customers`
- `PUT /api/customers/{id}`
- `DELETE /api/customers/{id}`

```bash
cd CustomerApiClient
dotnet run
```

Uses `System.Net.Http.Json` and `System.Text.Json`.

---

## 🔧 Tech Stack

- .NET 9
- ASP.NET Core Minimal APIs
- Entity Framework Core + SQLite
- MediatR (CQRS)
- xUnit
- Docker
- Kubernetes
- GitHub Actions

---

