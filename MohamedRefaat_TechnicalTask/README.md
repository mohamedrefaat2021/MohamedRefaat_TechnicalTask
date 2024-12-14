
# Person Details Application

## Overview
This application is an API service that fetches person details from both CSV files and MongoDB. It exposes an API endpoint to retrieve person data and supports filtering by name. The application also includes a simple web interface to view and search the data.

## Requirements
- .NET 6 or higher
- MongoDB instance
- Access to the CSV file
- A browser to view the web interface

## How to Run This Application

### 1. Clone the repository
Clone the repository to your local machine.

```bash
git clone https://github.com/mohamedrefaat2021/MohamedRefaat_TechnicalTask.git
cd person-details-app
```

### 2. Configure the application

#### 2.1. Configuration for CSV file
In the `appsettings.json` file, provide the path to your CSV file:
```json
{
  "CsvFilePath": "path/to/your/csv/file.csv"
}
```

#### 2.2. Configuration for MongoDB
In the `appsettings.json` file, configure your MongoDB connection:
```json
{
  "MongoDb": {
    "ConnectionString": "your-mongodb-connection-string",
    "DatabaseName": "your-database-name",
    "CollectionName": "your-collection-name"
  }
}
```

### 3. Run the Application

You can run the application using the .NET CLI:
```bash
dotnet run
```

By default, the API will be hosted at `https://localhost:7161`.

### 4. Access the Web Interface
You can access the web interface by opening the browser at `https://localhost:7161`. This page will allow you to search for persons by name.

### 5. Use the API Endpoint

#### GET /api/persons
This endpoint fetches person details. You can optionally filter by the `name` query parameter.

**Request:**
```http
GET https://localhost:7161/api/persons?name=ahmed
```

**Response:**
```json
[
  {
    "firstName": "ahmed",
    "lastName": "mohamed",
    "telephoneNumber": "123-456-7890",
    "fullAddress": "123 Main St, City, Country"
  },
  ...
]
```

## Design Patterns Used
- **Dependency Injection**: Services like `CsvRepository`, `MongoRepository`, and `PersonService` are injected into the controller and other services using the built-in DI container in .NET.
- **Repository Pattern**: Both `CsvRepository` and `MongoRepository` are responsible for abstracting the data access layer. They fetch data from the respective sources (CSV and MongoDB).
- **Service Layer Pattern**: The `PersonService` class acts as a service layer that contains the business logic of fetching, transforming, and filtering person data.

## Architecture
The application follows a simple layered architecture:

1. **Web Layer (API Controller)**: The `PersonsController` handles HTTP requests and returns responses to the client. It communicates with the `PersonService` to fetch the required data.
2. **Service Layer**: The `PersonService` handles the business logic for transforming and combining data from CSV and MongoDB.
3. **Repository Layer**: The `CsvRepository` and `MongoRepository` abstract the logic for reading from the CSV file and MongoDB, respectively.
4. **Data Models**: The `Person` class represents the unified data model that is used across different layers of the application.

## Running the Application with MongoDB and CSV
1. The `CsvRepository` reads data from a CSV file.
2. The `MongoRepository` fetches data from a MongoDB database.
3. The `PersonService` transforms data from both sources into a unified format and applies any filters if necessary.

## Folder Structure
```bash
.
├── Controllers
│   └── PersonsController.cs
├── Models
│   └── Person.cs
│   └── CsvPerson.cs
│   └── MongoPerson.cs
├── Repositories
│   └── CsvRepository.cs
│   └── MongoRepository.cs
├── Services
│   └── PersonService.cs
└── appsettings.json
```

## Notes
- Ensure that MongoDB is running and accessible at the configured connection string.
- The CSV file should have the necessary columns and data.
- You can adjust the MongoDB connection settings and the CSV file path as needed.

