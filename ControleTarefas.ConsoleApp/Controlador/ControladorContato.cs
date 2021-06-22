using ControleTarefasEContatos.ConsoleApp.Dominio;
using ControleTarefasEContatos.ConsoleApp.Infra;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleTarefasEContatos.ConsoleApp.Controlador
{
    public class ControladorContato : Controlador<Contato>
    {
        ContatoDao contatoDao = new ContatoDao();

        public override void Inserir(Contato contato)
        {
            SqlConnection conexaoComBanco;
            SqlCommand comando;
            AbrirConexaoComBanco(out conexaoComBanco, out comando);

            string sqlInsercao = contatoDao.ObtemQueryInsercaoContato();

            sqlInsercao += @"SELECT SCOPE_IDENTITY();";

            comando.CommandText = sqlInsercao;

            comando.Parameters.AddWithValue("Nome", contato.Nome);
            comando.Parameters.AddWithValue("Email", contato.Email);
            comando.Parameters.AddWithValue("Telefone", contato.Telefone);
            comando.Parameters.AddWithValue("Empresa", contato.Empresa);
            comando.Parameters.AddWithValue("Cargo", contato.Cargo);

            comando.ExecuteScalar();

            conexaoComBanco.Close();
        }
        public List<Contato> ListarPorCargo(string cargo)
        {
            List<Contato> listaPorCargo = new List<Contato>();
            List<Contato> contatos = SelecionarTodosOsRegistrosDoBanco();
            foreach (var contato in contatos)
            {
                if (contato.Cargo == cargo)
                    listaPorCargo.Add(contato);
            }
            return listaPorCargo;
        }
        public override void Editar(Contato contato, int idSelecionado)
        {
            SqlConnection conexaoComBanco;
            SqlCommand comando;
            AbrirConexaoComBanco(out conexaoComBanco, out comando);

            string sqlAtualizacao = contatoDao.ObtemQueryAtualizarContato();

            comando.CommandText = sqlAtualizacao;
            comando.Parameters.AddWithValue("Nome", contato.Nome);
            comando.Parameters.AddWithValue("Email", contato.Email);
            comando.Parameters.AddWithValue("Telefone", contato.Telefone);
            comando.Parameters.AddWithValue("Empresa", contato.Empresa);
            comando.Parameters.AddWithValue("Cargo", contato.Cargo);
            comando.Parameters.AddWithValue("ID", idSelecionado);

            comando.ExecuteNonQuery();

            conexaoComBanco.Close(); ;
        }
        public override void Excluir(int id)
        {
            SqlConnection conexaoComBanco;
            SqlCommand comando;
            AbrirConexaoComBanco(out conexaoComBanco, out comando);

            string sqlExclusao = contatoDao.ObtemQueryDeletarContato();

            comando.CommandText = sqlExclusao;
            comando.Parameters.AddWithValue("ID", id);
            comando.ExecuteNonQuery();

            conexaoComBanco.Close();
        }        
        public override List<Contato> SelecionarTodosOsRegistros(SqlDataReader leitorRegistro)
        {
            List<Contato> contatos = new List<Contato>();
            while (leitorRegistro.Read())
            {
                int id = Convert.ToInt32(leitorRegistro["Id"]);
                string nome = Convert.ToString(leitorRegistro["Nome"]);
                string email = Convert.ToString(leitorRegistro["Email"]);
                string telefone = Convert.ToString(leitorRegistro["Telefone"]);
                string empresa = Convert.ToString(leitorRegistro["Empresa"]);
                string cargo = Convert.ToString(leitorRegistro["Cargo"]);
                Contato contato = new Contato(id, nome, email, telefone, empresa, cargo);

                contatos.Add(contato);
            }
            return contatos;
        }
        public override string PegarStringSelecao()
        {
            return contatoDao.ObtemQuerySelecionarTodosContatos();
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
