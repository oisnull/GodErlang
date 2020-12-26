CREATE TABLE [dbo].[ProductStatus]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [UserId] INT NOT NULL, 
    [ReferUrl] NVARCHAR(500) NULL, 
    [State] INT NOT NULL, 
    [AddTime] DATETIME NOT NULL, 
    [StartExecTime] DATETIME NULL, 
    [EndExecTime] DATETIME NULL, 
    [ExecMessage] NVARCHAR(200) NULL
)
