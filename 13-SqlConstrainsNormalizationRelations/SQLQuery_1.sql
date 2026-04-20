CREATE DATABASE CompanyRelations;

USE CompanyRelations;


CREATE TABLE Departments
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(255) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);



CREATE TABLE Employees
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Phone NVARCHAR(20) NULL,
    HireDate DATE NOT NULL,
    DepartmentId INT NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_Employees_Departments
        FOREIGN KEY (DepartmentId) REFERENCES Departments(Id)
        ON DELETE NO ACTION
        ON UPDATE CASCADE
);



CREATE TABLE EmployeeDetails
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    EmployeeId INT NOT NULL UNIQUE,
    Address NVARCHAR(255) NULL,
    BirthDate DATE NULL,
    Gender NVARCHAR(20) NULL,
    MaritalStatus NVARCHAR(20) NULL,
    EmergencyContact NVARCHAR(100) NULL,
    NationalId NVARCHAR(50) NULL,

    CONSTRAINT FK_EmployeeDetails_Employees
        FOREIGN KEY (EmployeeId) REFERENCES Employees(Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);



CREATE TABLE Roles
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(255) NULL
);



CREATE TABLE EmployeeRoles
(
    EmployeeId INT NOT NULL,
    RoleId INT NOT NULL,
    AssignedDate DATE NOT NULL DEFAULT CAST(GETDATE() AS DATE),

    CONSTRAINT PK_EmployeeRoles PRIMARY KEY (EmployeeId, RoleId),

    CONSTRAINT FK_EmployeeRoles_Employees
        FOREIGN KEY (EmployeeId) REFERENCES Employees(Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,

    CONSTRAINT FK_EmployeeRoles_Roles
        FOREIGN KEY (RoleId) REFERENCES Roles(Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);



CREATE TABLE Projects
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    ProjectName NVARCHAR(100) NOT NULL,
    Budget DECIMAL(18,2) NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NULL,
    Status NVARCHAR(30) NOT NULL DEFAULT 'Planned'
);


CREATE TABLE EmployeeProjects
(
    EmployeeId INT NOT NULL,
    ProjectId INT NOT NULL,
    AssignedDate DATE NOT NULL DEFAULT CAST(GETDATE() AS DATE),
    Responsibility NVARCHAR(100) NULL,

    CONSTRAINT PK_EmployeeProjects PRIMARY KEY (EmployeeId, ProjectId),

    CONSTRAINT FK_EmployeeProjects_Employees
        FOREIGN KEY (EmployeeId) REFERENCES Employees(Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,

    CONSTRAINT FK_EmployeeProjects_Projects
        FOREIGN KEY (ProjectId) REFERENCES Projects(Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);

CREATE TABLE SalaryHistory
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    EmployeeId INT NOT NULL,
    Salary DECIMAL(18,2) NOT NULL CHECK (Salary > 0),
    FromDate DATE NOT NULL,
    ToDate DATE NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_SalaryHistory_Employees
        FOREIGN KEY (EmployeeId) REFERENCES Employees(Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);

