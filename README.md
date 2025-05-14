# 🚗 OptiWash Backend API

OptiWash is a backend API built with ASP.NET Core for managing and tracking internal operations in car wash businesses. Unlike customer-facing booking systems, this platform is designed for **business owners** to plan, monitor, and follow up on vehicle washes – especially for companies with large fleets.

---

## ✨ Features

- Manage multiple organizations and their vehicle fleets
- Track wash records (interior/exterior) per vehicle
- Mark wash completion status
- Monthly view of pending/completed washes
- View history and register missed or failed washes
- Clean architecture: Controller → Service → Repository
- Full Swagger UI for API testing
- xUnit + Moq unit testing

---

## 🛠 Technologies Used

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- SQL Server
- Swagger (Swashbuckle)
- xUnit + Moq (unit testing)

## 📦 NuGet Packages

The project uses the following main NuGet packages:

- `Microsoft.EntityFrameworkCore.SqlServer` 
- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.Tools`
- `Microsoft.EntityFrameworkCore.Design`
- `Microsoft.AspNetCore.Authentication.JwtBearer` 
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
- `xunit`, `moq` 


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



