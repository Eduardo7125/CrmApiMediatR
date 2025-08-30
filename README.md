# CRM API Mediator

A modular, scalable CRM backend built with ASP.NET Core, MediatR, Entity Framework Core, and Clean Architecture principles.  
The goal is to provide a robust foundation for a complete CRM system, supporting client and opportunity management, extensible business logic, and modern development practices.
STILL IN DEVELOPMENT

---

## 🚀 Quick Start for Developers

1. **Clone the repository**
   ```bash
   git clone https://github.com/eduar/crm-api-mediatr.git
   cd crm-api-mediatr/CrmApiMediatR
   ```

2. **Configure the database**
   - Update `Api/appsettings.json` with your PostgreSQL/Supabase credentials:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR-HOST;Port=5432;Database=YOUR-DB;User Id=YOUR-USER;Password=YOUR-PASSWORD;"
     }
     ```

3. **Restore dependencies**
   ```bash
   dotnet restore
   ```

4. **Apply migrations (if needed)**
   ```bash
   dotnet ef database update --project Infrastructure
   ```

5. **Run the API**
   ```bash
   dotnet run --project Api
   ```
   - The API will be available at [https://localhost:7190/swagger](https://localhost:7190/swagger) (see launchSettings.json).

---

## 🏗️ Architecture Overview

- **Clean Architecture**: Separation of concerns across layers:
  - **Api**: Minimal API endpoints, Swagger, HTTP configuration.
  - **Application**: Business logic, MediatR requests/handlers, validation, mapping.
  - **Domain**: Core entities, exceptions, enums.
  - **Infrastructure**: Persistence (EF Core), repositories, dependency injection.

- **CQRS with MediatR**:  
  All commands and queries are handled via MediatR, enabling pipeline behaviors for logging, validation, and performance monitoring.

- **Entity Framework Core**:  
  Supports PostgreSQL/Supabase out of the box.  
  Entities: `Client`, `Opportunity`.

- **Validation**:  
  FluentValidation for all commands and queries.

---

## 📦 Dependencies

- [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [MediatR](https://github.com/jbogard/MediatR)
- [AutoMapper](https://automapper.org/)
- [FluentValidation](https://fluentvalidation.net/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [Npgsql](https://www.npgsql.org/) (PostgreSQL provider)
- [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) (Swagger)

---

## 🧩 Main Features

- **Client CRUD**: Create, read, update, delete clients.
- **Opportunity Management**: Link opportunities to clients.
- **Validation & Error Handling**: Consistent, extensible.
- **Swagger UI**: Interactive API documentation.
- **Pipeline Behaviors**: Logging, performance, validation.

---

## 🛠️ Extending the Project

- Add new entities to `Domain/Entities`.
- Implement new features using MediatR commands/queries in `Application/Features`.
- Register new services in `Application/DependencyInjection.cs` or `Infrastructure/DependencyInjection.cs`.

---

## 💡 Contribution

Pull requests and issues are welcome!  
Feel free to fork and adapt for your own CRM needs.

---

## 📬 Contact & Social

- **Email:** [eduardocristea55@gmail.com](mailto:eduardocristea55@gmail.com)
- **LinkedIn:** [Eduardo Cristea Petrica](https://www.linkedin.com/in/eduardo-petrica-cristea-384b28291/)

---

## 📄 License

MIT License. See [LICENSE](LICENSE).

---

**Enjoy building your CRM!**
