CREATE TABLE [dbo].[TbContato]
(
    [Id] INT IDENTITY (1, 1) NOT NULL,
    [Nome] NVARCHAR(50) NULL, 
    [Email] NVARCHAR(50) NOT NULL, 
    [Telefone] NVARCHAR(50) NOT NULL, 
    [Empresa] NCHAR(10) NULL, 
    [Cargo] NCHAR(10) NULL
)
