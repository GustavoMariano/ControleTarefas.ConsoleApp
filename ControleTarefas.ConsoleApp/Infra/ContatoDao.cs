
namespace ControleTarefas.ConsoleApp.Infra
{
    public class ContatoDao
    {
        #region Scripts
        internal string ObtemQueryInsercaoContato()
        {
            return @"INSERT INTO TbContatos
	                (
		                [Nome], 
		                [Email], 
		                [Telefone],
                        [Empresa],
                        [Cargo]
	                ) 
	                VALUES
	                (
                        @Nome, 
		                @Email, 
		                @Telefone,
                        @Empresa,
                        @Contato
	                )";
        }

        internal string ObtemQuerySelecionarTodosContatos()
        {
            return @"SELECT 
                        [Id], 
		                [Nome], 
		                [Email], 
		                [Telefone],
                        [Empresa],
                        [Cargo]
                    FROM 
                        TbContatos";
        }
        internal string ObtemQuerySelecionarContatoPorId()
        {
            return @"SELECT 
                        [Id], 
		                [Nome], 
		                [Email], 
		                [Telefone],
                        [Empresa],
                        [Cargo]
                    FROM 
                        TbContatos
                    WHERE 
                        [ID] = @ID";
        }
        internal string ObtemQueryAtualizarContato()
        {
            return @"UPDATE TbContatos 
	                SET	
                        [Nome] = @Nome, 
                        [Email] = @Email,
                        [Telefone] = @Telefone,
                        [Empresa] = @Empresa,
                        [Cargo] = @Cargo
	                WHERE 
		                [Id] = @Id";
        }

        internal string ObtemQueryDeletarContato()
        {
            return @"DELETE FROM TbContatos 	                
	                WHERE 
		                [ID] = @ID";
        }
        #endregion
    }
}
