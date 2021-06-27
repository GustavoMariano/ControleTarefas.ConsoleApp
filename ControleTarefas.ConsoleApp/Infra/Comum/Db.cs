using System.Data.SqlClient;

namespace ControleTarefasEContatos.ConsoleApp.Infra.Comum
{
    public class Db
    {
        public Db()
        {
        }

        public void ResetaDadosEIdDB()
        {
            string enderecoDb = EnderecoDbControleTarefasEContatos();
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDb;
            conexaoComBanco.Open();

            SqlCommand comandoResetar = new SqlCommand();
            comandoResetar.Connection = conexaoComBanco;

            string sqlResetaID = @"DELETE FROM TbTarefas; 
                                   DBCC CHECKIDENT('TbTarefas', RESEED, 0);
                                   DELETE FROM TbCompromissos; 
                                   DBCC CHECKIDENT('TbCompromissos', RESEED, 0);
                                   DELETE FROM TbContatos; 
                                   DBCC CHECKIDENT('TbContatos', RESEED, 0)";

            comandoResetar.CommandText = sqlResetaID;
            comandoResetar.ExecuteScalar();
        }

        private static string EnderecoDbControleTarefasEContatos()
        {
            return @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBControleTarefas;Integrated Security=True;Pooling=False";
        }

        internal SqlConnection AbrirConexaoBanco()
        {
            string enderecoDb = EnderecoDbControleTarefasEContatos();
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDb;
            conexaoComBanco.Open();

            return conexaoComBanco;
        }

                
    }
}
