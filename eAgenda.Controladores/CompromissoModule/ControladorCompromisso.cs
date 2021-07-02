using eAgenda.Controladores.Shared;
using eAgenda.Dominio.CompromissoModule;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace eAgenda.Controladores.CompromissoModule
{
    public class ControladorCompromisso : Controlador<Compromisso>
    {
        #region Querys
        private string ObtemQueryInsercaoCompromisso()
        {
            return @"INSERT INTO TbCompromissos
	                (
		                [Assunto], 
		                [Localizacao],
                        [Link],
                        [DataInicio],
                        [DataFinal],
                        [TbContatos_Id]
	                ) 
	                VALUES
	                (
                        @Assunto, 
		                @Localizacao, 
		                @Link,
                        @DataInicio,
                        @DataFinal,
                        @ContatoId
	                )";
        }
        private string ObtemQuerySelecionarTodosCompromissos()
        {
            return @"SELECT 
                        comp.[Id],
                        [Assunto], 
		                [Localizacao],
                        [Link],
                        [DataInicio],
                        [DataFinal],
                        [TbContatos_Id], Nome
                    FROM 
                        TbCompromissos comp
                        LEFT JOIN TbContatos cont
                        ON TbContatos_Id = cont.Id
                        ";
        }
        private string ObtemQuerySelecionarCompromissoPorId()
        {
            return @"SELECT 
                        comp.[Id],
                        [Assunto], 
		                [Localizacao],
                        [Link],
                        [DataInicio],
                        [DataFinal],
                        [TbContato_Id], Nome
                    FROM 
                        TbCompromissos comp
                        LEFT JOIN TbContatos cont
                        ON TbContatos_Id = cont.Id
                    WHERE 
                        [ID] = @ID";
        }
        private string ObtemQueryAtualizarCompromisso()
        {
            return @"UPDATE TbCompromissos 
	                SET	
                        [Assunto] = @Assunto, 
		                [Localizacao] = @Localizacao,
                        [Link] = @Link,
                        [DataInicio] = @DataInicio,
                        [DataFinal] = @DataFinal,
                        [TbContatos_Id] = @TbContato_Id
	                WHERE 
		                [Id] = @Id";
        }
        private string ObtemQueryDeletarCompromisso()
        {
            return @"DELETE FROM TbCompromissos 	                
	                WHERE 
		                [ID] = @ID";
        }
        private string ObtemQueryVerificarDataUsada()
        {
            return @"SELECT COUNT(*)
                    FROM TbCompromissos
                    WHERE [DataInicio] = @DataInicio
                    AND
                        @DataInicio BETWEEN DataInicio AND DataInicio
                    OR
                        @DataFinal BETWEEN DataInicio AND DataFinal";
        }
        #endregion
        public override void Inserir(Compromisso compromisso)
        {
            SqlConnection conexaoComBanco;
            SqlCommand comando;
            AbrirConexaoComBanco(out conexaoComBanco, out comando);
            string sqlInsercao = "";
            //string sqlVerificaData = "";

            //sqlVerificaData = ObtemQueryVerificarDataUsada();
            //comando.Parameters.AddWithValue("DataInicio", compromisso.DataInicioCompromisso);
            //comando.Parameters.AddWithValue("DataFinal", compromisso.DataFinalCompromisso);

            //int id = Convert.ToInt32(comando.ExecuteScalar());
            //if (id > 0)
            //    return;

            sqlInsercao = ObtemQueryInsercaoCompromisso();            

            sqlInsercao += @"SELECT SCOPE_IDENTITY();";
            comando.CommandText = sqlInsercao;

            comando.Parameters.AddWithValue("Assunto", compromisso.Assunto);
            comando.Parameters.AddWithValue("Localizacao", compromisso.Localizacao);
            comando.Parameters.AddWithValue("DataInicio", compromisso.DataInicioCompromisso);
            comando.Parameters.AddWithValue("DataFinal", compromisso.DataFinalCompromisso);
            comando.Parameters.AddWithValue("Link", compromisso.LinkReuniao);
            if (compromisso.IdContato != 0)
                comando.Parameters.AddWithValue("ContatoId", compromisso.IdContato);
            else
                comando.Parameters.AddWithValue("ContatoId", DBNull.Value);

            object idInserido = comando.ExecuteScalar();

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

            string sqlAtualizacao = ObtemQueryAtualizarCompromisso();

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

            string sqlExclusao = ObtemQueryDeletarCompromisso();

            comando.CommandText = sqlExclusao;
            comando.Parameters.AddWithValue("ID", id);
            comando.ExecuteNonQuery();

            conexaoComBanco.Close();
        }
        public override string PegarStringSelecao()
        {
            return ObtemQuerySelecionarTodosCompromissos();
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
