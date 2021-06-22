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

            while (resultadoValidacao != "Sucesso!!")
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

            ControladorContato controladorContato = new ControladorContato();
            List<Contato> listaPorCargo = controladorContato.ListarPorCargo(cargo);

            if (listaPorCargo.Count < 1)
            {
                Console.WriteLine("Nenhuma tarefa criada até o momento!!");
                return;
            }

            foreach (var tarefa in listaPorCargo)
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

        public override void EditarRegistro()
        {
            Console.Clear();

            VisualizarTodosOsContatos();

            Console.WriteLine();

            List<Contato> todosContatos = controlador.SelecionarTodosOsRegistrosDoBanco();
            if (todosContatos.Count == 0)
            {
                Console.ReadLine();
                return;
            }

            Console.Write("Digite o ID da contato que deseja editar: ");
            int idSelecionado;
            Int32.TryParse(Console.ReadLine(), out idSelecionado);

            Contato ContatoSelecionado = controlador.SelecionarRegistroPorId(idSelecionado);

            if (ContatoSelecionado == null)
            {
                Console.WriteLine("ID não encontrado, tente novamente!!");
                Console.ReadLine();
            }
            else
            {
                string resultadoValidacao = (GravarContato(idSelecionado));

                if (resultadoValidacao == "Contato cadastrado com sucesso!!")
                {
                    Console.WriteLine("Contato editado com sucesso");
                    Console.ReadLine();
                    Console.Clear();
                }
                else
                    Console.WriteLine(resultadoValidacao);
            }
        }
        public override void ExcluirRegistro()
        {
            Console.Clear();

            VisualizarTodosOsContatos();

            Console.WriteLine();

            Console.Write("Digite o ID da contato que deseja excluir: ");
            int idSelecionado;
            Int32.TryParse(Console.ReadLine(), out idSelecionado);

            Contato contato = controlador.SelecionarRegistroPorId(idSelecionado);

            if (contato.Nome == null)
            {
                Console.WriteLine("Id não encontrado, tente novamente!!");
                Console.ReadLine();
            }
            else
            {
                controlador.Excluir(idSelecionado);
                Console.WriteLine("Registro excluído com sucesso");
                Console.ReadLine();
            }
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
                return "Sucesso!!";
            }
            else if(contato.Validar())
            {
                controlador.Inserir(contato);
                return "Sucesso!!";
            }  
            else
                return "Contato inválido";
        }

        private static void MontarCabecalhoTabela(string configuracaoColunasTabela)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(configuracaoColunasTabela, "Id", "Nome", "Email", "Telefone", "Empresa", "Cargo");

            Console.WriteLine("---------------------------------------------------------------------------------------------------------");

            Console.ResetColor();
        }
    }
}
