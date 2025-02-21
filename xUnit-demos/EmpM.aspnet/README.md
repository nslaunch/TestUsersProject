# Employee Management Application

A simple ASP.NET application to manage employees and departments.

---

## Table of Contents

1. [Project Setup]
2. [Database Configuration]
3. [Running the Application]

---

## Project Setup

### Prerequisites

- Microsoft SQL Server Management Studio (SSMS)
- Visual Studio with ASP.NET support
- .NET Framework installed on your system

1. Clone the repository:
   ```bash
   git clone https://github.com/vikmish7/EmpM.aspnet.git
   cd EmpM.aspnet
2. Open the solution file EmployeeManagementApp.sln in Visual Studio.
3. Configure your database connection in the Web.config file (refer to Database Configuration).

### Database Configuration
-Open SSMS and connect to your local database server.
-Run the following SQL scripts to set up the database, tables, and stored procedures.
-SQL Script for Tables and Procedures
-Copy and execute the following SQL script in SSMS:

4. Web config file changes for db on root level
  <connectionStrings>
    <add name="EMS" connectionString="Server = LCG-VIKASMISHRA; Database = EMPT; Integrated Security = true;" />
  </connectionStrings>
<connectionStrings>
    <add name="EMS" connectionString="Server =YOUR_SERVER_NAME;Database =YOUR_DATABASE_NAME;Integrated Security=True;" />
</connectionStrings>

### Running the Application
After completing the setup, press F5 in Visual Studio to run the application.

```sql
BEGIN TRY
    -- Drop Employee Table if it exists
    IF OBJECT_ID('dbo.Employee', 'U') IS NOT NULL
        DROP TABLE dbo.Employee;

    -- Drop Department Table if it exists
    IF OBJECT_ID('dbo.Department', 'U') IS NOT NULL
        DROP TABLE dbo.Department;

    -- Drop Procedures if they exist
    IF OBJECT_ID('dbo.InsertEmployee', 'P') IS NOT NULL
        DROP PROCEDURE dbo.InsertEmployee;

    IF OBJECT_ID('dbo.InsertDepartment', 'P') IS NOT NULL
        DROP PROCEDURE dbo.InsertDepartment;

    IF OBJECT_ID('dbo.GetEmployees', 'P') IS NOT NULL
        DROP PROCEDURE dbo.GetEmployees;

    IF OBJECT_ID('dbo.GetDepartments', 'P') IS NOT NULL
        DROP PROCEDURE dbo.GetDepartments;

    IF OBJECT_ID('dbo.UpdateEmployee', 'P') IS NOT NULL
        DROP PROCEDURE dbo.UpdateEmployee;

    IF OBJECT_ID('dbo.UpdateDepartment', 'P') IS NOT NULL
        DROP PROCEDURE dbo.UpdateDepartment;

    IF OBJECT_ID('dbo.DeleteEmployee', 'P') IS NOT NULL
        DROP PROCEDURE dbo.DeleteEmployee;

    IF OBJECT_ID('dbo.DeleteDepartment', 'P') IS NOT NULL
        DROP PROCEDURE dbo.DeleteDepartment;

    -- Create Employee Table
    CREATE TABLE dbo.Employee (
        EmployeeId INT IDENTITY PRIMARY KEY,
        EmployeeName VARCHAR(50),
        Designation VARCHAR(30),
        NID VARCHAR(20),
        BloodGroup VARCHAR(5),
        JoiningDate DATETIME,
        DepartmentId INT
    );

    -- Create Department Table
    CREATE TABLE dbo.Department (
        DepartmentId INT PRIMARY KEY,
        Code VARCHAR(255),
        DNAME VARCHAR(50)
    );
PRINT 'All Tables created successfully.';
END TRY
BEGIN CATCH
    PRINT 'Error occurred: ' + ERROR_MESSAGE();
END CATCH;

	GO

BEGIN TRY
   -- Create Insert Employee Procedure
    DECLARE @sql NVARCHAR(MAX);
    SET @sql = N'
    CREATE PROCEDURE dbo.InsertEmployee
        @EmployeeName VARCHAR(50),
        @Designation VARCHAR(30),
        @NID VARCHAR(20),
        @BloodGroup VARCHAR(5),
        @JoiningDate DATETIME,
        @DepartmentId INT
    AS
    BEGIN
        INSERT INTO dbo.Employee (EmployeeName, Designation, NID, BloodGroup, JoiningDate, DepartmentId)
        VALUES (@EmployeeName, @Designation, @NID, @BloodGroup, @JoiningDate, @DepartmentId);
    END;';
    EXEC sp_executesql @sql;

    -- Create Insert Department Procedure
    SET @sql = N'
    CREATE PROCEDURE dbo.InsertDepartment
        @DepartmentId INT,
        @Code VARCHAR(255),
        @DNAME VARCHAR(50)
    AS
    BEGIN
        INSERT INTO dbo.Department (DepartmentId, Code, DNAME)
        VALUES (@DepartmentId, @Code, @DNAME);
    END;';
    EXEC sp_executesql @sql;

    -- Create Retrieve Employees Procedure
    SET @sql = N'
    CREATE PROCEDURE dbo.GetEmployees
        @EmployeeId INT = NULL,
        @EmployeeName VARCHAR(50) = NULL,
        @Designation VARCHAR(30) = NULL
    AS
    BEGIN
        SELECT * FROM dbo.Employee
        WHERE (@EmployeeId IS NULL OR EmployeeId = @EmployeeId)
          AND (@EmployeeName IS NULL OR EmployeeName LIKE ''%'' + @EmployeeName + ''%'')
          AND (@Designation IS NULL OR Designation LIKE ''%'' + @Designation + ''%'');
    END;';
    EXEC sp_executesql @sql;

    -- Create Retrieve Departments Procedure
    SET @sql = N'
    CREATE PROCEDURE dbo.GetDepartments
        @DepartmentId INT = NULL,
        @Code VARCHAR(255) = NULL
    AS
    BEGIN
        SELECT * FROM dbo.Department
        WHERE (@DepartmentId IS NULL OR DepartmentId = @DepartmentId)
          AND (@Code IS NULL OR Code LIKE ''%'' + @Code + ''%'');
    END;';
    EXEC sp_executesql @sql;

    -- Create Update Employee Procedure
    SET @sql = N'
    CREATE PROCEDURE dbo.UpdateEmployee
        @EmployeeId INT,
        @EmployeeName VARCHAR(50) = NULL,
        @Designation VARCHAR(30) = NULL,
        @NID VARCHAR(20) = NULL,
        @BloodGroup VARCHAR(5) = NULL,
        @JoiningDate DATETIME = NULL,
        @DepartmentId INT = NULL
    AS
    BEGIN
        UPDATE dbo.Employee
        SET EmployeeName = COALESCE(@EmployeeName, EmployeeName),
            Designation = COALESCE(@Designation, Designation),
            NID = COALESCE(@NID, NID),
            BloodGroup = COALESCE(@BloodGroup, BloodGroup),
            JoiningDate = COALESCE(@JoiningDate, JoiningDate),
            DepartmentId = COALESCE(@DepartmentId, DepartmentId)
        WHERE EmployeeId = @EmployeeId;
    END;';
    EXEC sp_executesql @sql;

    -- Create Update Department Procedure
    SET @sql = N'
    CREATE PROCEDURE dbo.UpdateDepartment
        @DepartmentId INT,
        @Code VARCHAR(255) = NULL,
        @DNAME VARCHAR(50) = NULL
    AS
    BEGIN
        UPDATE dbo.Department
        SET Code = COALESCE(@Code, Code),
            DNAME = COALESCE(@DNAME, DNAME)
        WHERE DepartmentId = @DepartmentId;
    END;';
    EXEC sp_executesql @sql;

    -- Create Delete Employee Procedure
    SET @sql = N'
    CREATE PROCEDURE dbo.DeleteEmployee
        @EmployeeId INT
    AS
    BEGIN
        DELETE FROM dbo.Employee
        WHERE EmployeeId = @EmployeeId;
    END;';
    EXEC sp_executesql @sql;

    -- Create Delete Department Procedure
    SET @sql = N'
    CREATE PROCEDURE dbo.DeleteDepartment
        @DepartmentId INT
    AS
    BEGIN
        DELETE FROM dbo.Department
        WHERE DepartmentId = @DepartmentId;
    END;';
    EXEC sp_executesql @sql;

    PRINT 'All procedures created successfully.';
END TRY
BEGIN CATCH
    PRINT 'Error occurred: ' + ERROR_MESSAGE();
END CATCH;

-- Execute Insert Employee Procedure
EXEC dbo.InsertEmployee
    @EmployeeName = 'John Doe',
    @Designation = 'Software Engineer',
    @NID = '123456789',
    @BloodGroup = 'A+',
    @JoiningDate = '2022-01-01',
    @DepartmentId = 1;

-- Execute Insert Department Procedure
EXEC dbo.InsertDepartment
    @DepartmentId = 1,
    @Code = 'IT-001',
    @DNAME = 'Information Technology';

-- Execute Get Employees Procedure
EXEC dbo.GetEmployees


--    @EmployeeId = 1,
--    @EmployeeName = 'John',
--    @Designation = 'Engineer';

-- Execute Get Departments Procedure
EXEC dbo.GetDepartments
--    @DepartmentId = 1,
--    @Code = 'IT';

-- Execute Update Employee Procedure
EXEC dbo.UpdateEmployee
    @EmployeeId = 1,
    @EmployeeName = 'John Doe Jr.',
    @Designation = 'Senior Software Engineer',
    @NID = '987654321',
    @BloodGroup = 'B+',
    @JoiningDate = '2020-06-01',
    @DepartmentId = 1;

-- Execute Update Department Procedure
EXEC dbo.UpdateDepartment
    @DepartmentId = 1,
    @Code = 'IT-001-A',
    @DNAME = 'Information Technology - Software';

-- Execute Delete Employee Procedure
EXEC dbo.DeleteEmployee
    @EmployeeId = 1;

-- Execute Delete Department Procedure
EXEC dbo.DeleteDepartment
    @DepartmentId = 1;

USE [EMPT]
GO
/****** Object:  StoredProcedure [dbo].[sPGetEmployeeDepartments]    Script Date: 19-12-2024 18:43:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sPGetEmployeeDepartments]
	AS
BEGIN
	SET NOCOUNT ON;
 
	SELECT E.*,D.Code,D.DName as DepartmentName FROM dbo.EMPLOYEE E
	INNER JOIN DEPARTMENT D
	ON E.DepartmentId=D.DepartmentId
 
    
END
EXEC [dbo].[sPGetEmployeeDepartments]

USE [EMPT]
GO
/****** Object:  StoredProcedure [dbo].[sPGetDepartments]    Script Date: 19-12-2024 18:44:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sPGetDepartments]
	AS
BEGIN
	SET NOCOUNT ON;
 
	SELECT * FROM dbo.DEPARTMENT
END

EXEC [dbo].[sPGetEmployeeDepartments]

