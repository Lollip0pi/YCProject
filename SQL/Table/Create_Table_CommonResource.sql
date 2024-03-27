USE [YCDataBase]
GO

/****** Object:  Table [dbo].[CommonResource]    Script Date: 2024/3/27 �U�� 04:01:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CommonResource](
	[ResourceTarget] [varchar](50) NOT NULL,
	[ResourceKey] [varchar](60) NOT NULL,
	[LocaleId] [varchar](10) NOT NULL,
	[Name] [nvarchar](500) NULL,
 CONSTRAINT [PK_CommonResource] PRIMARY KEY CLUSTERED 
(
	[ResourceTarget] ASC,
	[ResourceKey] ASC,
	[LocaleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�h�y�t�ت����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CommonResource', @level2type=N'COLUMN',@level2name=N'ResourceTarget'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�h�y�t�D��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CommonResource', @level2type=N'COLUMN',@level2name=N'ResourceKey'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�y�tID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CommonResource', @level2type=N'COLUMN',@level2name=N'LocaleId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�y�t�W��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CommonResource', @level2type=N'COLUMN',@level2name=N'Name'
GO


