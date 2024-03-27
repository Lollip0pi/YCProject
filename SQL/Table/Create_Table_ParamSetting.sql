USE [YCDataBase]
GO

/****** Object:  Table [dbo].[ParamSetting]    Script Date: 2024/3/27 下午 03:19:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ParamSetting](
	[FuncType] [nvarchar](20) NOT NULL,
	[FuncTypeDesc] [nvarchar](100) NULL,
	[Param] [nvarchar](20) NOT NULL,
	[ParamDesc] [nvarchar](100) NULL,
	[Code] [nvarchar](20) NOT NULL,
	[CodeDesc] [nvarchar](255) NULL,
	[CanDel] [bit] NULL,
	[IsSystem] [bit] NULL,
 CONSTRAINT [PK_ParamSetting_1] PRIMARY KEY CLUSTERED 
(
	[FuncType] ASC,
	[Param] ASC,
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ParamSetting] ADD  CONSTRAINT [DF_ParamSetting_CanDel_1]  DEFAULT ((0)) FOR [CanDel]
GO

ALTER TABLE [dbo].[ParamSetting] ADD  CONSTRAINT [DF_ParamSetting_IsSystem_1]  DEFAULT ((0)) FOR [IsSystem]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'功能類別' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ParamSetting', @level2type=N'COLUMN',@level2name=N'FuncType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'功能類別敘述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ParamSetting', @level2type=N'COLUMN',@level2name=N'FuncTypeDesc'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'參數類別' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ParamSetting', @level2type=N'COLUMN',@level2name=N'Param'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'參數類別敘述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ParamSetting', @level2type=N'COLUMN',@level2name=N'ParamDesc'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'代碼' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ParamSetting', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'代碼敘述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ParamSetting', @level2type=N'COLUMN',@level2name=N'CodeDesc'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'能否被刪除 0 false 1 true' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ParamSetting', @level2type=N'COLUMN',@level2name=N'CanDel'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否為系統值 1為true  0為false' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ParamSetting', @level2type=N'COLUMN',@level2name=N'IsSystem'
GO


