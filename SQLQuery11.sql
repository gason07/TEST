CREATE DATABASE TEST
GO


USE TEST
GO
CREATE TABLE Employees1
(
     ID int primary key identity,
     FirstName nvarchar(50),
     LastName nvarchar(50),
     Gender nvarchar(50),
     Salary INT,
     DayTime Datetime
)
GO
INSERT INTO Employees1 VALUES ('Pranaya', 'Rout', 'Male', 60000, GETDATE())
INSERT INTO Employees1 VALUES ('Anurag', 'Mohanty', 'Male', 45000, GETDATE())
INSERT INTO Employees1 VALUES ('Preety', 'Tiwari', 'Female', 45000, GETDATE())
INSERT INTO Employees1 VALUES ('Sambit', 'Mohanty', 'Male', 70000, GETDATE())
INSERT INTO Employees1 VALUES ('Shushanta', 'Jena', 'Male', 45000, GETDATE())
INSERT INTO Employees1 VALUES ('Priyanka', 'Dewangan', 'Female', 30000, GETDATE())
INSERT INTO Employees1 VALUES ('Sandeep', 'Kiran', 'Male', 45000, GETDATE())
INSERT INTO Employees1 VALUES('Shudhansshu', 'Nayak', 'Male', 30000,GETDATE())
INSERT INTO Employees1 VALUES ('Hina', 'Sharma', 'Female', 35000, GETDATE())
INSERT INTO Employees1 VALUES ('Preetiranjan', 'Sahoo', 'Male', 80000, GETDATE())
GO

SELECT * FROM dbo.Employees1
GO

SELECT * FROM dbo.Employees1
WHERE Salary > 20000
GO

CREATE TABLE dbo.Company
(
     ID int primary key identity,
     NameCompany nvarchar(50),
     AddressCompany nvarchar(50),
     ThoiGianCapNhat Datetime
)
GO

SELECT * FROM dbo.Company
GO

CREATE TABLE dbo.CompanyST
(
     ID int primary key identity,
     NameCompany nvarchar(50),
     AddressCompany nvarchar(50),
	 AddressURL NVARCHAR(100),
     ThoiGianCapNhat Datetime
)
GO

SELECT * FROM dbo.CompanyST
GO

CREATE TABLE dbo.PhimHinhAnh
(
 ID int primary key identity,
 TieuDe NVARCHAR(max),
 NgayThang DATETIME,
 TenTapTin NVARCHAR(max),
 DuongDanURL NVARCHAR(max),
 MoTa NVARCHAR(max),
 ThoiGianCapNhat DATETIME,
 IsDeleted BIT NOT NULL,
 IsVideo BIT NOT NULL,
 IsImage BIT NOT NULL,
)
GO

CREATE TABLE dbo.BanDo
(
ID int primary key identity,
TenBanDo NVARCHAR(max),
LoaiBanDo DATETIME,
TenTapTin NVARCHAR(max),
DuongDanURL NVARCHAR(max),
MoTa NVARCHAR(max),
ThoiGianCapNhat DATETIME,
IsDeleted BIT NOT NULL
)
GO
--DELETE FROM dbo.CompanyST
--WHERE ID = 5
--GO

SELECT * FROM dbo.PhimHinhAnh
GO

SELECT * FROM dbo.BanDo
GO

CREATE TABLE dbo.FileDinhKem
(
ID int primary key identity,
TenFileDinhKem NVARCHAR(max),
NgayTao DATETIME,
MoTa NVARCHAR(max),
DuongDanURL NVARCHAR(max),
IsVideo BIT NOT NULL,
IsImage BIT NOT NULL,
IsDeleted BIT NOT NULL
)
GO

CREATE TABLE dbo.TapTinDinhkem
(
ID INT PRIMARY KEY IDENTITY NOT NULL,
TenTapTin NVARCHAR(MAX)
MaBanTinDuBao INT PRIMARY KEY IDENTITY NULL
MoTa NVARCHAR(MAX),
NgayDinhkem DATETIME,
DuongDan NVARCHAR(MAX),
IsVideo BIT NULL,
IsHinhAnh BIT NULL,
IsDeleted BIT NOT NULL
)

--DELETE FROM dbo.BanDo
--WHERE ID = 1003
--GO