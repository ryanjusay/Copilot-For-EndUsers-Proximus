# Wright Brothers API

Minimal REST API backend for managing an in‑memory catalog of historic Wright Brothers aircraft.

### Tech Stack
- .NET (ASP.NET Core Web API)
- C# (nullable + modern practices)
- Minimal in‑memory data store (static list)

### Core Endpoints (Planes)
- GET /planes – list all planes
- GET /planes/{id} – get plane by id
- GET /planes/count – count planes
- GET /planes/year/{year} – filter by year
- POST /planes – add a plane
- PUT /planes/{id} – update a plane
- DELETE /planes/{id} – remove a plane

Plane model: Id, Name, Year, Description, RangeInKm, ImageUrl.

### Quick Start
1. Restore & build: `dotnet build`
2. Run: `dotnet run --project WrightBrothersApi`
3. Browse Swagger UI (if enabled) at: https://localhost:5001/swagger or http://localhost:5000/swagger

### Development Notes
- Data is not persisted; restart resets the list.
- Add validation / persistence as needed for production scenarios.

### Contributing
Open a PR with a concise description. Keep README minimal.

### License
See root `LICENSE.md`.

### References
- This example is based on the awesome work of Thijs Limmen, Xebia Netherlands, [GitHub](ThijSlim)