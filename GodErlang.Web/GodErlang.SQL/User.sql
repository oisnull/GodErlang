CREATE TABLE [dbo].[User]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(20) NOT NULL, 
    [PhoneNum] NVARCHAR(13) NULL, 
    [Email] NVARCHAR(100) NULL, 
    [Password] NVARCHAR(32) NOT NULL, 
    [HeadImage] NVARCHAR(100) NULL, 
    [State] int NOT NULL, 
    [Sex] int NOT NULL, 
    [CreateTime] DATETIME NOT NULL, 
    [UpdateTime] DATETIME NULL
)