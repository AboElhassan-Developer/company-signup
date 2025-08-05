# Company Sign-Up System

## 📌 Overview
The **Company Sign-Up System** is a full-stack web application that enables companies to register online, verify their account via OTP email confirmation, set a secure password, and log in to access a personalized home page.

The system is built using:
- **Frontend**: Angular 16+
- **Backend**: ASP.NET Core Web API (.NET 8) with Clean Architecture
- **Database**: PostgreSQL
- **Architecture**: 4 Layers (Data, Repository, Services, API)
- **Security**: OTP Email Verification + Password Validation Rules
- **File Upload**: Company Logo upload and preview before submission

---
 ## 🚀 Features

### 1️⃣ Sign-Up
### 2️⃣ OTP Verification
### 3️⃣ Set Password
### 4️⃣ Login
### 5️⃣ Home Page

---
## 🏗 Project Structure

### **Backend** (.NET 8 Web API)
CompanySignUpSystem/
│── CompanySignUpSystem.Data/ # Entities, DbContext
│── CompanySignUpSystem.Repository/ # Interfaces & Repository Implementations
│── CompanySignUpSystem.Services/ # Business Logic & DTOs
│── CompanySignUpSystem.API/ # Controllers, API Endpoints, DI Configurations

### **Frontend** (Angular 16)
company-signup-angular/
│── src/
│ ├── app/
│ │ ├── auth/ # Signup, Login, OTP, Set Password components
│ │ ├── home/ # Home component
│ │ ├── services/ # AuthService, API integration
│ │ └── shared/ # Shared UI components
│ └── environments/ # API configuration

---

## ⚙ Installation & Setup

### 1️⃣ Clone Repositories
# Backend
git clone https://github.com/AboElhassan-Developer/company-signup.git
cd CompanySignUpSystem
# Frontend
git clone https://github.com/AboElhassan-Developer/company-signup-frontend.git
cd company-signup-frontend

2️⃣ Backend Setup
Create PostgreSQL database
Update appsettings.json
Apply migrations
Run API
API will run at https://localhost:7053
3️⃣ Frontend Setup
npm install
ng serve
Frontend will run at http://localhost:4200

نسخ
تحرير
