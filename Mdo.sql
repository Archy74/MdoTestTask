USE [Mdo]
GO

/****** Object:  Table [dbo].[Images]    Script Date: 08/30/2018 23:57:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Images](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Image] [image] NULL,
	[Name] [nvarchar](255) NULL,
	[Format] [nchar](5) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


