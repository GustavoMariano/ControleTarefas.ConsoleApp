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
        internal string ObtemQueryInsercaoCompromisso()
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

        internal string ObtemQuerySelecionarTodosCompromissos()
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
        internal string ObtemQuerySelecionarCompromissoPorId()
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

        internal string ObtemQueryAtualizarCompromisso()
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

        internal string ObtemQueryDeletarCompromisso()
        {
            return @"DELETE FROM TbCompromissos 	                
	                WHERE 
		                [ID] = @ID";
        }
        #endregion
    }
}
