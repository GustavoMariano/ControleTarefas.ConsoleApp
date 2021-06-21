CREATE TABLE [dbo].[TbContatos] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Nome]     NVARCHAR (50) NULL,
    [Email]    NVARCHAR (50) NOT NULL,
    [Telefone] NVARCHAR (20) NOT NULL,
    [Empresa]  NVARCHAR (50)    NULL,
    [Cargo]    NVARCHAR (50)    NULL
);
