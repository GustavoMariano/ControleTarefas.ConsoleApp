CREATE TABLE [dbo].[Table]
(
	[Id] INT IDENTITY(1,1) PRIMARY KEY, 
    [Titulo] NVARCHAR(50) NULL, 
    [Prioridade] INT NULL, 
    [DataCriacao] DATETIME NULL, 
    [DataConclusao] DATETIME NULL, 
    [PercentualConclusao] INT NULL,

)
