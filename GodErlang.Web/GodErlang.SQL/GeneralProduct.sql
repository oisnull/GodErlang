CREATE TABLE [dbo].[GeneralProduct]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [SourceType] INT NOT NULL, 
    [SourceTypeName] NVARCHAR(50) NOT NULL, 
    [ReferUrl] NVARCHAR(500) NULL, 
    [OriginId] NVARCHAR(100) NULL, 
    [Title] NVARCHAR(500) NULL, 
    [OfferType] NVARCHAR(500) NULL, 
    [ActualPrice] MONEY NOT NULL, 
    [ActualPriceDesc] NVARCHAR(100) NULL, 
    [PromotionPrice] MONEY NOT NULL, 
    [PromotionPriceDesc] NVARCHAR(100) NULL, 
    [PriceCurrency] NVARCHAR(100) NULL, 
    [ProductImages] NVARCHAR(1000) NULL, 
    [RecordTime] DATETIME NOT NULL, 
    [UpdateTime] DATETIME NULL
)
