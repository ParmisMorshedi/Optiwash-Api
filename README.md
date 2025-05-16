# 🚗 OptiWash Backend API

OptiWash is a backend API built with ASP.NET Core for managing and tracking internal operations in car wash businesses. Unlike customer-facing booking systems, this platform is designed for **business owners** to plan, monitor, and follow up on vehicle washes – especially for companies with large fleets.

---

## ✨ Features

- API endpoints for managing organizations, cars, and wash records
- Secure user registration and login with ASP.NET Core Identity
- Manage multiple organizations and their vehicle fleets
- Add, update, and delete cars linked to organizations
- Track wash records (interior/exterior) per vehicle
- Mark wash records as completed, failed, or pending
- Monthly summary of completed and not completed washes per organization
- Clean architecture: Controller → Service → Repository
- Full Swagger UI for API testing and documentation
- Unit testing with xUnit and Moq

---

## 🛠 Technologies Used

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- SQL Server
- Swagger (Swashbuckle)
- xUnit + Moq (unit testing)

## 📦 NuGet Packages

The project uses the following main NuGet packages:

- `Microsoft.EntityFrameworkCore.SqlServer`– for database integration  
- `Microsoft.EntityFrameworkCore`- ORM for database interactions
- `Microsoft.EntityFrameworkCore.Tools` - Tools for Entity Framework Core, used for database migrations and scaffolding.
- `Microsoft.EntityFrameworkCore.Design` - Design-time tools for Entity Framework Core, used for migrations and database updates.
- `Microsoft.AspNetCore.Authentication.JwtBearer` – for JWT-based authentication  
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore` – provides ASP.NET Core Identity support using EF Core, including user and role management  
- `xunit`, `moq` – for unit testing  


---

## 📦 Getting Started

### Prerequisites

- .NET 8 SDK 
- SQL Server (local or cloud)
- Visual Studio 

### Installation Steps

1. **Clone the repository**

   ```bash
   git clone https://github.com/ParmisMorshedi/OptiWash_Api.git
   cd OptiWash-Backend

2. **Update appsettings.json**

   ```bash
   "ConnectionStrings": {
   "DefaultConnection": "Server=YOUR_SERVER;Database=OptiWashDb;"
   }
   
3. **Apply database migrations**

   ```bash
   dotnet ef database update

4. **Run the project**
   
   ```bash
   dotnet run
5. **Open Swagger UI**

---

## ⚙️ Configuration

- All database settings are found in `appsettings.json`
- You can change environment settings in `launchSettings.json`

---

## 📚 API Documentation

The API is fully documented using Swagger UI. Once the app is running, visit:

    http://localhost:5000/swagger

### 📌 Example Endpoints

- `GET /api/WashRecords`
- `POST /api/WashRecords`
- `GET /api/WashRecords/car/{carId}`
- `DELETE /api/WashRecords/{id}`

---

### 🧪 Testing

Unit tests are written using **xUnit** and **Moq**.

#### ▶️ Run tests

    dotnet test

### ✅ Test Coverage Includes

- WashRecordService logic  
- Car and Organization services  
- Data handling and exception behavior

---
### 👤 Author

Developed by **Parmis Morshedi**  
Final project (Examensarbete) – [Chas Academy](https://chasacademy.se)

[GitHub](https://github.com/ParmisMorshedi) • [LinkedIn](www.linkedin.com/in/parmis-morshedi-b1280b28b)



