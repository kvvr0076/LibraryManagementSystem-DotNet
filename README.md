# Library Management System (.NET MVC)

A complete Library Management System built with ASP.NET Core MVC and SQL Server.

---

## 🔧 Technologies Used

- ASP.NET Core MVC (.NET 9.0)
- SQL Server Express 2022
- Entity Framework Core
- Bootstrap 5
- Visual Studio 2022

---

## ✅ Features

- User Roles: Admin and Librarian
- Admin Dashboard with live statistics
- Add / Edit / Delete Books
- Add / Edit / Delete Members
- Borrow / Return Book Tracking
- Manage Users (Admin only)
- Role-based Access Control
- Secure Login & Signup with Session
- Responsive UI (Bootstrap)

---

📢Notes

- Admin can manage all users, members, books, and borrow records
- Librarians can manage books and borrow/return but not users/members
- Borrowing decreases quantity, returning increases it
- Overdue books are highlighted in red
- Everything is protected with session-based login

---

## 🛠 How to Run Locally

1. Clone this repository or download the ZIP  
2. Open the solution file `.sln` in Visual Studio  
3. Go to `appsettings.json` and set the connection string: 
  	- "ConnectionStrings": {
 "DefaultConnection": "Server=localhost;Database=LibraryDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
4. Open **Package Manager Console** and run:
    - Add-Migration Init
    - Update-Database

5. Press `Ctrl + F5` to run the app

---

## Default Admin Credentials

- Username: admin
- Password: admin123

- Insert this manually into your database:

```sql
INSERT INTO Users (Name, Email, Phone, Username, Password, Role)
VALUES ('Admin User', 'admin@example.com', '9999999999', 'admin', 'admin123', 'admin');

```

Folder Structure

LibraryManagementSystem/
├── Controllers/
│   ├── AccountController.cs
│   ├── HomeController.cs
│   ├── BookController.cs
│   ├── MemberController.cs
│   ├── BorrowController.cs
│   ├── UserController.cs
├── Models/
│   ├── User.cs
│   ├── Member.cs
│   ├── Book.cs
│   ├── BorrowRecord.cs
│   ├── ApplicationDbContext.cs
├── Views/
│   ├── Shared/
│   │   ├── _Layout.cshtml
│   │   ├── _ViewImports.cshtml
│   │   ├── _ViewStart.cshtml
│   ├── Home/
│   │   └── Index.cshtml
│   ├── Account/
│   │   ├── Login.cshtml
│   │   └── Signup.cshtml
│   ├── Book/
│   │   ├── Index.cshtml
│   │   ├── Add.cshtml
│   │   └── Edit.cshtml
│   ├── Member/
│   │   ├── Index.cshtml
│   │   ├── Add.cshtml
│   │   └── Edit.cshtml
│   ├── Borrow/
│   │   ├── Index.cshtml
│   │   └── Lend.cshtml
│   ├── User/
│   │   ├── Index.cshtml
│   │   ├── Add.cshtml
│   │   └── Edit.cshtml
├── wwwroot/
├── appsettings.json
├── Program.cs


---
- Created by Vishnuvardhan Reddy Komatireddy
- Year: 2024