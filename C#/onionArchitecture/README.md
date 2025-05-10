# BookStore API (Onion Architecture)

BookStore is a RESTful API built with ASP.NET Core using **Onion Architecture**. It is the first project in the `Architectures/CSharp/OnionArchitecture` folder, demonstrating Onion Architecture principles such as dependency inversion, separation of concerns, and testability. The API allows users to manage books by creating and listing them.

## Table of Contents

- [Project Structure](#project-structure)
- [Technologies](#technologies)
- [Setup Instructions](#setup-instructions)
- [Running the API](#running-the-api)
- [Testing the API](#testing-the-api)
- [Onion Architecture Layers](#onion-architecture-layers)
- [Contributing](#contributing)
- [License](#license)

## Project Structure

The solution (`BookStore.sln`) is organized into four projects:

```
BookStore/
├── BookStore.Domain/          # Core entities and business logic
├── BookStore.Application/     # Application services, DTOs, and interfaces
├── BookStore.Infrastructure/  # Database access and external implementations
├── BookStore.Presentation/    # ASP.NET Core API and controllers
├── README.md
└── .gitignore
```

## Technologies

- **.NET 8.0**: Core framework.
- **ASP.NET Core**: Web API framework.
- **Entity Framework Core**: ORM with in-memory database (can be switched to SQLite).
- **Swashbuckle**: Swagger for API documentation.
- **C#**: Programming language.

## Setup Instructions

### Prerequisites

- **.NET 8 SDK**: Install from [Microsoft](https://dotnet.microsoft.com/download/dotnet/8.0).
- **VS Code**: With the C# extension installed.
- **Git**: For version control.

### Steps

1. **Clone the Repository**:

   ```bash
   git clone https://github.com/ahmed1567/Architectures.git
   cd Architectures/C#/OnionArchitecture
   ```

2. **Restore Dependencies**:

   ```bash
   dotnet restore
   ```

3. **Build the Solution**:
   ```bash
   dotnet build
   ```

## Running the API

1. **Navigate to the Presentation Project**:

   ```bash
   cd BookStore.Presentation
   ```

2. **Run the API**:

   ```bash
   dotnet run
   ```

   The API will start at `http://localhost:5141`

3. **Access Swagger UI**:
   Open `http://localhost:5141/swagger` in a browser to explore the API endpoints.

## Testing the API

Use tools like `curl`, Postman, or the Swagger UI to test the endpoints.

### Create a Book

```bash
curl -X POST http://localhost:5141/api/book \
-H "Content-Type: application/json" \
-d '{"title":"Clean Code","author":"Robert C. Martin","price":29.99}' \
--insecure
```

**Response**:

```json
{
  "id": 1,
  "title": "Clean Code",
  "author": "Robert C. Martin",
  "price": 29.99
}
```

### List All Books

```bash
curl -X GET http://localhost:5141/api/book --insecure
```

**Response**:

```json
[
  {
    "id": 1,
    "title": "Clean Code",
    "author": "Robert C. Martin",
    "price": 29.99
  }
]
```

## Onion Architecture Layers

The project follows Onion Architecture principles, with dependencies flowing inward.

### BookStore.Domain

- **Purpose**: Contains the `Book` entity and core business logic.
- **References**: None.
- **Key Files**:
  - `Book.cs`: Defines the `Book` entity with validation rules (e.g., title/author cannot be empty, price cannot be negative).

### BookStore.Application

- **Purpose**: Contains application services (`CreateBookService`, `GetAllBooksService`), DTOs (`BookDto`), and repository interfaces (`IBookRepository`).
- **References**: `BookStore.Domain`.
- **Key Files**:
  - `Dtos/BookDto.cs`: DTO for book data.
  - `Interfaces/IBookRepository.cs`: Interface for book persistence.
  - `Services/CreateBookService.cs`: Logic for creating a book.
  - `Services/GetAllBooksService.cs`: Logic for listing books.

### BookStore.Infrastructure

- **Purpose**: Implements repository interfaces and database access using Entity Framework Core.
- **References**: `BookStore.Application`, `BookStore.Domain`.
- **Key Files**:
  - `Persistence/AppDbContext.cs`: EF Core context with `DbSet<Book>`.
  - `Persistence/BookRepository.cs`: Implementation of `IBookRepository`.

### BookStore.Presentation

- **Purpose**: ASP.NET Core API with controllers to handle HTTP requests.
- **References**: `BookStore.Application`, `BookStore.Infrastructure`.
- **Key Files**:
  - `Controllers/BookController.cs`: Handles `POST /api/book` and `GET /api/book`.
  - `Program.cs`: Configures dependency injection and middleware.

### Dependency Flow

```
Presentation --> Infrastructure --> Application --> Domain
             --> Application
```
