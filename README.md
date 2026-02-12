# 🏋️‍♂️ Gym Management System
**Advanced ASP.NET Core MVC Implementation | Clean Architecture**



A robust management solution designed to handle gym operations with a focus on **decoupling** and **transactional integrity**. This project serves as a showcase of modern .NET backend patterns.

---

## 🏗 Architectural Highlights

This project isn't just a CRUD app; it's built with scalability in mind:

* **Repository Pattern:** Abstracts the data access logic, making the application easier to maintain and unit test by decoupling the controller from EF Core.
* **Unit of Work Pattern:** Ensures that all repository operations within a single business transaction succeed or fail together, maintaining strict database ACID properties.
* **Data Mapping (AutoMapper):** Implemented to strictly separate **Domain Entities** from **ViewModels/DTOs**, preventing sensitive data exposure and reducing boilerplate code.
* **Session Management:** Secured using **Cookie-based Authentication**, providing a lightweight and reliable user experience.

---

## 🛠️ Tech Stack

| Layer | Technology |
| :--- | :--- |
| **Framework** | ASP.NET Core MVC |
| **Language** | C# (Modern Syntax) |
| **ORM** | Entity Framework Core |
| **Database** | Microsoft SQL Server |
| **Mapping** | AutoMapper |
| **UI/UX** | Bootstrap 5, JS, CSS3, HTML5 |

---

## 🚀 Key Features

- ✅ **Member Management:** Full lifecycle tracking (Registration, Membership status).
- ✅ **Transaction Safety:** Guaranteed by the Unit of Work pattern.
- ✅ **Responsive Design:** A professional UI crafted by a Frontend Specialist for seamless mobile/desktop use.
- ✅ **Secure Sessions:** Efficient tracking via optimized Cookie management.
- ✅ **Clean Separation:** Clear division of concerns (Controllers, Services, Repositories).

---

## 📁 Project Structure

```text
GymManagementSystem/
├── 📂 Controllers         # Handles HTTP requests & coordinates between Services
├── 📂 Data                # Core Data Layer
│   ├── 📂 Repositories    # Implementation of Data Access Logic
│   └── 📂 UnitOfWork      # Coordination of multiple repository operations
├── 📂 Models              # Database Entities (Domain Layer)
├── 📂 DTOs/ViewModels     # Data Transfer Objects for the Presentation Layer
├── 📂 Services            # Business Logic Layer
├── 📂 Views               # Razor Views (UI)
└── 📂 wwwroot             # Static files (CSS, JS, Images)
