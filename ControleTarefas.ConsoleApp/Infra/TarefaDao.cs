
namespace ControleTarefas.ConsoleApp.Infra
{
    public class TarefaDao
    {
        #region Scripts
        internal string ObtemQueryInsercaoTarefa()
        {
            return @"INSERT INTO TbTarefas 
	                (
		                [Titulo], 
		                [Prioridade], 
		                [DataCriacao],
                        [PercentualConclusao]
	                ) 
	                VALUES
	                (
                        @Titulo, 
		                @Prioridade, 
		                @DataCriacao,
                        @Percentual
	                )";
        }

        internal string ObtemQuerySelecionarTodasTarefa()
        {
            return @"SELECT 
                        [Id], 
                        [Titulo], 
                        [Prioridade],
                        [DataCriacao],
                        [DataConclusao],
                        [PercentualConclusao] 
                    FROM 
                        TbTarefas";
        }
        internal string ObtemQuerySelecionarTarefaPorId()
        {
            return @"SELECT 
                        [Id], 
                        [Titulo], 
                        [Prioridade],
                        [DataCriacao],
                        [DataConclusao],
                        [PercentualConclusao] 
                    FROM 
                        TbTarefas
                    WHERE 
                        [ID] = @ID";
        }
        internal string ObtemQueryAtualizarTarefa()
        {
            return @"UPDATE TbTarefas 
	                SET	
                        [Titulo] = @Titulo, 
                        [Prioridade] = @Prioridade,
                        [DataConclusao] = @DataConclusao,
                        [PercentualConclusao] = @Percentual
	                WHERE 
		                [Id] = @Id";
        }

        internal string ObtemQueryDeletarTarefa()
        {
            return @"DELETE FROM TbTarefas 	                
	                WHERE 
		                [ID] = @ID";
        }
        #endregion
    }
}
