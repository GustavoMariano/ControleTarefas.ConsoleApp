using System.Collections.Generic;
using System.Data.SqlClient;
using eAgenda.Controladores.Infra.Comum;
using eAgenda.Dominio.Shared;

namespace eAgenda.Controladores.Shared
{
    public abstract class Controlador<T> where T : EntidadeBase
    {
        public List<T> registros;
        public Db db = new Db();
        public Controlador()
        {
            registros = new List<T>();
        }       
        //public bool InserirRegistro(T registro)
        //{
        //    if (registro.Validar())
        //    {
        //        Inserir(registro);
        //        return true;
        //    }
        //    return false;
        //}
        public T SelecionarRegistroPorId(int id)
        {
            SelecionarTodosOsRegistrosDoBanco();
            return registros.Find(x => x.id == id);
        }
        public List<T> SelecionarTodosOsRegistrosDoBanco(string complementoDaQuery = "")
        {
            SqlConnection conexaoComBanco;
            SqlCommand comando;
            AbrirConexaoComBanco(out conexaoComBanco, out comando);

            string sqlSelecao = PegarStringSelecao();

            comando.CommandText = sqlSelecao;
            SqlDataReader leitorRegistro = comando.ExecuteReader();

            registros = SelecionarTodosOsRegistros(leitorRegistro);
            conexaoComBanco.Close();
            return registros;
        }
        private void AbrirConexaoComBanco(out SqlConnection conexaoComBanco, out SqlCommand comando)
        {
            conexaoComBanco = db.AbrirConexaoBanco();
            comando = new SqlCommand();
            comando.Connection = conexaoComBanco;
        }
        public bool EditarRegistro(int id, T registro)
        {
            bool idExiste = SelecionarRegistroPorId(id) != null;
            if (idExiste && registro.Validar())
            {
                Editar(registro, id);
                return true;
            }
            return false;
        }
        public bool ExcluirRegistro(int id)
        {
            bool idExiste = SelecionarRegistroPorId(id) != null;
            if (idExiste)
            {
                Excluir(id);
                return true;
            }
            return false;
        }
        public abstract void Inserir(T registro);
        public abstract void Editar(T registro, int id);
        public abstract void Excluir(int id);
        public abstract string PegarStringSelecao();
        public abstract List<T> SelecionarTodosOsRegistros(SqlDataReader leitorRegistro);
    }
}
