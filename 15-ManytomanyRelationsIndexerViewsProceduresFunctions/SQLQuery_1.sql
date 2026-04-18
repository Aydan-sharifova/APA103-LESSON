USE CompanyMM;



CREATE TABLE Employees
(
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    BirthDate DATE NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    CONSTRAINT CHK_Employees_BirthDate CHECK (BirthDate < GETDATE())
);


CREATE TABLE Projects
(
    ProjectID INT PRIMARY KEY IDENTITY(1,1),
    ProjectName NVARCHAR(100) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    CONSTRAINT CHK_Projects_Dates CHECK (EndDate >= StartDate)
);


CREATE TABLE EmployeeProjects
(
    EmployeeID INT NOT NULL,
    ProjectID INT NOT NULL,
    AssignedDate DATE NOT NULL DEFAULT GETDATE(),

    CONSTRAINT PK_EmployeeProjects PRIMARY KEY (EmployeeID, ProjectID),
    CONSTRAINT FK_EmployeeProjects_Employees FOREIGN KEY (EmployeeID)
        REFERENCES Employees(EmployeeID),
    CONSTRAINT FK_EmployeeProjects_Projects FOREIGN KEY (ProjectID)
        REFERENCES Projects(ProjectID)
);


INSERT INTO Employees (FirstName, LastName, BirthDate, Email)
VALUES
('Aydan', 'Sharifova', '2002-05-14', 'aydan@gmail.com'),
('Murad', 'Aliyev', '1998-11-22', 'murad@gmail.com'),
('Nigar', 'Hasanova', '2000-03-10', 'nigar@gmail.com'),
('Tural', 'Mammadov', '1997-08-01', 'tural@gmail.com'),
('Leyla', 'Karimova', '1999-12-05', 'leyla@gmail.com');

INSERT INTO Projects (ProjectName, StartDate, EndDate)
VALUES
('ERP System', '2026-01-01', '2026-06-30'),
('Booking App', '2026-02-15', '2026-08-15'),
('E-Commerce Platform', '2026-03-01', '2026-09-30');

INSERT INTO EmployeeProjects (EmployeeID, ProjectID, AssignedDate)
VALUES
(1, 1, '2026-01-10'),
(1, 2, '2026-02-20'),
(1, 3, '2026-03-05'),
(2, 1, '2026-01-15'),
(2, 2, '2026-02-25'),
(3, 2, '2026-03-01'),
(4, 3, '2026-03-10'),
(5, 1, '2026-01-18');

SELECT * FROM Employees;
SELECT * FROM Projects;



SELECT 
    e.EmployeeID,
    e.FirstName,
    e.LastName,
    p.ProjectID,
    p.ProjectName,
    ep.AssignedDate
FROM Employees e
JOIN EmployeeProjects ep ON e.EmployeeID = ep.EmployeeID
JOIN Projects p ON ep.ProjectID = p.ProjectID
ORDER BY e.EmployeeID, p.ProjectID;



SELECT 
    p.ProjectID,
    p.ProjectName,
    COUNT(ep.EmployeeID) AS EmployeeCount
FROM Projects p
LEFT JOIN EmployeeProjects ep ON p.ProjectID = ep.ProjectID
GROUP BY p.ProjectID, p.ProjectName
ORDER BY p.ProjectID;



SELECT 
    e.EmployeeID,
    e.FirstName,
    e.LastName,
    COUNT(ep.ProjectID) AS ProjectCount
FROM Employees e
JOIN EmployeeProjects ep ON e.EmployeeID = ep.EmployeeID
GROUP BY e.EmployeeID, e.FirstName, e.LastName
HAVING COUNT(ep.ProjectID) > 2;


GO
CREATE VIEW EmployeeProjectView
AS
SELECT
    e.EmployeeID,
    e.FirstName + ' ' + e.LastName AS FullName,
    p.ProjectID,
    p.ProjectName,
    ep.AssignedDate
FROM Employees e
JOIN EmployeeProjects ep ON e.EmployeeID = ep.EmployeeID
JOIN Projects p ON ep.ProjectID = p.ProjectID;
GO


SELECT *
FROM EmployeeProjectView
WHERE EmployeeID = 1;
GO

CREATE PROCEDURE sp_AssignEmployeeToProject
    @empId INT,
    @projId INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS
    (
        SELECT 1
        FROM EmployeeProjects
        WHERE EmployeeID = @empId AND ProjectID = @projId
    )
    BEGIN
        INSERT INTO EmployeeProjects (EmployeeID, ProjectID, AssignedDate)
        VALUES (@empId, @projId, GETDATE());
    END
END;
 
GO

CREATE FUNCTION fn_GetProjectCount (@empId INT)
RETURNS INT
AS
BEGIN
    DECLARE @ProjectCount INT;

    SELECT @ProjectCount = COUNT(*)
    FROM EmployeeProjects
    WHERE EmployeeID = @empId;

    RETURN @ProjectCount;
END;
GO

SELECT dbo.fn_GetProjectCount(1) AS ProjectCountForEmployee1;



EXEC sp_AssignEmployeeToProject @empId = 3, @projId = 1;


SELECT *
FROM EmployeeProjectView
WHERE EmployeeID = 3;
GO

DELETE FROM EmployeeProjects
WHERE EmployeeID = 2;
GO


SELECT *
FROM EmployeeProjectView
WHERE EmployeeID = 2;
GO