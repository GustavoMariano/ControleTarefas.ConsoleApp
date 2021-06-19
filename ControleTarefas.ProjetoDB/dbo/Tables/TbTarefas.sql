CREATE TABLE [dbo].[TbTarefas] (
    [Id]                  INT           IDENTITY (1, 1) NOT NULL,
    [Titulo]              NVARCHAR (50) NULL,
    [Prioridade]          INT           NULL,
    [DataCriacao]         DATETIME      NULL,
    [DataConclusao]       DATETIME      NULL,
    [PercentualConclusao] INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

