USE [animal]
GO
/****** Object:  Table [dbo].[animals]    Script Date: 2/21/2017 4:14:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[animals](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[gender] [varchar](255) NULL,
	[date] [varchar](255) NULL,
	[breed] [varchar](255) NULL,
	[typeId] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[type]    Script Date: 2/21/2017 4:14:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[type](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[animals] ON 

INSERT [dbo].[animals] ([id], [name], [gender], [date], [breed], [typeId]) VALUES (1, N'Bob', N'boy', N'2/21/2017', N'english bully', 1)
INSERT [dbo].[animals] ([id], [name], [gender], [date], [breed], [typeId]) VALUES (2, N'cvdsf', N'dsaf', N'asdf', N'asdf', 3)
SET IDENTITY_INSERT [dbo].[animals] OFF
SET IDENTITY_INSERT [dbo].[type] ON 

INSERT [dbo].[type] ([id], [name]) VALUES (1, N'dog')
INSERT [dbo].[type] ([id], [name]) VALUES (2, N'birds')
INSERT [dbo].[type] ([id], [name]) VALUES (3, N'cat')
SET IDENTITY_INSERT [dbo].[type] OFF
