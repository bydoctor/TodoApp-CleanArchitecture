# 🚀 TodoApp - Clean Architecture Web API

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat\&logo=dotnet)
![Architecture](https://img.shields.io/badge/Architecture-Clean-green)
![License](https://img.shields.io/badge/License-MIT-blue)

A robust, scalable, and testable RESTful Web API built with **.NET 10**, implementing **Clean Architecture** principles. This project demonstrates best practices in software design, separating concerns into distinct layers to avoid the "Fat Controller" anti-pattern.

## 📖 Overview

This project serves as a practical example of how to build enterprise-level applications using modern .NET technologies. It enforces strict separation of concerns:

* **Controllers** manage HTTP requests only.
* **Services** handle all business logic and validations.
* **Repositories** abstract the data access layer.

## 🏗 Architecture

The solution follows the **Clean Architecture (Onion Architecture)** approach, ensuring that the core business logic depends on nothing but itself.

### Folder Structure

```text
TodoApp
├── 📂 Domain                # 🏠 Core Entities (No dependencies)
│   └── 📂 Entities          # Database models (TodoItem)
│
├── 📂 Application           # 🧠 Business Logic & Rules
│   ├── 📂 DTOs              # Data Transfer Objects
│   ├── 📂 Interfaces        # Contracts (ITodoService, ITodoRepository)
│   ├── 📂 Services          # Business implementation
│   └── 📂 Validators        # FluentValidation rules
│
├── 📂 Infrastructure        # 🏗 Database & External Concerns
│   ├── 📂 Data              # EF Core DbContext
│   └── 📂 Repositories      # Data access implementation
│
└── 📂 Controllers           # 🌐 API Entry Points
    └── TodosController.cs   # Handles HTTP requests
```

## 🛠 Tech Stack & Features

* **Framework**: .NET 10 (ASP.NET Core Web API)
* **Database**: SQLite
* **ORM**: Entity Framework Core (Code-First Migrations)
* **Validation**: FluentValidation
* **Documentation**: Swagger UI (Swashbuckle)
* **Dependency Injection**: Built-in ASP.NET Core DI
* **Design Patterns**: Clean Architecture, Repository Pattern, N-Tier Architecture

## 🚀 Getting Started

Follow these instructions to get a copy of the project up and running on your local machine.

### Prerequisites

* .NET 10 SDK installed
* An IDE such as Visual Studio, Rider, or VS Code
* Git installed

### Installation Steps

#### 1️⃣ Clone the repository

```bash
git clone https://github.com/bydoctor/TodoApp-CleanArchitecture.git
cd TodoApp-CleanArchitecture
```

#### 2️⃣ Restore dependencies

This command downloads all required NuGet packages.

```bash
dotnet restore
```

#### 3️⃣ Apply database migrations

The project uses SQLite. This command will generate the `Todos.db` file based on the entities.

```bash
dotnet ef database update
```

#### 4️⃣ Run the project

```bash
dotnet run --project TodoApi
```

Or simply press the **Run** button in your IDE.

### Access the API

Once running, open your browser to view the Swagger documentation:

```
http://localhost:5xxx/swagger
```

(Check your terminal for the exact port.)

## 🔌 API Endpoints

| Method | Endpoint        | Description       |
| -----: | --------------- | ----------------- |
|    GET | /api/todos      | Get all tasks     |
|    GET | /api/todos/{id} | Get task by ID    |
|   POST | /api/todos      | Create a new task |
|    PUT | /api/todos/{id} | Update task       |
| DELETE | /api/todos/{id} | Delete task       |

### Payload Examples

**Create**

```json
{ "title": "Buy Milk" }
```

**Update**

```json
{ "title": "Buy Milk", "isCompleted": true }
```

## 🛡 Validations

Data integrity is ensured using **FluentValidation**. 
> Validations are intentionally handled in the Service Layer to keep
> business rules independent from transport concerns (HTTP).


### Example Rules

* Title:

    * Cannot be empty
    * Must be at least 1 character long

If a request fails validation, the API returns a **400 Bad Request** response with detailed error messages.

## 🤝 Contributing

Contributions, issues, and feature requests are welcome!

1. Fork the project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the **MIT License**.

---

Happy Coding! 💙
