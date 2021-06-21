using ControleTarefasEContatos.ConsoleApp.Dominio;
using ControleTarefasEContatos.ConsoleApp.Infra;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleTarefasEContatos.ConsoleApp.Controlador
{
    public class ControladorTarefa : Controlador<Tarefa>
    {
        TarefaDao tarefaDao = new TarefaDao();
        public override void Inserir(Tarefa tarefa)
        {
            SqlConnection conexaoComBanco;
            SqlCommand comando;
            AbrirConexaoComBanco(out conexaoComBanco, out comando);

            string sqlInsercao = tarefaDao.ObtemQueryInsercaoTarefa();

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

            string sqlAtualizacao = tarefaDao.ObtemQueryAtualizarTarefa();

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

            string sqlExclusao = tarefaDao.ObtemQueryDeletarTarefa();

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
            return tarefaDao.ObtemQuerySelecionarTodasTarefa();
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
