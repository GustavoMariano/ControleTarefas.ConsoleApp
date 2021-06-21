CREATE TABLE [dbo].[TbTarefas] (
    [Id]                  INT           IDENTITY (1, 1) NOT NULL,
    [Titulo]              NVARCHAR (50) NOT NULL,
    [Prioridade]          INT           NOT NULL,
    [DataCriacao]         DATETIME      NOT NULL,
    [DataConclusao]       DATETIME      NULL,
    [PercentualConclusao] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

