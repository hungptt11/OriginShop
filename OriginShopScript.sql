CREATE DATABASE OriginCoffee
ON PRIMARY
(name = OriginCoffee, filename = 'C:\TELLER\OriginCoffee.MDF', size = 4MB, maxsize = unlimited, filegrowth = 20%)
LOG ON
(name = OriginCoffee_Log, filename = 'C:\TELLER\OriginCoffee.LDF', size = 4MB, maxsize = unlimited, filegrowth = 20%)
GO
use OriginCoffee
go
IF EXISTS (SELECT * FROM sys.objects WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[About]') AND TYPE IN (N'U'))
BEGIN
	DROP TABLE About
END
go
Create table About
(
	[Id] [int] identity(1,1) primary key,
	[Title] [nvarchar](250) NULL,
	[MetaTitle] [varchar](250) NULL,
	[Description] [nvarchar](500) NULL,
	[Image] [nvarchar](250) NULL,
	[Detail] [ntext] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[MetaKeywords] [nvarchar](250) NULL,
	[MetaDescriptions] [nvarchar](250) NULL,
	[Status] [bit] NULL,
)
go
IF EXISTS (SELECT * FROM sys.objects WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[NewsCategory]') AND TYPE IN (N'U'))
BEGIN
	DROP TABLE NewsCategory
END
go
Create table NewsCategory
(
	[Id] [int] IDENTITY(1,1) primary key,
	[Name] [nvarchar](250) NULL,
	[MetaTitle] [varchar](250) NULL,
	[ParentID] [int] NULL,
	[DisplayOrder] [int] NULL DEFAULT ((0)),
	[SeoTitle] [nvarchar](250) NULL,
	[CreatedDate] [datetime] NULL DEFAULT (getdate()),
	[CreatedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[MetaKeywords] [nvarchar](250) NULL,
	[MetaDescriptions] [nvarchar](250) NULL,
	[Status] [bit] NULL DEFAULT ((1)),
	[ShowOnHome] [bit] NULL DEFAULT ((0)),
)
go
IF EXISTS (SELECT * FROM sys.objects WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[Products]') AND TYPE IN (N'U'))
BEGIN
	DROP TABLE Products
END
go
Create table Products
(
	[Id] [int] IDENTITY(1,1) primary key,
	[ProductCode] [varchar](10) NULL,
	[ProductName] [nvarchar](250) NULL,
	[MetaTitle] [varchar](250) NULL,
	[Description] [nvarchar](500) NULL,
	[ProductImage] [nvarchar](250) NULL,
	[MoreImages] [xml] NULL,
	[Price] [decimal](18, 0) NULL DEFAULT ((0)),
	[PromotionPrice] [decimal](18, 0) NULL,
	[IncludeVAT] [bit] NULL,
	[Quantity] [int] NULL DEFAULT ((0)),
	[CategoryID] [int] NULL,
	[Detail] [ntext] NULL,
	[CreatedDate] [datetime] NULL DEFAULT (getdate()),
	[CreatedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[MetaKeywords] [nvarchar](250) NULL,
	[MetaDescriptions] [nvarchar](250) NULL,
	[Status] [bit] NULL  DEFAULT ((1)),
	[TopHot] [date] NULL,
	[ViewCounts] [int] NULL,
)
go
IF EXISTS (SELECT * FROM sys.objects WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[Slider]') AND TYPE IN (N'U'))
BEGIN
	DROP TABLE Slider
END
go
Create table Slider
(
	[Id] [int] IDENTITY(1,1) primary key,
	[Image] [nvarchar](250) NULL,
	[DisplayOrder] [int] NULL DEFAULT ((1)),
	[Link] [nvarchar](250) NULL,
	[Description] [nvarchar](250) NULL,
	[CreatedDate] [datetime] NULL DEFAULT (getdate()),
	[CreatedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[Status] [bit] NULL DEFAULT ((1)),
)
go
IF EXISTS (SELECT * FROM sys.objects WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[SystemConfig]') AND TYPE IN (N'U'))
BEGIN
	DROP TABLE SystemConfig
END
go
Create table SystemConfig
(
	[Id] [varchar](50) NOT NULL,
	[ConfigName] [nvarchar](50) NULL,
	[Type] [nvarchar](50) NULL,
	[Value] [nvarchar](250) NULL,
	[Status] [bit] NULL,
)
go
IF EXISTS (SELECT * FROM sys.objects WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[ProductCategory]') AND TYPE IN (N'U'))
BEGIN
	DROP TABLE ProductCategory
END
go
Create table ProductCategory
(
	[Id] [int] IDENTITY(1,1) primary key,
	[Name] [nvarchar](250) NULL,
	[MetaTitle] [varchar](250) NULL,
	[ParentID] [int] NULL,
	[DisplayOrder] [int] NULL DEFAULT ((0)),
	[SeoTitle] [nvarchar](250) NULL,
	[CreatedDate] [datetime] NULL DEFAULT (getdate()),
	[CreatedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[MetaKeywords] [nvarchar](250) NULL,
	[MetaDescriptions] [nvarchar](250) NULL,
	[Status] [bit] NULL DEFAULT ((1)),
	[ShowOnHome] [bit] NULL DEFAULT ((0)),
)
go
IF EXISTS (SELECT * FROM sys.objects WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[News]') AND TYPE IN (N'U'))
BEGIN
	DROP TABLE News
END
go
Create table News
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](250) NULL,
	[MetaTitle] [varchar](250) NULL,
	[Description] [nvarchar](500) NULL,
	[NewImage] [nvarchar](250) NULL,
	[NewCategoryID] [int] NULL,
	[Detail] [ntext] NULL,
	[CreatedDate] [datetime] NULL DEFAULT (getdate()),
	[CreatedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[MetaKeywords] [nvarchar](250) NULL,
	[MetaDescriptions] [nvarchar](250) NULL,
	[Status] [bit] NULL DEFAULT ((1)),
	[TopHot] [datetime] NULL,
	[ViewCount] [int] NULL,
	[TagID] [varchar](50) NULL,
)
go
IF EXISTS (SELECT * FROM sys.objects WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[Menu]') AND TYPE IN (N'U'))
BEGIN
	DROP TABLE Menu
END
go
Create table Menu
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Text] [nvarchar](50) NULL,
	[Link] [nvarchar](250) NULL,
	[DisplayOrder] [int] NULL DEFAULT ((1)),
	[Target] [nvarchar](250) NULL,
	[Status] [bit] NULL,
	[MenuTypeID] [int] NULL,
	[MenuParentID] [int] NULL,
	[MenuDestination] [int] NULL,
)