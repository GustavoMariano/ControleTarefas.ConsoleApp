using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleTarefas.ConsoleApp.Controlador;
using ControleTarefas.ConsoleApp.Dominio;

namespace ControleTarefas.ConsoleApp.Tela
{
    public class TelaAdicionar<TipoTarefa> : TelaBase
    {
        private readonly Controlador<Tarefa> controlador;

        public TelaAdicionar(string tit) : base(tit)
        {
            controlador = new Controlador<Tarefa>();
        }

        public override Tarefa ObterOpcao()
        {
            Console.Write("Digite o titulo da tarefa: ");
            string titulo = Console.ReadLine();

            Console.Write("Defina a prioridade (1 - Baixa, 2 - Média, 3 - Alta");
            int prioridade = Convert.ToInt32(Console.ReadLine());

            //Console.Write("Digite a data de criação: ");
            //DateTime dataCriacao = Convert.ToDateTime(Console.ReadLine());

            Tarefa tarefa = new Tarefa(titulo, prioridade);

            return new Tarefa(titulo, prioridade);
        }
    }
}
