using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleTarefas.ConsoleApp.Infra
{
    class CompromissoDao
    {
        #region Scripts
        internal string ObtemQueryInsercaoCompromissoSemContato()
        {
            return @"INSERT INTO TbCompromissos
	                (
		                [Assunto], 
		                [Localizacao],
                        [Link],
                        [DataInicio],
                        [DataFinal]
	                ) 
	                VALUES
	                (
                        @Assunto, 
		                @Localizacao, 
		                @Link,
                        @DataInicio,
                        @DataFinal
	                )";
        }

        internal string ObtemQueryInsercaoCompromissoComContato()
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
                        @Contato_Id
	                )";
        }

        internal string ObtemQuerySelecionarTodosCompromissos()
        {
            return @"SELECT 
                        comp.Id,
                        [Assunto], 
		                [Localizacao],
                        [Link],
                        [DataInicio],
                        [DataFinal],
                        [TbContatos_Id]
                    FROM 
                        TbCompromissos comp";
        }
        internal string ObtemQuerySelecionarCompromissoPorId()
        {
            return @"SELECT 
                        [Assunto], 
		                [Localizacao],
                        [Link],
                        [DataInicio],
                        [DataFinal],
                        [Contato_Id]
                    FROM 
                        TbCompromissos
                    WHERE 
                        [ID] = @ID";
        }

        //internal string ObtemQuerySelecionarCompromissoPor()
        //{
        //    return @"SELECT 
        //                [Assunto], 
		      //          [Localizacao],
        //                [Link],
        //                [DataInicio],
        //                [DataFinal],
        //                [Contato_Id]
        //            FROM 
        //                TbCompromissos
        //            WHERE 
        //                [Cargo] = @Cargo";
        //}
        internal string ObtemQueryAtualizarCompromisso()
        {
            return @"UPDATE TbContatos 
	                SET	
                        [Assunto], 
		                [Localizacao],
                        [Link],
                        [DataInicio],
                        [DataFinal],
                        [Contato_Id]
	                WHERE 
		                [Id] = @Id";
        }

        internal string ObtemQueryDeletarCompromisso()
        {
            return @"DELETE FROM TbCompromissos 	                
	                WHERE 
		                [ID] = @ID";
        }
        #endregion
    }
}
