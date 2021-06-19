using ControleTarefas.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ControleTarefas.ConsoleApp.Infra;

namespace ControleTarefas.ConsoleApp.Controlador
{
    public class Controlador<T> where T : EntidadeBase
    {
        public Db db = new Db();
        public Controlador()
        {
        }

        public int InserirTarefa(Tarefa tarefa)
        {
            SqlConnection conexaoComBanco = db.AbrirConexaoBanco();

            SqlCommand comandoInsercao = new SqlCommand();
            comandoInsercao.Connection = conexaoComBanco;

            string sqlInsercao = db.ObtemQueryInsercaoTarefa();

            sqlInsercao += @"SELECT SCOPE_IDENTITY();";

            comandoInsercao.CommandText = sqlInsercao;
            comandoInsercao.Parameters.AddWithValue("Titulo", tarefa.Titulo);
            comandoInsercao.Parameters.AddWithValue("Prioridade", tarefa.Prioridade);
            comandoInsercao.Parameters.AddWithValue("DataCriacao", DateTime.Now);
            comandoInsercao.Parameters.AddWithValue("Percentual", tarefa.PercentualConcluido);

            object id = comandoInsercao.ExecuteScalar();
            tarefa.id = Convert.ToInt32(id);

            conexaoComBanco.Close();
            return tarefa.id;
        }

        public List<Tarefa> VisualizarTodasTarefas()
        {
            SqlConnection conexaoComBanco = db.AbrirConexaoBanco();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao = db.ObtemQuerySelecionarTodasTarefa();

            comandoSelecao.CommandText = sqlSelecao;

            SqlDataReader leitorTarefas = comandoSelecao.ExecuteReader();

            List<Tarefa> tarefa = new List<Tarefa>();

            while (leitorTarefas.Read())
            {
                DateTime dataConclusao = new DateTime(0001 / 01 / 01);
                int id = Convert.ToInt32(leitorTarefas["Id"]);
                string titulo = Convert.ToString(leitorTarefas["Titulo"]);
                int prioridade = Convert.ToInt32(leitorTarefas["Prioridade"]);
                DateTime dataAbertura = Convert.ToDateTime(leitorTarefas["DataCriacao"]);
                if (leitorTarefas["DataConclusao"] != DBNull.Value)
                    dataConclusao = Convert.ToDateTime(leitorTarefas["DataConclusao"]);
                int percentualConclusao = Convert.ToInt32(leitorTarefas["PercentualConclusao"]);

                Tarefa selecaoTarefa = new Tarefa(id, titulo, prioridade, dataAbertura, dataConclusao, percentualConclusao);

                tarefa.Add(selecaoTarefa);
            }

            conexaoComBanco.Close();
            return tarefa;
        }
        public Tarefa SelecionarPorId(int idSelecionado)
        {
            SqlConnection conexaoComBanco = db.AbrirConexaoBanco();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao = db.ObtemQuerySelecionarPorId();

            comandoSelecao.CommandText = sqlSelecao;
            comandoSelecao.Parameters.AddWithValue("ID", idSelecionado);

            SqlDataReader leitorTarefas = comandoSelecao.ExecuteReader();

            if (leitorTarefas.Read() == false)
                return null;
            DateTime dataConclusao = new DateTime(0001 / 01 / 01);
            int id = Convert.ToInt32(leitorTarefas["Id"]);
            string titulo = Convert.ToString(leitorTarefas["Titulo"]);
            int prioridade = Convert.ToInt32(leitorTarefas["Prioridade"]);
            DateTime dataCricao = Convert.ToDateTime(leitorTarefas["DataCriacao"]);
            if (leitorTarefas["DataConclusao"] != DBNull.Value)
                dataConclusao = Convert.ToDateTime(leitorTarefas["DataConclusao"]);
            int porcentagemConclusao = Convert.ToInt32(leitorTarefas["PercentualConclusao"]);

            Tarefa tarefaEditavel = new Tarefa(id, titulo, prioridade, dataCricao, dataConclusao, porcentagemConclusao);

            conexaoComBanco.Close();

            return tarefaEditavel;
        }        

        public void EditarTarefa(Tarefa tarefaEditavel)
        {
            SqlConnection conexaoComBanco = db.AbrirConexaoBanco();

            SqlCommand comandoAtualizacao = new SqlCommand();
            comandoAtualizacao.Connection = conexaoComBanco;
            string sqlAtualizacao = db.ObtemQueryAtualizarTarefa();

            comandoAtualizacao.CommandText = sqlAtualizacao;
            comandoAtualizacao.Parameters.AddWithValue("Id", tarefaEditavel.Id);
            comandoAtualizacao.Parameters.AddWithValue("Titulo", tarefaEditavel.Titulo);
            comandoAtualizacao.Parameters.AddWithValue("Prioridade", tarefaEditavel.Prioridade);
            comandoAtualizacao.Parameters.AddWithValue("DataConclusao", tarefaEditavel.DataConclusao);
            comandoAtualizacao.Parameters.AddWithValue("PercentualConclusao", tarefaEditavel.PercentualConcluido);

            comandoAtualizacao.ExecuteNonQuery();

            conexaoComBanco.Close();
        }                

        public void DeletarTarefa(int idDeletar)
        {
            SqlConnection conexaoComBanco = db.AbrirConexaoBanco();

            SqlCommand comandoExclusao = new SqlCommand();
            comandoExclusao.Connection = conexaoComBanco;
            string sqlExclusao = db.ObtemQueryDeletarTarefa();

            comandoExclusao.CommandText = sqlExclusao;

            comandoExclusao.Parameters.AddWithValue("ID", idDeletar);

            comandoExclusao.ExecuteNonQuery();

            conexaoComBanco.Close();
        }
    }
}
