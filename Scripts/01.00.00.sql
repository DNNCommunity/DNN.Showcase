CREATE TABLE [dbo].[Community_Showcase_Category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Community_Showcase_Category] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Community_Showcase_Site](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[url] [nvarchar](250) NOT NULL,
	[description] [nvarchar](2000) NULL,
	[is_active] [bit] NOT NULL,
	[thumbnail] [nvarchar](250) NULL,
	[user_id] [int] NOT NULL,
	[created_date] [datetime] NOT NULL,
 CONSTRAINT [PK_Community_Showcase_Sites] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Community_Showcase_SiteCategory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[site_id] [int] NOT NULL,
	[category_id] [int] NOT NULL,
 CONSTRAINT [PK_Community_Showcase_SiteCategory] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Community_Showcase_Site]  WITH CHECK ADD  CONSTRAINT [FK_Community_Showcase_Sites_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Community_Showcase_Site] CHECK CONSTRAINT [FK_Community_Showcase_Sites_Users]
GO
ALTER TABLE [dbo].[Community_Showcase_SiteCategory]  WITH CHECK ADD  CONSTRAINT [FK_Community_Showcase_SiteCategory_Community_Showcase_Category] FOREIGN KEY([category_id])
REFERENCES [dbo].[Community_Showcase_Category] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Community_Showcase_SiteCategory] CHECK CONSTRAINT [FK_Community_Showcase_SiteCategory_Community_Showcase_Category]
GO
ALTER TABLE [dbo].[Community_Showcase_SiteCategory]  WITH CHECK ADD  CONSTRAINT [FK_Community_Showcase_SiteCategory_Community_Showcase_Site] FOREIGN KEY([site_id])
REFERENCES [dbo].[Community_Showcase_Site] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Community_Showcase_SiteCategory] CHECK CONSTRAINT [FK_Community_Showcase_SiteCategory_Community_Showcase_Site]
GO
