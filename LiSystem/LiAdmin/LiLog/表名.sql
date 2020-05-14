USE [LiLog]
GO

/****** Object:  Table [dbo].[LogInfo]    Script Date: 2020/5/9 10:29:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[LogInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LogDate] [nvarchar](30) NOT NULL,
	[LogLevel] [nvarchar](20) NULL,
	[UserID] [nvarchar](20) NULL,
	[UserName] [nvarchar](20) NULL,
	[HostIP] [nvarchar](200) NULL,
	[HostName] [nvarchar](400) NULL,
	[Message] [nvarchar](max) NULL,
	[Exception] [nvarchar](max) NULL,
 CONSTRAINT [PK_LogInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


