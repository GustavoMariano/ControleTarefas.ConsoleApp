using ControleTarefas.ConsoleApp.Infra;
using ControleTarefasEContatos.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleTarefasEContatos.ConsoleApp.Controlador
{
    public class ControladorCompromisso : Controlador<Compromisso>
    {
        CompromissoDao compromissoDao = new CompromissoDao();
        public override void Inserir(Compromisso compromisso)
        {
            SqlConnection conexaoComBanco;
            SqlCommand comando;
            AbrirConexaoComBanco(out conexaoComBanco, out comando);
            string sqlInsercao = "";

            if (compromisso.IdContato == 0)
                sqlInsercao = compromissoDao.ObtemQueryInsercaoCompromissoSemContato();
            else
            {
                sqlInsercao = compromissoDao.ObtemQueryInsercaoCompromissoComContato();
                comando.Parameters.AddWithValue("Contato_Id", compromisso.IdContato);
            }

            sqlInsercao += @"SELECT SCOPE_IDENTITY();";
            comando.CommandText = sqlInsercao;

            comando.Parameters.AddWithValue("Assunto", compromisso.Assunto);
            comando.Parameters.AddWithValue("Localizacao", compromisso.Localizacao);            
            comando.Parameters.AddWithValue("DataInicio", compromisso.DataInicioCompromisso);
            comando.Parameters.AddWithValue("DataFinal", compromisso.DataFinalCompromisso);
            comando.Parameters.AddWithValue("Link", compromisso.LinkReuniao);

            object id = comando.ExecuteScalar();

            conexaoComBanco.Close();
        }
        public override List<Compromisso> SelecionarTodosOsRegistros(SqlDataReader leitorRegistro)
        {
            List<Compromisso> listaCompromissos = new List<Compromisso>();
            while (leitorRegistro.Read())
            {
                int id = Convert.ToInt32(leitorRegistro["Id"]);
                string assunto = Convert.ToString(leitorRegistro["Assunto"]);
                string localizacao = Convert.ToString(leitorRegistro["Localizacao"]);
                string link = Convert.ToString(leitorRegistro["Link"]);
                DateTime dataInicio = Convert.ToDateTime(leitorRegistro["DataInicio"]);
                DateTime dataFinal = Convert.ToDateTime(leitorRegistro["DataFinal"]);
                int idContato = 0;
                string nome = "";
                if (leitorRegistro["TbContatos_Id"] != DBNull.Value)
                {
                    idContato = Convert.ToInt32(leitorRegistro["TbContatos_Id"]);
                    nome = Convert.ToString(leitorRegistro["Nome"]);
                }
                Compromisso compromisso = new Compromisso(id, assunto, localizacao, idContato, dataInicio, dataFinal, link, nome);

                listaCompromissos.Add(compromisso);
            }
            return listaCompromissos;
        }
        public override void Editar(Compromisso compromisso, int idSelecionado)
        {
            SqlConnection conexaoComBanco;
            SqlCommand comando;
            AbrirConexaoComBanco(out conexaoComBanco, out comando);

            string sqlAtualizacao = compromissoDao.ObtemQueryAtualizarCompromisso();

            comando.CommandText = sqlAtualizacao;
            comando.Parameters.AddWithValue("Assunto", compromisso.Assunto);
            comando.Parameters.AddWithValue("Localizacao", compromisso.Localizacao);
            comando.Parameters.AddWithValue("Link", compromisso.LinkReuniao);
            comando.Parameters.AddWithValue("DataInicio", compromisso.DataInicioCompromisso);
            comando.Parameters.AddWithValue("DataFinal", compromisso.DataFinalCompromisso);
            comando.Parameters.AddWithValue("TbContato_Id", compromisso.IdContato);
            comando.Parameters.AddWithValue("Id", idSelecionado);

            comando.ExecuteNonQuery();

            conexaoComBanco.Close();
        }
        public override void Excluir(int id)
        {
            SqlConnection conexaoComBanco;
            SqlCommand comando;
            AbrirConexaoComBanco(out conexaoComBanco, out comando);

            string sqlExclusao = compromissoDao.ObtemQueryDeletarCompromisso();

            comando.CommandText = sqlExclusao;
            comando.Parameters.AddWithValue("ID", id);
            comando.ExecuteNonQuery();

            conexaoComBanco.Close();
        }
        public override string PegarStringSelecao()
        {
            return compromissoDao.ObtemQuerySelecionarTodosCompromissos();
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
