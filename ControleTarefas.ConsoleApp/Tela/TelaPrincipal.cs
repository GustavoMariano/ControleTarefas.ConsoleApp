using System;
using ControleTarefas.ConsoleApp.Controlador;
using ControleTarefas.ConsoleApp.Dominio;

namespace ControleTarefas.ConsoleApp.Tela
{
    public class TelaPrincipal : TelaBase
    {
        private readonly Controlador<Tarefa> controlador;

        private readonly TelaAdicionar<Tarefa> telaAdicionar;
        private readonly TelaVisualizar<Tarefa> telaVisualizar;
        private readonly TelaEditar<Tarefa> telaEditar;        
        private readonly TelaExcluir<Tarefa> telaExcluir;

        public TelaPrincipal() : base("Tela Principal")
        {
            controlador = new Controlador<Tarefa>();

            telaAdicionar = new TelaAdicionar<Tarefa>("Adicionar Tarefa");
            telaVisualizar = new TelaVisualizar<Tarefa>("Visualizar Tarefas");
            telaEditar = new TelaEditar<Tarefa>("Editar Tarefa");
            telaExcluir = new TelaExcluir<Tarefa>("Encerrar Tarefa");
        }

        public TelaBase ObterTela()
        {
            ConfigurarTela("Escolha uma opção: ");

            TelaBase telaSelecionada = null;
            string opcao = "0";
            do
            {
                Console.Clear();

                Console.WriteLine("Digite 1 para inserir nova tarefa");
                Console.WriteLine("Digite 2 para visualizar tarefas");
                Console.WriteLine("Digite 3 para editar tarefa");
                Console.WriteLine("Digite 4 para excluir tarefa");
                Console.WriteLine("Digite S para Voltar");
                Console.WriteLine();
                Console.Write("Opção: ");
                opcao = Console.ReadLine();

                if (opcao.Equals("s", StringComparison.OrdinalIgnoreCase))
                    Environment.Exit(0);

                switch (opcao)
                {
                    case "1": telaSelecionada = telaAdicionar; break;
                    case "2": telaSelecionada = telaVisualizar; break;
                    case "3": telaSelecionada = telaEditar; break;
                    case "4": telaSelecionada = telaExcluir; break;
                    default:
                        break;
                }              

            } while (OpcaoInvalida(opcao));

            return telaSelecionada;
        }

        private bool OpcaoInvalida(string opcao)
        {
            if (opcao != "1" && opcao != "2" && opcao != "3" && opcao != "4" && opcao != "S" && opcao != "s")
            {
                ApresentarMensagem("Opção inválida", TipoMensagem.Erro);
                return true;
            }
            else
                return false;
        }
    }
}
