using ControleTarefasEContatos.ConsoleApp.Controlador;
using ControleTarefasEContatos.ConsoleApp.Dominio;
using System.Collections.Generic;
using System;

namespace ControleTarefasEContatos.ConsoleApp.Tela
{
    class TelaCompromisso : TelaBase
    {
        private Controlador<Compromisso> controlador;
        public TelaCompromisso(string tit) : base(tit)
        {
            this.controlador = new ControladorCompromisso();
        }
        public override void InserirNovoRegistro()
        {
            Console.Clear();

            string resultadoValidacao = "a";

            while (resultadoValidacao != "Sucesso!!")
            {
                resultadoValidacao = (GravarCompromisso());
                Console.WriteLine(resultadoValidacao);
                Console.ReadLine();
                Console.Clear();
            }
        }
        public override void VisualizarRegistros()
        {
            Console.WriteLine("1 - Todos os compromissos\n2 - Filtrar por periodo\n3 - Compromissos passados\n4 - Compromissos futuros");
            string opcao = Console.ReadLine();
            switch (opcao)
            {
                case "1": VisualizarTodosOsCompromissos(); break;
                case "2": VisualizarCompromissoPorPeriodo(); break;
                case "3": VisualizarCompromissosPassados(); break;
                case "4": VisualizarCompromissosFuturos(); break;
                case "5": VisualizarCompromissosDeHoje(); break;
                default: break;
            }
        }
        private void VisualizarCompromissosDeHoje()
        {
            Console.Clear();
            string configuracaColunasTabela = "{0,-05} | {1,-15} | {2,-10} | {3,-15} | {4,-20} | {5,-20} | {6,-25}";
            MontarCabecalhoTabela(configuracaColunasTabela);

            List<Compromisso> todosCompromissos = controlador.SelecionarTodosOsRegistrosDoBanco();
            VerificaCompromomissoBanco(todosCompromissos);

            List<Compromisso> compromissosDeHoje = new List<Compromisso>();
            foreach (var item in todosCompromissos)
            {
                if (item.DataInicioCompromisso > DateTime.Today && item.DataInicioCompromisso > DateTime.Today.AddDays(1))
                    compromissosDeHoje.Add(item);
            }

            if (todosCompromissos.Count < 1)
            {
                Console.WriteLine("Nenhuma tarefa para hoje!!");
                Console.ReadLine();
                return;
            }

            foreach (var compromisso in todosCompromissos)
            {
                Console.WriteLine(configuracaColunasTabela, compromisso.id, compromisso.Assunto, compromisso.Localizacao, compromisso.LinkReuniao, compromisso.DataInicioCompromisso, compromisso.DataFinalCompromisso, compromisso.Nome);
            }

            Console.ReadLine();
        }
        private void VisualizarCompromissosFuturos()
        {
            Console.Clear();
            string configuracaColunasTabela = "{0,-05} | {1,-15} | {2,-10} | {3,-15} | {4,-20} | {5,-20} | {6,-25}";

            MontarCabecalhoTabela(configuracaColunasTabela);

            List<Compromisso> todosCompromissos = controlador.SelecionarTodosOsRegistrosDoBanco();
            VerificaCompromomissoBanco(todosCompromissos);

            List<Compromisso> compromissosFuturos = new List<Compromisso>();

            foreach (var item in todosCompromissos)
            {
                if (item.DataInicioCompromisso > DateTime.Now)
                    compromissosFuturos.Add(item);
            }

            if (todosCompromissos.Count < 1)
            {
                Console.WriteLine("Nenhuma tarefa agendada para o futuro!!");
                Console.ReadLine();
                return;
            }

            foreach (var compromisso in todosCompromissos)
            {
                Console.WriteLine(configuracaColunasTabela, compromisso.id, compromisso.Assunto, compromisso.Localizacao, compromisso.LinkReuniao, compromisso.DataInicioCompromisso, compromisso.DataFinalCompromisso, compromisso.Nome);
            }

            Console.ReadLine();
        }
        private void VisualizarCompromissosPassados()
        {
            Console.Clear();
            string configuracaColunasTabela = "{0,-05} | {1,-15} | {2,-10} | {3,-15} | {4,-20} | {5,-20} | {6,-25}";

            MontarCabecalhoTabela(configuracaColunasTabela);

            List<Compromisso> todosCompromissos = controlador.SelecionarTodosOsRegistrosDoBanco();
            VerificaCompromomissoBanco(todosCompromissos);

            List<Compromisso> compromissosPassados = new List<Compromisso>();

            foreach (var item in todosCompromissos)
            {
                if (item.DataInicioCompromisso < DateTime.Now)
                    compromissosPassados.Add(item);
            }

            if (todosCompromissos.Count < 1)
            {
                Console.WriteLine("Nenhuma tarefa foi realizada!!");
                Console.ReadLine();
                return;
            }

            foreach (var compromisso in todosCompromissos)
            {
                Console.WriteLine(configuracaColunasTabela, compromisso.id, compromisso.Assunto, compromisso.Localizacao, compromisso.LinkReuniao, compromisso.DataInicioCompromisso, compromisso.DataFinalCompromisso, compromisso.Nome);
            }

            Console.ReadLine();
        }
        private void VisualizarCompromissoPorPeriodo()
        {
            Console.WriteLine("Digite a data incial do periodo que deseja verificar Ex. yyyy/MM/dd");
            DateTime dataInicialFiltro = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Digite a data final do periodo que deseja verificar Ex. yyyy/MM/dd");
            DateTime dataFinalFiltro = Convert.ToDateTime(Console.ReadLine());
            Console.Clear();
            string configuracaColunasTabela = "{0,-05} | {1,-15} | {2,-10} | {3,-15} | {4,-20} | {5,-20} | {6,-25}";

            MontarCabecalhoTabela(configuracaColunasTabela);

            List<Compromisso> todosCompromissos = controlador.SelecionarTodosOsRegistrosDoBanco();
            List<Compromisso> compromissosFiltrados = controlador.SelecionarTodosOsRegistrosDoBanco();

            foreach (var item in todosCompromissos)
            {
                if (item.DataInicioCompromisso >= dataInicialFiltro && item.DataFinalCompromisso <= dataFinalFiltro)
                    compromissosFiltrados.Add(item);
            }
            VerificaCompromomissoBanco(todosCompromissos);

            foreach (var tarefa in compromissosFiltrados)
            {
                Console.WriteLine(configuracaColunasTabela, tarefa.id, tarefa.Assunto, tarefa.Localizacao, tarefa.LinkReuniao, tarefa.DataInicioCompromisso, tarefa.DataFinalCompromisso, tarefa.IdContato);
            }

            Console.ReadLine();
        }
        private void VisualizarTodosOsCompromissos()
        {
            Console.Clear();
            string configuracaColunasTabela = "{0,-05} | {1,-15} | {2,-10} | {3,-15} | {4,-20} | {5,-20} | {6,-25}";

            MontarCabecalhoTabela(configuracaColunasTabela);

            List<Compromisso> todosCompromissos = controlador.SelecionarTodosOsRegistrosDoBanco();
            VerificaCompromomissoBanco(todosCompromissos);

            foreach (var compromisso in todosCompromissos)
            {
                Console.WriteLine(configuracaColunasTabela, compromisso.id, compromisso.Assunto, compromisso.Localizacao, compromisso.LinkReuniao, compromisso.DataInicioCompromisso, compromisso.DataFinalCompromisso, compromisso.Nome);
            }

            Console.ReadLine();
        }
        public override void EditarRegistro()
        {
            Console.Clear();

            VisualizarTodosOsCompromissos();

            Console.WriteLine();

            List<Compromisso> todosCompromissos = controlador.SelecionarTodosOsRegistrosDoBanco();
            VerificaCompromomissoBanco(todosCompromissos);

            Console.Write("Digite o ID da contato que deseja editar: ");
            int idSelecionado;
            Int32.TryParse(Console.ReadLine(), out idSelecionado);

            Compromisso CompromissoSelecionado = controlador.SelecionarRegistroPorId(idSelecionado);

            if (CompromissoSelecionado == null)
            {
                Console.WriteLine("ID não encontrado, tente novamente!!");
                Console.ReadLine();
            }
            else
            {
                string resultadoValidacao = (GravarCompromisso(idSelecionado));

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

            VisualizarTodosOsCompromissos();

            Console.WriteLine();

            Console.Write("Digite o ID do compromisso que deseja excluir: ");
            int idSelecionado;
            Int32.TryParse(Console.ReadLine(), out idSelecionado);

            Compromisso compromisso = controlador.SelecionarRegistroPorId(idSelecionado);

            if (compromisso.Nome == null)
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
        private string GravarCompromisso(int idSelecionado = 0, int idContato = 0)
        {
            Compromisso compromisso;
            TelaContato telaContato = new TelaContato("");
            Console.Clear();
            Console.WriteLine("Selecione como deseja visualizar os contatos");
            telaContato.VisualizarRegistros();
            Console.WriteLine("\nDigite o ID para selecionar o contato do compromisso\nPARA NÃO SELECIONAR CONTATO, DIGITE 0 (ZERO)");
            idContato = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Digite o assunto do compromisso ");
            string assunto = Console.ReadLine();

            Console.WriteLine("O assunto será remoto ou presencial? 1 - Presencial, 2 - Remoto");
            string opcao = Console.ReadLine();

            string localizacao = "Não possui";
            string link = "Não possui";
            if (opcao == "1")
            {
                Console.WriteLine("Digite o localizacao do compromisso ");
                localizacao = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Insira o link do compromisso ");
                link = Console.ReadLine();
            }

            Console.WriteLine("Digite data e hora de inicio Ex. yyyy/MM/dd HH:mm:SS ");
            DateTime dataInicio = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Digite a hora de encerramento do compromisso Ex. yyyy/MM/dd HH:mm:SS ");
            DateTime dataFinal = Convert.ToDateTime(Console.ReadLine());

            compromisso = new Compromisso(idSelecionado, assunto, localizacao, idContato, dataInicio, dataFinal, link);
            if (idSelecionado != 0)
            {
                controlador.Editar(compromisso, idSelecionado);
                return "Sucesso!!";
            }
            else if (compromisso.Validar())
            {
                controlador.Inserir(compromisso);
                return "Sucesso!!";
            }
            else
                return "Contato inválido";
        }
        #region Métodos Privados
        private void VerificaCompromomissoBanco(List<Compromisso> todosCompromissos)
        {
            if (todosCompromissos.Count < 1)
            {
                Console.WriteLine("Nenhuma tarefa criada até o momento!!");
                Console.ReadLine();
                return;
            }
        }
        private static void MontarCabecalhoTabela(string configuracaoColunasTabela)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(configuracaoColunasTabela, "Id", "Assunto", "Localização", "Link", "Data de Inicio", "Data Final", "Contato");

            Console.WriteLine("---------------------------------------------------------------------------------------------------------");

            Console.ResetColor();
        }
        #endregion
    }
}
