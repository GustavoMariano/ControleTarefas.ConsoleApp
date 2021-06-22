using ControleTarefasEContatos.ConsoleApp.Controlador;
using ControleTarefasEContatos.ConsoleApp.Dominio;
using System.Collections.Generic;
using System;

namespace ControleTarefasEContatos.ConsoleApp.Tela
{
    public class TelaContato : TelaBase
    {
        private Controlador<Contato> controlador;

        public TelaContato(string titulo) : base(titulo)
        {
            this.controlador = new ControladorContato();
        }

        public override void InserirNovoRegistro()
        {
            Console.Clear();

            string resultadoValidacao = "a";

            while (resultadoValidacao != "Contato cadastrado com sucesso!!")
            {
                resultadoValidacao = (GravarContato());
                Console.WriteLine(resultadoValidacao);
                Console.ReadLine();
                Console.Clear();
            }
        }

        public override void VisualizarRegistros()
        {
            Console.WriteLine("1 - Todos os contatos\n2 - Filtrar por cargo");
            string opcao = Console.ReadLine();
            switch (opcao)
            {
                case "1": VisualizarTodosOsContatos(); break;
                case "2": VisualizarContatosPorCargo(); break;
                default: break;
            }
        }

        private void VisualizarContatosPorCargo()
        {
            Console.WriteLine("Digite o cargo que deseja filtrar");
            string cargo = Console.ReadLine();
            Console.Clear();

            string configuracaColunasTabela = "{0,-05} | {1,-15} | {2,-10} | {3,-15} | {4,-20} | {5,-20}";

            MontarCabecalhoTabela(configuracaColunasTabela);

            List<Contato> todosContatos = controlador.SelecionarTodosOsRegistrosDoBanco();

            List<Contato> contatosPorCargo = new List<Contato>();

            foreach (var item in todosContatos)
            {
                if (item.Cargo == cargo)
                    contatosPorCargo.Add(item);
            }

            if (todosContatos.Count < 1)
            {
                Console.WriteLine("Nenhuma tarefa criada até o momento!!");
                return;
            }

            foreach (var tarefa in todosContatos)
            {
                Console.WriteLine(configuracaColunasTabela, tarefa.id, tarefa.Nome, tarefa.Email, tarefa.Telefone, tarefa.Empresa, tarefa.Cargo);
            }

            Console.ReadLine();
        }

        private void VisualizarTodosOsContatos()
        {
            string configuracaColunasTabela = "{0,-05} | {1,-15} | {2,-10} | {3,-15} | {4,-20} | {5,-20}";

            MontarCabecalhoTabela(configuracaColunasTabela);

            List<Contato> todosContatos = controlador.SelecionarTodosOsRegistrosDoBanco();

            if (todosContatos.Count < 1)
            {
                Console.WriteLine("Nenhuma tarefa criada até o momento!!");
                return;
            }

            foreach (var tarefa in todosContatos)
            {
                Console.WriteLine(configuracaColunasTabela, tarefa.id, tarefa.Nome, tarefa.Email, tarefa.Telefone, tarefa.Empresa, tarefa.Cargo);
            }

            Console.ReadLine();
        }

        private string GravarContato(int id = 0)
        {
            Contato contato;

            Console.WriteLine("Digite o nome do contato ");
            string nome = Console.ReadLine();
            Console.WriteLine("Digite o email do contato ");
            string email = Console.ReadLine();
            Console.WriteLine("Digite o telefone do contato ");
            string telefone = Console.ReadLine();
            Console.WriteLine("Digite a empresa do contato ");
            string empresa = Console.ReadLine();
            Console.WriteLine("Digite o cargo do contato ");
            string cargo = Console.ReadLine();

            contato = new Contato(nome, email, telefone, empresa, cargo);
            if (id != 0)
            {
                controlador.Editar(contato, id);
                return "Contato editado com sucesso!!";
            }
            else if(contato.Validar())
            {
                controlador.Inserir(contato);
                return "Contato criado com sucesso!!";
            }  
            else
                return "Contato inválido";
        }

        private static void MontarCabecalhoTabela(string configuracaoColunasTabela)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(configuracaoColunasTabela, "Id", "Titulo", "Prioridade", "% Conclusão", "Data Criação", "Data Conclusão");

            Console.WriteLine("---------------------------------------------------------------------------------------------------------");

            Console.ResetColor();
        }
    }
}
