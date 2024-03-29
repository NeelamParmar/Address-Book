USE [AddressBook]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 03/21/2019 19:11:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Contact] ON
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName]) VALUES (2, N'Sara', N'Parker')
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName]) VALUES (3, N'Mike', N'Murrey')
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName]) VALUES (4, N'Adam', N'Eve')
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName]) VALUES (5, N'Stanley', N'Cooper')
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName]) VALUES (6, N'Dan', N'Dumber')
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName]) VALUES (7, N'Bob', N'Watson')
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName]) VALUES (8, N'John', N'Doe')
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName]) VALUES (9, N'John', N'Beaver')
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName]) VALUES (10, N'Peter', N'Parker')
SET IDENTITY_INSERT [dbo].[Contact] OFF
/****** Object:  Table [dbo].[ContactDetail]    Script Date: 03/21/2019 19:11:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContactId] [int] NOT NULL,
	[DetailsType] [int] NOT NULL,
	[Details] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ContactDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ContactDetail] ON
INSERT [dbo].[ContactDetail] ([Id], [ContactId], [DetailsType], [Details]) VALUES (12, 4, 1, N'121212121')
INSERT [dbo].[ContactDetail] ([Id], [ContactId], [DetailsType], [Details]) VALUES (13, 4, 2, N'eve@test.com')
SET IDENTITY_INSERT [dbo].[ContactDetail] OFF
/****** Object:  ForeignKey [FK_ContactDetail_Contact]    Script Date: 03/21/2019 19:11:55 ******/
ALTER TABLE [dbo].[ContactDetail]  WITH CHECK ADD  CONSTRAINT [FK_ContactDetail_Contact] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[ContactDetail] CHECK CONSTRAINT [FK_ContactDetail_Contact]
GO
