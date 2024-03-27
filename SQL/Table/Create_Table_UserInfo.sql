USE [YCDataBase]
GO

/****** Object:  Table [dbo].[UserInfo]    Script Date: 2024/3/27 下午 02:08:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserInfo](
	[UserGuid] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](20) NULL,
	[UserAcct] [nvarchar](20) NULL,
	[UserPass] [nvarchar](200) NULL,
	[Status] [varchar](2) NULL,
	[CreateTime] [datetime] NULL,
	[ModifyTime] [datetime] NULL,
 CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED 
(
	[UserGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用者Guid' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'UserGuid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用者名稱' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'UserName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用者帳號' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'UserAcct'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用者密碼' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'UserPass'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'狀態 1=有效 2=註銷' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'Status'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立時間' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改時間' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO


