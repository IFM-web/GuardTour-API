# GuardTour API

**Description**  
This API supports guard tour management for IFM (facility management) systems. It provides endpoints for login, guard check-in/out, tour logs, etc.

**Features**  
- Login endpoint for guards  
- Start/stop tour, record checkpoints  
- Fetch tour reports  
- … etc

**Getting Started**  
**Prerequisites**  
- .NET version X  
- SQL Server (or whichever DB)  
- Environment variables: `DB_CONNECTION`, `JWT_KEY`, etc

**Installation**  
```bash
git clone https://github.com/YourOrg/GuardTour-API.git
cd GuardTour-API
dotnet restore
dotnet run
