using System;
using ControleTarefasEContatos.ConsoleApp.Tela;

namespace ControleTarefasEContatos.ConsoleApp
{
    class Program
    {        
        static void Main(string[] args)
        {
            TelaPrincipal telaPrincipal = new TelaPrincipal("");

            while (true)
            {
                TelaBase telaSelecionada = (TelaBase)telaPrincipal.ObterOpcao("");

                if (telaSelecionada == null)
                    break;

                string opcao = "";

                Console.Clear();

                Console.WriteLine(((TelaBase)telaSelecionada).Titulo);

                opcao = telaSelecionada.ObterOpcao();

                switch (opcao)
                {
                    case "1": telaSelecionada.InserirNovoRegistro(); break;
                    case "2": telaSelecionada.VisualizarRegistros(); break;
                    case "3": telaSelecionada.EditarRegistro(); break;
                    case "4": telaSelecionada.ExcluirRegistro(); break;
                    default:
                        break;
                }
            }
        }
    }
}
