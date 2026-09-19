Project Purpose
A multi-university academic management system designed to streamline the administration of universities, faculties, departments, users, modules, and graduation criteria. Built with ASP.NET Core and React.

MultiVersity – A Full-Stack Multi-University Management Platform (ASP.NET Core + React)

Multi-University Management System (MUMS)

# MultiVersity — Backend

**REST API for the MultiVersity university management platform.**

A complete backend for digitizing the academic lifecycle — from a student's first application to their final transcript. Built for Moroccan universities.

---

## 🎯 Overview

This is the **API layer** of MultiVersity. It powers a React frontend with a clean, role-aware REST API covering:

- University & faculty management
- Academic programs, courses, and departments
- Admissions, applications, and requirements
- Enrollments and student registry
- Grade entry, publishing, and transcript computation
- File uploads (photos, documents, logos)
- JWT authentication with ASP.NET Identity

---

## ✨ Features

### 🏛️ University Management

- Faculties, departments, programs, courses
- Degrees (Licence, Master, Doctorat)
- University profile with logo upload

### 📝 Admissions & Applications

- Custom application periods and requirements
- 5-step student application flow
- Document upload with validation
- Faculty review queue with status management

### 🎓 Enrollment

- Automatic student number generation (`STU-2025-00042`)
- Role upgrade Applicant → Student
- One active enrollment per student (Moroccan regulation)

### 📊 Grades

- Normale & Rattrapage sessions
- Automatic computation: effective score, weighted average, compensation rules, credits earned
- Publish workflow (professor enters, admin publishes)

### 🔐 Security

- ASP.NET Identity + JWT
- Role-based authorization: `UniversityAdmin`, `Dean`, `Professor`, `Applicant`, `Student`
- Password setup via email for invited staff

---

## 🏗️ Architecture

Clean **N-tier architecture** with strict separation of concerns:
Web API (MultiVersity)
↓
Service Layer (business logic, orchestration, transactions)
↓
Repository (EF Core, query composition, projections)
↓
SQL Server (Identity + domain tables)

**Rules:**

- Business rules live in the **service layer** — never in controllers or the database
- Repositories return **entities** — projections happen in the service
- **DTOs at boundaries** — clients never see entities
- **Strict layering** — Service has no reference to Web API

---

## 🛠️ Tech Stack

- **.NET 8** — modern, fast, cross-platform
- **ASP.NET Core Web API** — REST endpoints
- **Entity Framework Core 8** — ORM with LINQ
- **SQL Server** — relational database
- **ASP.NET Identity** — authentication & authorization
- **AutoMapper** — object-to-object mapping
- **JWT** — stateless authentication
- **Serilog** — structured logging

---

## 📁 Project Structure

multiversity-backend/
├── Entities/ # Domain models
│ ├── Models/ # University, Faculty, Course, Grade, ...
│ └── Exceptions/ # Domain-specific exceptions
│
├── Shared/ # DTOs + request features
│ ├── DataTransferObjects/ # ForCreation, ForUpdate, Dto
│ └── RequestFeatures/ # Filter parameters
│
├── Contracts/ # Repository interfaces
├── Repository/ # EF Core implementations
│ ├── Configuration/ # Entity configurations
│ ├── Extensions/ # Filter extensions
│ └── RepositoryContext.cs
│
├── Service.Contracts/ # Service interfaces
├── Service/ # Business logic
│ ├── ApplicationService.cs
│ ├── EnrollmentService.cs
│ ├── GradeService.cs
│ └── ...
│
├── MultiVersity.Presentation/ # Controllers
│ └── Controllers/
│
└── MultiVersity/ # Web host
├── Program.cs
├── MappingProfile.cs
└── wwwroot/uploads/ # File storage

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server (local or Docker)
- A code editor (VS 2022, Rider, or VS Code)

### Setup

```bash
# Clone
git clone https://github.com/your-org/multiversity-backend.git
cd multiversity-backend

# Restore packages
dotnet restore

# Configure the connection string
# Edit MultiVersity/appsettings.Development.json:
#   "ConnectionStrings": {
#     "sqlConnection": "Server=localhost;Database=MultiVersity;Trusted_Connection=True;"
#   }

# Apply migrations
dotnet ef database update --project MultiVersity

# Trust the HTTPS dev certificate
dotnet dev-certs https --trust

# Run
dotnet run --project MultiVersity

API runs on https://localhost:5001. Swagger UI at https://localhost:5001/swagger.

First-time Setup
Register a university admin via POST /api/auth/register/university-admin

Login via POST /api/auth/login → receive JWT

Use Authorization: Bearer <token> on subsequent requests

Create faculties, departments, programs, courses

Invite deans and professors via POST /api/faculties/{id}/dean and /professors

Create an admission and publish it → students can apply

Key Workflows
1. Student signs up → "Applicant" role
2. Browses programs → finds open admission
3. Fills 5-step wizard (personal, academic, photo, documents, review)
4. Dean reviews → sets status (Submitted → UnderReview → Approved)
5. If approved → student accepts/declines
6. If accepted → dean enrolls → student number generated, role upgraded to Student
7. Student sees grades as they're published
Grade Entry & Publishing
1. Professor selects course + year + semester + session
2. Enters grades inline for enrolled students
3. Saves (creates/updates grade records)
4. Publishes → grades visible to students
5. System computes:
   - Effective score = max(Normale, Rattrapage)
   - Semester average (weighted by coefficient)
   - Validation (individual, rattrapage, or compensation)
   - Credits earned
```
