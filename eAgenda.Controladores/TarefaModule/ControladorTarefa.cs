using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using eAgenda.Controladores.Shared;
using eAgenda.Dominio.TarefaModule;

namespace eAgenda.Controladores.TarefaModule
{
    public class ControladorTarefa : Controlador<Tarefa>
    {
        #region Queries
        private string ObtemQueryInsercaoTarefa()
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
        private string ObtemQuerySelecionarTodasTarefa()
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
        private string ObtemQuerySelecionarTarefaPorId()
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
        private string ObtemQueryAtualizarTarefa()
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
        private string ObtemQueryDeletarTarefa()
        {
            return @"DELETE FROM TbTarefas 	                
	                WHERE 
		                [ID] = @ID";
        }
        private string ObtemQuerySelecionarTarefasFinalizadas()
        {
            return @"select * from TbTarefas where PercentualConclusao >= 100 order by Prioridade";
        }
        internal string ObtemQuerySelecionarTarefasEmAberto()
        {
            return @"select * from TbTarefas where PercentualConclusao < 100 order by Prioridade";
        }
        #endregion
        public override void Inserir(Tarefa tarefa)
        {
            SqlConnection conexaoComBanco;
            SqlCommand comando;
            AbrirConexaoComBanco(out conexaoComBanco, out comando);

            string sqlInsercao = ObtemQueryInsercaoTarefa();

            sqlInsercao += @"SELECT SCOPE_IDENTITY();";

            comando.CommandText = sqlInsercao;

            comando.Parameters.AddWithValue("Titulo", tarefa.Titulo);
            comando.Parameters.AddWithValue("Prioridade", tarefa.Prioridade);
            comando.Parameters.AddWithValue("DataCriacao", tarefa.DataCriacao);
            comando.Parameters.AddWithValue("Percentual", tarefa.PercentualConcluido);

            comando.ExecuteScalar();

            conexaoComBanco.Close();
        }
        public override void Editar(Tarefa tarefa, int idSelecionado)
        {
            SqlConnection conexaoComBanco;
            SqlCommand comando;
            AbrirConexaoComBanco(out conexaoComBanco, out comando);

            string sqlAtualizacao = ObtemQueryAtualizarTarefa();

            comando.CommandText = sqlAtualizacao;
            comando.Parameters.AddWithValue("Titulo", tarefa.Titulo);
            comando.Parameters.AddWithValue("Prioridade", tarefa.Prioridade);
            comando.Parameters.AddWithValue("DataCriacao", tarefa.DataCriacao);
            comando.Parameters.AddWithValue("DataConclusao", tarefa.DataConclusao);
            comando.Parameters.AddWithValue("Percentual", tarefa.PercentualConcluido);
            comando.Parameters.AddWithValue("ID", idSelecionado);

            comando.ExecuteNonQuery();

            conexaoComBanco.Close();
        }
        public override void Excluir(int id)
        {
            SqlConnection conexaoComBanco;
            SqlCommand comando;
            AbrirConexaoComBanco(out conexaoComBanco, out comando);

            string sqlExclusao = ObtemQueryDeletarTarefa();

            comando.CommandText = sqlExclusao;
            comando.Parameters.AddWithValue("ID", id);
            comando.ExecuteNonQuery();

            conexaoComBanco.Close();
        }
        public override List<Tarefa> SelecionarTodosOsRegistros(SqlDataReader leitorRegistro)
        {
            List<Tarefa> tarefas = new List<Tarefa>();
            while (leitorRegistro.Read())
            {
                DateTime dataConclusao = DateTime.MinValue;
                int id = Convert.ToInt32(leitorRegistro["Id"]);
                string titulo = Convert.ToString(leitorRegistro["Titulo"]);
                int prioridade = Convert.ToInt32(leitorRegistro["Prioridade"]);
                DateTime dataDeCriacao = Convert.ToDateTime(leitorRegistro["DataCriacao"]);
                if (leitorRegistro["DataConclusao"] != DBNull.Value)
                    dataConclusao = Convert.ToDateTime(leitorRegistro["DataConclusao"]);
                int percentualConclusao = Convert.ToInt32(leitorRegistro["PercentualConclusao"]);
                Tarefa tarefa = new Tarefa(id, titulo, prioridade, dataDeCriacao, dataConclusao, percentualConclusao);

                tarefas.Add(tarefa);
            }
            return tarefas;
        }
        public override string PegarStringSelecao()
        {
            return ObtemQuerySelecionarTodasTarefa();
        }
        public List<Tarefa> SelecionarTarefasFinalizadas()
        {
            SqlConnection conexaoComBanco;
            SqlCommand comando;
            AbrirConexaoComBanco(out conexaoComBanco, out comando);

            string sqlFinalizadas = ObtemQuerySelecionarTarefasFinalizadas();

            comando.CommandText = sqlFinalizadas;
            SqlDataReader leitorRegistro = comando.ExecuteReader();

            List<Tarefa> tarefas = new List<Tarefa>();

            while (leitorRegistro.Read())
            {
                DateTime dataConclusao = DateTime.MinValue;
                int id = Convert.ToInt32(leitorRegistro["Id"]);
                string titulo = Convert.ToString(leitorRegistro["Titulo"]);
                int prioridade = Convert.ToInt32(leitorRegistro["Prioridade"]);
                DateTime dataDeCriacao = Convert.ToDateTime(leitorRegistro["DataCriacao"]);
                if (leitorRegistro["DataConclusao"] != DBNull.Value)
                    dataConclusao = Convert.ToDateTime(leitorRegistro["DataConclusao"]);
                int percentualConclusao = Convert.ToInt32(leitorRegistro["PercentualConclusao"]);
                Tarefa tarefa = new Tarefa(id, titulo, prioridade, dataDeCriacao, dataConclusao, percentualConclusao);

                tarefas.Add(tarefa);
            }
            return tarefas;
        }
        public List<Tarefa> SelecionarTarefasAbertas()
        {
            SqlConnection conexaoComBanco;
            SqlCommand comando;
            AbrirConexaoComBanco(out conexaoComBanco, out comando);

            string sqlFinalizadas = ObtemQuerySelecionarTarefasEmAberto();

            comando.CommandText = sqlFinalizadas;
            SqlDataReader leitorRegistro = comando.ExecuteReader();

            List<Tarefa> tarefas = new List<Tarefa>();

            while (leitorRegistro.Read())
            {
                DateTime dataConclusao = DateTime.MinValue;
                int id = Convert.ToInt32(leitorRegistro["Id"]);
                string titulo = Convert.ToString(leitorRegistro["Titulo"]);
                int prioridade = Convert.ToInt32(leitorRegistro["Prioridade"]);
                DateTime dataDeCriacao = Convert.ToDateTime(leitorRegistro["DataCriacao"]);
                if (leitorRegistro["DataConclusao"] != DBNull.Value)
                    dataConclusao = Convert.ToDateTime(leitorRegistro["DataConclusao"]);
                int percentualConclusao = Convert.ToInt32(leitorRegistro["PercentualConclusao"]);
                Tarefa tarefa = new Tarefa(id, titulo, prioridade, dataDeCriacao, dataConclusao, percentualConclusao);

                tarefas.Add(tarefa);
            }
            return tarefas;
        }
        #region Métodos Privados
        private void AbrirConexaoComBanco(out SqlConnection conexaoComBanco, out SqlCommand comando)
        {
            conexaoComBanco = db.AbrirConexaoBanco();
            comando = new SqlCommand();
            comando.Connection = conexaoComBanco;
        }
        #endregion
    }
}
