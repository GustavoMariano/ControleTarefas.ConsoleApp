using System;
using eAgenda.ConsoleApp.CompromissoModule;
using eAgenda.ConsoleApp.ContatoModule;
using eAgenda.ConsoleApp.TarefaModule;
using eAgenda.Controladores.CompromissoModule;
using eAgenda.Controladores.ContatoModule;
using eAgenda.Controladores.Shared;
using eAgenda.Controladores.TarefaModule;
using eAgenda.Dominio.CompromissoModule;
using eAgenda.Dominio.ContatoModule;
using eAgenda.Dominio.TarefaModule;

namespace eAgenda.ConsoleApp.Shared
{
    class TelaPrincipal : TelaBase
    {
        //static Controlador<Tarefa> controladorTarefa = new ControladorTarefa();
        //static Controlador<Contato> controladorContato = new ControladorContato();
        //static Controlador<Compromisso> controladorCompromisso = new ControladorCompromisso();

        static TelaTarefa telaTarefa = new TelaTarefa("Controle de Tarefas\n-------------------\n");
        static TelaContato telaContato = new TelaContato("Controle de Contatos\n-------------------\n");
        static TelaCompromisso telaCompromisso = new TelaCompromisso("Controle de Compromisso\n-------------------\n");

        public TelaPrincipal(string titulo) : base(titulo) { }

        public TelaBase ObterOpcao(string titulo)
        {
            TelaBase telaSelecionada = null;
            string opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("Menu Principal" + "\n---------------\n");
                Console.WriteLine("Digite 1 para o controle de tarefas");
                Console.WriteLine("Digite 2 para o controle de contatos");
                Console.WriteLine("Digite 3 para o controle de compromisso");
                Console.WriteLine("Digite s para Voltar");
                Console.WriteLine();
                Console.Write("Opção: ");
                opcao = Console.ReadLine();

                if (opcao.Equals("s", StringComparison.OrdinalIgnoreCase))
                    Environment.Exit(0);

                switch (opcao)
                {
                    case "1": telaSelecionada = telaTarefa; break;
                    case "2": telaSelecionada = telaContato; break;
                    case "3": telaSelecionada = telaCompromisso; break;
                    default:
                        break;
                }

            } while (OpcaoInvalida(opcao));

            return telaSelecionada;
        }

        private bool OpcaoInvalida(string opcao)
        {
            if (opcao != "1" && opcao != "2" && opcao != "3" && opcao != "S" && opcao != "s")
            {
                Console.WriteLine("Opção inválida");
                Console.ReadLine();
                Console.Clear();
                return true;
            }
            else
                return false;
        }
    }
}
