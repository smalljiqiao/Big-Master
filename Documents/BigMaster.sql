USE [master]
GO
/****** Object:  Database [BigMaster]    Script Date: 2018/7/19 15:12:33 ******/
CREATE DATABASE [BigMaster]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BigMaster', FILENAME = N'D:\developTools\SQL2017\Developer\Release\MSSQL14.MSSQLSERVER\MSSQL\DATA\BigMaster.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BigMaster_log', FILENAME = N'D:\developTools\SQL2017\Developer\Release\MSSQL14.MSSQLSERVER\MSSQL\DATA\BigMaster_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [BigMaster] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BigMaster].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BigMaster] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BigMaster] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BigMaster] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BigMaster] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BigMaster] SET ARITHABORT OFF 
GO
ALTER DATABASE [BigMaster] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BigMaster] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BigMaster] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BigMaster] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BigMaster] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BigMaster] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BigMaster] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BigMaster] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BigMaster] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BigMaster] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BigMaster] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BigMaster] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BigMaster] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BigMaster] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BigMaster] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BigMaster] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BigMaster] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BigMaster] SET RECOVERY FULL 
GO
ALTER DATABASE [BigMaster] SET  MULTI_USER 
GO
ALTER DATABASE [BigMaster] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BigMaster] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BigMaster] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BigMaster] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BigMaster] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'BigMaster', N'ON'
GO
ALTER DATABASE [BigMaster] SET QUERY_STORE = OFF
GO
USE [BigMaster]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [BigMaster]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 2018/7/19 15:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AndroidInfo]    Script Date: 2018/7/19 15:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AndroidInfo](
	[Phone] [varchar](20) NOT NULL,
	[AndroidId] [varchar](50) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.AndroidInfo] PRIMARY KEY CLUSTERED 
(
	[Phone] ASC,
	[AndroidId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BPrice]    Script Date: 2018/7/19 15:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BPrice](
	[PriceId] [int] IDENTITY(1,1) NOT NULL,
	[TypeId] [int] NOT NULL,
	[OriginalCost] [decimal](2, 2) NOT NULL,
	[PreferentialCost] [decimal](2, 2) NOT NULL,
 CONSTRAINT [PK_dbo.BPrice] PRIMARY KEY CLUSTERED 
(
	[PriceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BType]    Script Date: 2018/7/19 15:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BType](
	[TypeId] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](10) NULL,
 CONSTRAINT [PK_dbo.BType] PRIMARY KEY CLUSTERED 
(
	[TypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BurialPoint]    Script Date: 2018/7/19 15:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BurialPoint](
	[Phone] [varchar](20) NOT NULL,
	[BpId] [uniqueidentifier] NOT NULL,
	[FType] [nvarchar](10) NULL,
	[SType] [nvarchar](10) NULL,
	[Remark] [nvarchar](200) NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.BurialPoint] PRIMARY KEY CLUSTERED 
(
	[Phone] ASC,
	[BpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Log]    Script Date: 2018/7/19 15:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log](
	[LogId] [uniqueidentifier] NOT NULL,
	[Mes] [nvarchar](2000) NULL,
	[StackTrace] [nvarchar](4000) NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Log] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 2018/7/19 15:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Phone] [varchar](20) NOT NULL,
	[OrderId] [uniqueidentifier] NOT NULL,
	[OrderType] [varchar](20) NULL,
	[Price] [decimal](2, 2) NOT NULL,
	[PayState] [varchar](10) NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Order] PRIMARY KEY CLUSTERED 
(
	[Phone] ASC,
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderSearch]    Script Date: 2018/7/19 15:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderSearch](
	[Phone] [varchar](20) NOT NULL,
	[OrderId] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](40) NULL,
	[Sex] [tinyint] NOT NULL,
	[BirthDay] [datetime] NOT NULL,
	[ManName] [nvarchar](40) NULL,
	[ManBirthDay] [datetime] NOT NULL,
	[WomanName] [nvarchar](40) NULL,
	[WomanBirthDay] [datetime] NOT NULL,
	[Order_Phone] [varchar](20) NOT NULL,
	[Order_OrderId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.OrderSearch] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Search]    Script Date: 2018/7/19 15:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Search](
	[Phone] [varchar](20) NOT NULL,
	[SearchId] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](40) NULL,
	[Sex] [tinyint] NOT NULL,
	[BirthDay] [datetime] NOT NULL,
	[ManName] [nvarchar](40) NULL,
	[ManBirthDay] [datetime] NOT NULL,
	[WomanName] [nvarchar](40) NULL,
	[WomanBirthDay] [datetime] NOT NULL,
	[ZhouWord] [nvarchar](100) NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Search] PRIMARY KEY CLUSTERED 
(
	[Phone] ASC,
	[SearchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2018/7/19 15:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Phone] [varchar](20) NOT NULL,
	[NickName] [nvarchar](20) NULL,
	[Email] [varchar](50) NULL,
	[Password] [varchar](20) NOT NULL,
	[Salt] [char](8) NOT NULL,
	[SaltPassword] [char](50) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.User] PRIMARY KEY CLUSTERED 
(
	[Phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Phone]    Script Date: 2018/7/19 15:12:33 ******/
CREATE NONCLUSTERED INDEX [IX_Phone] ON [dbo].[AndroidInfo]
(
	[Phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TypeId]    Script Date: 2018/7/19 15:12:33 ******/
CREATE NONCLUSTERED INDEX [IX_TypeId] ON [dbo].[BPrice]
(
	[TypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Phone]    Script Date: 2018/7/19 15:12:33 ******/
CREATE NONCLUSTERED INDEX [IX_Phone] ON [dbo].[BurialPoint]
(
	[Phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Phone]    Script Date: 2018/7/19 15:12:33 ******/
CREATE NONCLUSTERED INDEX [IX_Phone] ON [dbo].[Order]
(
	[Phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Order_Phone_Order_OrderId]    Script Date: 2018/7/19 15:12:33 ******/
CREATE NONCLUSTERED INDEX [IX_Order_Phone_Order_OrderId] ON [dbo].[OrderSearch]
(
	[Order_Phone] ASC,
	[Order_OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Phone]    Script Date: 2018/7/19 15:12:33 ******/
CREATE NONCLUSTERED INDEX [IX_Phone] ON [dbo].[Search]
(
	[Phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AndroidInfo] ADD  CONSTRAINT [DF_CreateTime_AndroidInfo]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[BurialPoint] ADD  CONSTRAINT [DF_CreateTime_BurialPoint]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[Log] ADD  CONSTRAINT [DF_CreateTime_Log]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_CreateTime_Order]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[Search] ADD  CONSTRAINT [DF_CreateTime_Search]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_CreateTime_User]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[AndroidInfo]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AndroidInfo_dbo.User_Phone] FOREIGN KEY([Phone])
REFERENCES [dbo].[User] ([Phone])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AndroidInfo] CHECK CONSTRAINT [FK_dbo.AndroidInfo_dbo.User_Phone]
GO
ALTER TABLE [dbo].[BPrice]  WITH CHECK ADD  CONSTRAINT [FK_dbo.BPrice_dbo.BType_TypeId] FOREIGN KEY([TypeId])
REFERENCES [dbo].[BType] ([TypeId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BPrice] CHECK CONSTRAINT [FK_dbo.BPrice_dbo.BType_TypeId]
GO
ALTER TABLE [dbo].[BurialPoint]  WITH CHECK ADD  CONSTRAINT [FK_dbo.BurialPoint_dbo.User_Phone] FOREIGN KEY([Phone])
REFERENCES [dbo].[User] ([Phone])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BurialPoint] CHECK CONSTRAINT [FK_dbo.BurialPoint_dbo.User_Phone]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Order_dbo.User_Phone] FOREIGN KEY([Phone])
REFERENCES [dbo].[User] ([Phone])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_dbo.Order_dbo.User_Phone]
GO
ALTER TABLE [dbo].[OrderSearch]  WITH CHECK ADD  CONSTRAINT [FK_dbo.OrderSearch_dbo.Order_Order_Phone_Order_OrderId] FOREIGN KEY([Order_Phone], [Order_OrderId])
REFERENCES [dbo].[Order] ([Phone], [OrderId])
GO
ALTER TABLE [dbo].[OrderSearch] CHECK CONSTRAINT [FK_dbo.OrderSearch_dbo.Order_Order_Phone_Order_OrderId]
GO
ALTER TABLE [dbo].[Search]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Search_dbo.User_Phone] FOREIGN KEY([Phone])
REFERENCES [dbo].[User] ([Phone])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Search] CHECK CONSTRAINT [FK_dbo.Search_dbo.User_Phone]
GO
ALTER TABLE [dbo].[OrderSearch]  WITH CHECK ADD  CONSTRAINT [CK_OrderSearch_Sex] CHECK  (([Sex]=(0) OR [Sex]=(1) OR [Sex]=(2)))
GO
ALTER TABLE [dbo].[OrderSearch] CHECK CONSTRAINT [CK_OrderSearch_Sex]
GO
ALTER TABLE [dbo].[Search]  WITH CHECK ADD  CONSTRAINT [CK_Search_Sex] CHECK  (([Sex]=(0) OR [Sex]=(1) OR [Sex]=(2)))
GO
ALTER TABLE [dbo].[Search] CHECK CONSTRAINT [CK_Search_Sex]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AndroidInfo', @level2type=N'COLUMN',@level2name=N'Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'安卓ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AndroidInfo', @level2type=N'COLUMN',@level2name=N'AndroidId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生成时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AndroidInfo', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户安卓信息类' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AndroidInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务价格类ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BPrice', @level2type=N'COLUMN',@level2name=N'PriceId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务种类ID，外键关联BType表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BPrice', @level2type=N'COLUMN',@level2name=N'TypeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务原价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BPrice', @level2type=N'COLUMN',@level2name=N'OriginalCost'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务优惠价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BPrice', @level2type=N'COLUMN',@level2name=N'PreferentialCost'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务价格类' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务种类ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BType', @level2type=N'COLUMN',@level2name=N'TypeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BType', @level2type=N'COLUMN',@level2name=N'TypeName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务种类类' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BurialPoint', @level2type=N'COLUMN',@level2name=N'Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'埋点ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BurialPoint', @level2type=N'COLUMN',@level2name=N'BpId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一级类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BurialPoint', @level2type=N'COLUMN',@level2name=N'FType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'二级类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BurialPoint', @level2type=N'COLUMN',@level2name=N'SType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'补充说明，当二类类型不能满足埋点操作时，将额外信息写入此列' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BurialPoint', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生成时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BurialPoint', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'埋点信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BurialPoint'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'日志ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Log', @level2type=N'COLUMN',@level2name=N'LogId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'错误信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Log', @level2type=N'COLUMN',@level2name=N'Mes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'堆栈轨迹' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Log', @level2type=N'COLUMN',@level2name=N'StackTrace'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生成时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Log', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'错误日志类' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Log'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'OrderId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'OrderType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单价格，精确两位小数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'Price'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单支付状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'PayState'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生成时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单类' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderSearch', @level2type=N'COLUMN',@level2name=N'Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderSearch', @level2type=N'COLUMN',@level2name=N'OrderId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'查询姓名 对应八字详批和宝宝取名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderSearch', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'性别 值为1时是男性，为2时是女性，为0时是未知 对应八字详批和宝宝取名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderSearch', @level2type=N'COLUMN',@level2name=N'Sex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出生日期 对应八字详批和宝宝取名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderSearch', @level2type=N'COLUMN',@level2name=N'BirthDay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'八字合婚男方姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderSearch', @level2type=N'COLUMN',@level2name=N'ManName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'八字合婚男方出生日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderSearch', @level2type=N'COLUMN',@level2name=N'ManBirthDay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'八字合婚女方姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderSearch', @level2type=N'COLUMN',@level2name=N'WomanName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'八字合婚女方出生日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderSearch', @level2type=N'COLUMN',@level2name=N'WomanBirthDay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单查询类<br/>记录订单所有查询的关键字信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderSearch'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Search', @level2type=N'COLUMN',@level2name=N'Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'搜索ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Search', @level2type=N'COLUMN',@level2name=N'SearchId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'查询姓名 对应八字详批和宝宝取名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Search', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'性别 值为1时是男性，为2时是女性，为0时是未知 对应八字详批和宝宝取名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Search', @level2type=N'COLUMN',@level2name=N'Sex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出生日期 对应八字详批和宝宝取名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Search', @level2type=N'COLUMN',@level2name=N'BirthDay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'八字合婚男方姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Search', @level2type=N'COLUMN',@level2name=N'ManName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'八字合婚男方出生日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Search', @level2type=N'COLUMN',@level2name=N'ManBirthDay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'八字合婚女方姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Search', @level2type=N'COLUMN',@level2name=N'WomanName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'八字合婚女方出生日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Search', @level2type=N'COLUMN',@level2name=N'WomanBirthDay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'周公解梦搜索词' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Search', @level2type=N'COLUMN',@level2name=N'ZhouWord'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生成时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Search', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'搜索记录类' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Search'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'昵称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'NickName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邮箱' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码明文' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码盐值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Salt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码密文' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'SaltPassword'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'注册时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户信息类' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User'
GO
USE [master]
GO
ALTER DATABASE [BigMaster] SET  READ_WRITE 
GO
