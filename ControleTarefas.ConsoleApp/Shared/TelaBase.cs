using eAgenda.Dominio.Shared;
using System;
using System.Collections.Generic;

namespace eAgenda.ConsoleApp.Shared
{
    public abstract class TelaBase
    {
        private string titulo;

        public string Titulo { get { return titulo; } }
        public TelaBase(string tit)
        {
            titulo = tit;
        }
        public string ObterOpcao()
        {
            string opcao;
            do
            {
                Console.Clear();

                Console.WriteLine("Digite 1 para inserir novo registro");
                Console.WriteLine("Digite 2 para visualizar registros");
                Console.WriteLine("Digite 3 para editar registro");
                Console.WriteLine("Digite 4 para excluir registro");
                Console.WriteLine("Digite S para Voltar");
                Console.WriteLine();

                Console.Write("Opção: ");
                opcao = Console.ReadLine();

            } while (ValidaOpcao(opcao));
            Console.Clear();
            return opcao;
        }
        public virtual void InserirNovoRegistro() { }
        public virtual void VisualizarRegistros() { }
        public virtual void EditarRegistro() { }
        public virtual void ExcluirRegistro() { }

        #region Métodos privados
        protected void VerificaRegistrosBanco(List<EntidadeBase> todosRegistros)
        {
            if (todosRegistros.Count < 1)
            {
                Console.WriteLine("Nenhuma tarefa criada até o momento!!");
                Console.ReadLine();
                return;
            }
        }
        private static bool ValidaOpcao(string opcao)
        {
            if (opcao != "1" && opcao != "2" && opcao != "3" && opcao != "4" && opcao != "s" && opcao != "S")
            {
                Console.WriteLine("Opção inválida, tente novamente!!");
                Console.ReadLine();
                return true;
            }
            else
                return false;
        }
        #endregion
    }
}
