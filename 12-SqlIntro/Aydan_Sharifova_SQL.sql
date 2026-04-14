-- CREATE DATABASE Company;
use Company


CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) UNIQUE,
    PhoneNumber NVARCHAR(20),
    HireDate DATE,
    JobTitle NVARCHAR(100),
    Salary DECIMAL(10,2),
    Department NVARCHAR(50)
);

INSERT INTO Employees (EmployeeID, FirstName, LastName, Email, PhoneNumber, HireDate, JobTitle, Salary, Department)
VALUES
(1, N'Aysel', N'Məmmədova', N'aysel.mammadova@company.az', N'0501112233', '2019-03-15', N'Backend Developer', 2500, N'IT'),
(2, N'Leyla', N'Həsənova', N'leyla.hasanova@company.az', N'0502223344', '2021-06-10', N'HR Specialist', 1800, N'HR'),
(3, N'Kamal', N'Əliyev', N'kamal.aliyev@company.az', N'0503334455', '2022-01-20', N'System Administrator', 2200, N'IT'),
(4, N'Nigar', N'Quliyeva', N'nigar.quliyeva@company.az', N'0504445566', '2018-11-05', N'Accountant', 2100, N'Maliyyə'),
(5, N'Murad', N'İsmayılov', N'murad.ismayilov@company.az', N'0505556677', '2023-09-01', N'Sales Specialist', 1700, N'Satış');


SELECT * FROM Employees;


SELECT *
FROM Employees
WHERE Salary > 2000;

SELECT *
FROM Employees
WHERE Department = N'IT';


SELECT *
FROM Employees
ORDER BY Salary DESC;


SELECT FirstName, Salary
FROM Employees;


SELECT *
FROM Employees
WHERE HireDate > '2020-12-31';


SELECT *
FROM Employees
WHERE Email LIKE N'%company.az%';


SELECT MAX(Salary) AS HighestSalary
FROM Employees;


SELECT MIN(Salary) AS LowestSalary
FROM Employees;


SELECT AVG(Salary) AS AverageSalary
FROM Employees;


SELECT COUNT(*) AS TotalEmployees
FROM Employees;


SELECT SUM(Salary) AS TotalSalary
FROM Employees;


SELECT Department, COUNT(*) AS EmployeeCount
FROM Employees
GROUP BY Department;


SELECT Department, AVG(Salary) AS AverageDepartmentSalary
FROM Employees
GROUP BY Department;


SELECT Department, MAX(Salary) AS HighestDepartmentSalary
FROM Employees
GROUP BY Department;


UPDATE Employees
SET Salary = 2800
WHERE EmployeeID = 1;


UPDATE Employees
SET Salary = Salary * 1.10
WHERE Department = N'IT';


UPDATE Employees
SET JobTitle = N'HR Meneceri'
WHERE FirstName = N'Leyla' AND LastName = N'Həsənova';


DELETE FROM Employees
WHERE EmployeeID = 5;


INSERT INTO Employees (EmployeeID, FirstName, LastName, Email, PhoneNumber, HireDate, JobTitle, Salary, Department)
VALUES (6, N'Rauf', N'Kərimov', N'rauf.karimov@company.az', N'0506667788', '2024-02-12', N'Intern', 1400, N'IT');

DELETE FROM Employees
WHERE Salary < 1500;


SELECT *
FROM Employees
WHERE FirstName LIKE N'%a%';


SELECT *
FROM Employees
WHERE Salary BETWEEN 2000 AND 2500;


SELECT *
FROM Employees
WHERE Department IN (N'Maliyyə', N'IT');
