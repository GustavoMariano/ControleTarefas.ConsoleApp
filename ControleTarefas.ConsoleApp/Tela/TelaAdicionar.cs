using System;
using ControleTarefasEContatos.ConsoleApp.Controlador;
using ControleTarefasEContatos.ConsoleApp.Dominio;

namespace ControleTarefasEContatos.ConsoleApp.Tela
{
    public class TelaAdicionar<TipoTarefa> : TelaBase
    {
        private readonly Controlador<Tarefa> controlador;

        public TelaAdicionar(string tit) : base(tit)
        {
            //controlador = new Controlador<Tarefa>();
        }

        public override Tarefa ObterOpcao()
        {
            Console.Write("Digite o titulo da tarefa: ");
            string titulo = Console.ReadLine();

            Console.Write("Defina a prioridade (1 - Baixa, 2 - Média, 3 - Alta");
            int prioridade = Convert.ToInt32(Console.ReadLine());

            Tarefa tarefa = new Tarefa(titulo, prioridade);

            return new Tarefa(titulo, prioridade);
        }
    }
}
