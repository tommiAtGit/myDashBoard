# System Architecture - myDashBoard

This document outlines the high-level architecture of the myDashBoard application, a modular system for managing personal finance, notes, and tasks.

## 1. Frontend Architecture

The frontend is built as a Single Page Application (SPA) using **React 18** and **JavaScript**.

- **Framework:** React (Create React App template).
- **UI Library:** Material UI (MUI) version 6 for components and styling.
- **Routing:** React Router DOM version 6 for navigation between views.
- **API Communication:** Axios for making asynchronous HTTP requests to backend services.
- **State Management:** Uses React Hooks (`useState`, `useEffect`) for local component state and data fetching.
- **Key Components:**
    - `myDashBoardFinanceView`: Interface for financial tracking.
    - `myDashboardNotesView`: Tabbed interface for notes management and "Active Recall" exercises.
    - `myDashboardTodoView`: Dashboard for managing tasks across different statuses.

## 2. Backend Architecture

The backend consists of three independent microservices built with **.NET 8.0** and **ASP.NET Core**.

### General Service Patterns
- **Architecture:** N-tier architecture (Controller -> Service -> Repository).
- **Communication:** RESTful APIs using JSON.
- **Mapping:** AutoMapper is used to transform internal Models to external Domain/DTO objects.
- **Dependency Injection:** Standard .NET DI container for managing service and repository lifecycles.

### Services

#### myFinanceService
- **Goal:** Manages financial balances, budgets, transactions, and balance sheets.
- **Endpoints:** Handles balance updates, budget creation, transaction history, and BalanceSheet CRUD operations.
- **Port:** 5002

#### myNotesService
- **Goal:** Manages user notes and documentation.
- **Endpoints:** Supports CRUD operations for notes, keyword-based searching, and owner-based filtering.
- **Port:** 5003

#### myTodoService
- **Goal:** Manages tasks and todo lists.
- **Endpoints:** Handles task creation, updates (including status changes), and deletion.
- **Port:** 5001

## 3. Database Architecture

The system is designed to transition from in-memory mock repositories to a persistent relational database.

- **Storage Engine:** Relational Database (PostgreSQL or MySQL).
- **Persistence:** Currently implemented as in-memory `List<T>` within "Mock" repository classes for prototyping.
- **Schema Design:** Detailed entity definitions and relations can be found in the [Database Architecture Document](./database_architecture.md).

## 4. Infrastructure

- **Containerization:** The entire system is containerized using **Docker**.
- **Orchestration:** **Docker Compose** manages the multi-container setup, including the frontend, three backend services, and a MySQL database container.
- **Networking:** Services communicate within a private Docker bridge network.
