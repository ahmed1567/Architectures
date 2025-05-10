TaskManager API
TaskManager is a RESTful API built with ASP.NET Core using Clean Architecture. It allows users to manage jobs (tasks) by creating and listing them. The project is designed to demonstrate Clean Architecture principles, with a focus on separation of concerns, testability, and maintainability. The entity is named Job instead of Task to avoid naming conflicts with System.Threading.Tasks.Task.
Table of Contents

Project Structure
Technologies
Setup Instructions
Running the API
Testing the API
Clean Architecture Layers
Contributing
License

Project Structure
The solution (TaskManager.sln) is organized into four projects, following Clean Architecture:
TaskManager/
├── TaskManager.Domain/ # Core entities and business logic
├── TaskManager.Application/ # Use cases, DTOs, and repository interfaces
├── TaskManager.Infrastructure/ # Database access and external implementations
├── TaskManager.WebApi/ # ASP.NET Core API and controllers
├── README.md
├── .gitignore
└── TaskManager.sln

Technologies

.NET 8.0: Core framework.
ASP.NET Core: Web API framework.
Entity Framework Core: ORM with in-memory database (can be switched to SQLite).
Swashbuckle: Swagger for API documentation.
C#: Programming language.

Setup Instructions
Prerequisites

.NET 8 SDK: Install from Microsoft.
VS Code: With the C# extension installed.
Git: For version control.

Steps

Clone the Repository (if using a remote repository):
git clone https://github.com/ahmed1567/Architectures.git
cd TaskManager

If working locally, navigate to the project directory:
cd ~/Architecture/C#/cleanArchitecture

Restore Dependencies:
dotnet restore

Build the Solution:
dotnet build

Running the API

Navigate to the WebApi Project:
cd TaskManager.WebApi

Run the API:
dotnet run

The API will start at http://localhost:5059

Access Swagger UI:Open http://localhost:5059/swagger in a browser to explore the API endpoints.

Testing the API
Use tools like curl, Postman, or the Swagger UI to test the endpoints.
Create a Job
curl -X POST http://localhost:5059/api/job \
-H "Content-Type: application/json" \
-d '{"title":"Learn Clean Architecture","description":"Build a Task Manager API"}' \
--insecure

Response:
{
"id": 1,
"title": "Learn Clean Architecture",
"description": "Build a Task Manager API",
"isCompleted": falsehood,
"createdAt": "2025-05-10T13:30:00Z"
}

Response:
[
{
"id": 1,
"title": "Learn Clean Architecture",
"description": "Build a Task Manager API",
"isCompleted": false,
"createdAt": "2025-05-10T13:30:00Z"
}
]

Clean Architecture Layers
The project follows Clean Architecture principles, with dependencies flowing inward.
TaskManager.Domain

Purpose: Contains the Job entity and core business logic.
References: None.
Key Files:
Job.cs: Defines the Job entity with validation rules (e.g., title cannot be empty).

TaskManager.Application

Purpose: Contains use cases (CreateJobUseCase, GetAllJobsUseCase), DTOs (JobDto), and repository interfaces (IJobRepository).
References: TaskManager.Domain.
Key Files:
Dtos/JobDto.cs: DTO for job data.
Ports/IJobRepository.cs: Interface for job persistence.
UseCases/CreateJobUseCase.cs: Logic for creating a job.
UseCases/GetAllJobsUseCase.cs: Logic for listing jobs.

TaskManager.Infrastructure

Purpose: Implements repository interfaces and database access using Entity Framework Core.
References: TaskManager.Application, TaskManager.Domain.
Key Files:
Persistence/AppDbContext.cs: EF Core context with DbSet<Job>.
Persistence/JobRepository.cs: Implementation of IJobRepository.

TaskManager.WebApi

Purpose: ASP.NET Core API with controllers to handle HTTP requests.
References: TaskManager.Application, TaskManager.Infrastructure.
Key Files:
Controllers/JobController.cs: Handles POST /api/job and GET /api/job.
Program.cs: Configures dependency injection and middleware.

Dependency Flow
WebApi --> Infrastructure --> Application --> Domain
Infrastructure --> Domain
