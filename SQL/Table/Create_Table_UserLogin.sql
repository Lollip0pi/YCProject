USE [YCDataBase]
GO

/****** Object:  Table [dbo].[UserLogin]    Script Date: 2024/3/27 下午 02:13:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserLogin](
	[UserGuid] [uniqueidentifier] NOT NULL,
	[CreateTime] [datetime] NULL,
	[ModifyTime] [datetime] NULL,
	[isLogin] [nvarchar](1) NULL,
 CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED 
(
	[UserGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立時間' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserLogin', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改時間' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserLogin', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否登入 Y=是 N=否' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserLogin', @level2type=N'COLUMN',@level2name=N'isLogin'
GO


