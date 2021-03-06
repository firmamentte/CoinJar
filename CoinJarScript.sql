USE [CoinJar]
GO
/****** Object:  Table [dbo].[CoinItem]    Script Date: 2021/08/18 22:34:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CoinItem](
	[CoinItemID] [uniqueidentifier] NOT NULL,
	[CoinID] [uniqueidentifier] NOT NULL,
	[Volume] [decimal](18, 2) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[DeletionDate] [datetime] NOT NULL,
 CONSTRAINT [PK_CoinItem] PRIMARY KEY CLUSTERED 
(
	[CoinItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vCoinItemTotalCount]    Script Date: 2021/08/18 22:34:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vCoinItemTotalCount]
AS
SELECT CoinID, SUM(Volume) AS TotalCount
FROM CoinItem
WHERE DeletionDate='9995-01-01 00:00:00.000'
GROUP BY CoinID

GO
/****** Object:  Table [dbo].[Coin]    Script Date: 2021/08/18 22:34:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coin](
	[CoinID] [uniqueidentifier] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Coin] PRIMARY KEY CLUSTERED 
(
	[CoinID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Coin] ADD  CONSTRAINT [DF_Coin_CoinID]  DEFAULT (newid()) FOR [CoinID]
GO
ALTER TABLE [dbo].[CoinItem] ADD  CONSTRAINT [DF_CoinItem_CoinItemID]  DEFAULT (newid()) FOR [CoinItemID]
GO
ALTER TABLE [dbo].[CoinItem]  WITH CHECK ADD  CONSTRAINT [FK_CoinItem_Coin] FOREIGN KEY([CoinID])
REFERENCES [dbo].[Coin] ([CoinID])
GO
ALTER TABLE [dbo].[CoinItem] CHECK CONSTRAINT [FK_CoinItem_Coin]
GO
/****** Object:  StoredProcedure [dbo].[spGetTotalAmount]    Script Date: 2021/08/18 22:34:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetTotalAmount] 

AS
BEGIN

SELECT coin.Amount, ISNULL(SUM((coin.Amount/100) * vCoinItemTotalCount.TotalCount),0) AS TotalAmount, ISNULL(vCoinItemTotalCount.TotalCount,0) AS TotalCount
FROM Coin coin
LEFT JOIN vCoinItemTotalCount
ON coin.CoinID=vCoinItemTotalCount.CoinID
GROUP BY coin.Amount,
		 vCoinItemTotalCount.TotalCount
END
GO
/****** Object:  StoredProcedure [dbo].[spResetCoinItem]    Script Date: 2021/08/18 22:34:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Bonginkosi Dlamini>
-- Create date: <2021/08/17>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spResetCoinItem]
AS
BEGIN
	UPDATE CoinItem
	SET DeletionDate = GETDATE()
	WHERE DeletionDate = '9995-01-01 00:00:00.000'
END
GO

INSERT INTO [dbo].[Coin]([CoinID],[Amount])
     VALUES(NEWID(),100),
		   (NEWID(),50),
		   (NEWID(),25),
		   (NEWID(),10),
		   (NEWID(),5),
		   (NEWID(),1)
GO
