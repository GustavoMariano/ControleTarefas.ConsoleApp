CREATE TABLE [dbo].[TbCompromissos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Assunto] [nvarchar](50) NOT NULL,
	[Localizacao] [nvarchar](50) NULL,
	[Link] [nvarchar](50) NULL,
	[DataInicio] [smalldatetime] NOT NULL,
	[DataFinal] [smalldatetime] NOT NULL,
	[TbContatos_Id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[TbCompromissos]  WITH CHECK ADD FOREIGN KEY([TbContatos_Id])
REFERENCES [dbo].[TbContatos] ([Id])
GO