using System.Data.SqlClient;

namespace ControleTarefas.ConsoleApp.Infra
{
    public class Db
    {
        public Db()
        {
        }

        public void ResetaDadosEIdDB()
        {
            string enderecoDb = EnderecoDbControleTarefas();
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDb;
            conexaoComBanco.Open();

            SqlCommand comandoResetar = new SqlCommand();
            comandoResetar.Connection = conexaoComBanco;

            string sqlResetaID = @"DELETE FROM TbTarefas; DBCC CHECKIDENT('TbTarefas', RESEED, 0)";

            comandoResetar.CommandText = sqlResetaID;
            comandoResetar.ExecuteScalar();
        }

        private static string EnderecoDbControleTarefas()
        {
            return @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBControleTarefas;Integrated Security=True;Pooling=False";
        }

        internal SqlConnection AbrirConexaoBanco()
        {
            string enderecoDb = EnderecoDbControleTarefas();
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDb;
            conexaoComBanco.Open();

            return conexaoComBanco;
        }

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
        internal string ObtemQuerySelecionarPorId()
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
                        [PercentualConclusao] = @PercentualConclusao
	                WHERE 
		                [Id] = @Id";
        }

        internal string ObtemQueryDeletarTarefa()
        {
            return @"DELETE FROM TbTarefas 	                
	                WHERE 
		                [ID] = @ID";
        }        
    }
}
