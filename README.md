# 🍽️ Table Management System

ระบบจัดการโต๊ะและจองโต๊ะร้านอาหาร พัฒนาด้วย ASP.NET Core MVC และ PostgreSQL

---

## 📖 Project Overview

Table Management System เป็นระบบสำหรับจัดการโต๊ะร้านอาหาร  
รองรับการแสดงสถานะโต๊ะ (ว่าง/ไม่ว่าง) และระบบจองโต๊ะ  
พร้อม Client-side Validation และ Dynamic UI

---

## 🚀 Features

- ✅ แสดงโต๊ะแยกตามโซน
- ✅ แสดงสถานะโต๊ะ (ว่าง / ไม่ว่าง)
- ✅ จองโต๊ะผ่าน Right Panel (Dynamic UI)
- ✅ ซ่อน/แสดงข้อมูลอัตโนมัติเมื่อเลือกโต๊ะ
- ✅ Real-time Validation (Validate ทันทีเมื่อกรอกผิด)
- ✅ SweetAlert2 แจ้งเตือนเมื่อจองสำเร็จ
- ✅ Partial View สำหรับ Edit/Delete
- ✅ ใช้ ViewModel แยกจาก Model (Clean Architecture Concept)

---

## 🛠 Tech Stack

- .NET 9
- ASP.NET Core MVC
- Entity Framework Core 9
- PostgreSQL
- Npgsql EF Core Provider
- Bootstrap 5
- jQuery Validation
- SweetAlert2
- Git & GitHub

---

## 🗄 Database

ใช้ PostgreSQL เป็นฐานข้อมูลหลัก  
เชื่อมต่อผ่าน Entity Framework Core (Npgsql Provider)

### Example Connection String
"ConnectionStrings": {
"DefaultConnection": "Host=localhost;Database=TableManagementDb;Username=postgres;Password=yourpassword"
}
---

## 🖥️ How to Run

1️⃣ Clone repositorygit 
clone https://github.com/P-Ittiphol/TableManagement.git

2️⃣ เข้าโฟลเดอร์โปรเจค
cd TableManagement


3️⃣ แก้ไข Connection String ใน appsettings.json
"ConnectionStrings": {
"DefaultConnection": "Host=localhost;Database=TableManagementDb;Username=postgres;Password=yourpassword"
}

4️⃣ สร้าง Database
dotnet ef database update


5️⃣ Run project
dotnet run


---

## 📂 Project Structure
Controllers/
Models/
ViewModels/
Views/
Data/
wwwroot/

---

## 👨‍💻 Developer

P-Ittiphol  
GitHub: https://github.com/P-Ittiphol




