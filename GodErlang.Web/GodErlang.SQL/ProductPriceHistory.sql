CREATE TABLE [dbo].[ProductPriceHistory]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [ProductId] INT NOT NULL, 
    [ActualPrice] MONEY NOT NULL,  
    [PromotionPrice] MONEY NOT NULL, 
    [PriceCurrency] NVARCHAR(100) NULL, 
    [RecordTime] DATETIME NOT NULL
)