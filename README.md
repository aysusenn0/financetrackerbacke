# 💰 FinanceTracker Backend API

A robust RESTful API for personal finance management, built with **.NET 8** using **Clean Architecture** principles. This system allows users to track incomes/expenses, categorize transactions (Fixed/Variable), and handle multi-currency logic with real-time exchange rates.

## 🚀 Key Features

* **Clean Architecture (Onion):** Separation of concerns with Domain, Application, Infrastructure, and API layers.
* **CQRS Pattern:** Command Query Responsibility Segregation using MediatR.
* **Real-time Exchange Rates:** Automated fetching of USD/EUR rates from **TCMB (Central Bank of Turkey)**.
* **High Performance:** **Redis** caching strategy for currency data.
* **Data Integrity:** MSSQL database with 5NF normalization.
* **Validation:** FluentValidation for request data integrity.

## 🛠️ Tech Stack

* **Framework:** .NET 8 Web API
* **Database:** MS SQL Server 2022
* **ORM:** Entity Framework Core
* **Caching:** Redis
* **Mapping:** AutoMapper
* **Documentation:** Swagger UI

## ⚙️ Getting Started

1.  **Clone the repo**
    ```bash
    git clone [https://github.com/yourusername/financetrackerbacke.git](https://github.com/yourusername/financetrackerbacke.git)
    ```
2.  **Configure Database**
    Update the `ConnectionStrings` in `appsettings.json` for your local MSSQL and Redis instances.
3.  **Run Migrations**
    Open Package Manager Console and run:
    ```bash
    Update-Database
    ```
4.  **Run the API**
    Start the project and navigate to `/swagger` to test endpoints.

## 🏗️ Architecture Overview

The solution follows the Onion Architecture:
* **Core (Domain):** Entities and Enums.
* **Application:** CQRS Handlers, DTOs, Interfaces, Validators.
* **Infrastructure:** EF Core context, Redis implementation, TCMB Service.
* **API:** Controllers and Dependency Injection setup.
