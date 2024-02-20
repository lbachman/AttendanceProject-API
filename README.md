# Attendance Management API

---

## Overview

This API provides CRUD (Create, Read, Update, Delete) operations to manage attendance data efficiently. It serves as the backend logic for an attendance database system, enabling easy manipulation and retrieval of attendance records.

## Endpoints

1. **Get Students**
   - **Endpoint:** `GET / https://localhost:7001/api/v1/AttendanceAPI/all/{key}`
   - **Description:** gets list of all students.
   - **Response:** Returns list of students.

2. **Post a Student**
   - **Endpoint:** `POST / https://localhost:7001/api/v1/AttendanceAPI/{key}`
   - **Description:** add a single student object.
   - **Request Body:** JSON containing student details.
   - **Response:** Returns the created student object.

## Authentication and Authorization

- **Authentication:** This API requires authentication to access certain endpoints, ensuring data security.
- **Authorization:** Role-based access control mechanisms are implemented to restrict operations based on user roles.

## Technologies Used

- **Language:** This API is implemented using C# & the ASP core framework.
- **Database:** MySQL is used for storing attendance data.


---
