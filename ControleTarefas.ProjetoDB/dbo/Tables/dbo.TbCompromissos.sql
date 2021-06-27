CREATE TABLE [dbo].[TbCompromissos] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Assunto]      NVARCHAR (50) NOT NULL,
    [Localizacao]  NVARCHAR (50) NULL,
    [Link]         NVARCHAR (50) NULL,
    [DataInicio]   SMALLDATETIME NOT NULL,
    [DataFinal]    SMALLDATETIME NOT NULL,
    [Id_TbContato] INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT fk_CompromissoContato FOREIGN KEY ([Id_TbContato]) REFERENCES [dbo].[TbContatos] ([Id])
);

