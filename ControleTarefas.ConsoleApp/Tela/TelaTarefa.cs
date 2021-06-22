using ControleTarefasEContatos.ConsoleApp.Controlador;
using ControleTarefasEContatos.ConsoleApp.Dominio;
using System.Collections.Generic;
using System;

namespace ControleTarefasEContatos.ConsoleApp.Tela
{
    public class TelaTarefa : TelaBase
    {
        private Controlador<Tarefa> controlador;
        public TelaTarefa(string titulo) : base(titulo)
        {
            this.controlador = new ControladorTarefa();
        }

        public override void InserirNovoRegistro()
        {
            Console.Clear();

            string resultadoValidacao = "a";

            while (resultadoValidacao != "Tarefa cadastrada com sucesso!!")
            {
                resultadoValidacao = (GravarTarefa());
                Console.WriteLine(resultadoValidacao);
                Console.ReadLine();
                Console.Clear();
            }
        }
        public override void VisualizarRegistros()
        {
            Console.WriteLine("Como deseja visualizar as tarefas?");
            Console.WriteLine("1 - Apenas Fechadas\n2 - Apenas Abertas\n3 - Todas");
            string opcao = Console.ReadLine();
            switch (opcao)
            {
                case "1": VisualizarTarefasFechadas(); break;
                case "2": VisualizarTarefasAbertas(); break;
                case "3": VisualizarTodasTarefas(); break;
                default: break;
            }
        }

        private void VisualizarTarefasAbertas()
        {
            string configuracaColunasTabela = "{0,-05} | {1,-15} | {2,-10} | {3,-15} | {4,-20}";

            MontarCabecalhoTabela(configuracaColunasTabela);

            List<Tarefa> todasTarefas = controlador.SelecionarTodosOsRegistrosDoBanco();
            List<Tarefa> tarefasAbertas = new List<Tarefa>();

            foreach (var item in todasTarefas)
            {
                if (item.DataConclusao == DateTime.MinValue)
                    tarefasAbertas.Add(item);
            }

            if (todasTarefas.Count < 1)
            {
                Console.WriteLine("Nenhuma tarefa aberta no momento!!");
                Console.ReadLine();
                return;
            }

            foreach (var tarefa in todasTarefas)
            {
                Console.WriteLine(configuracaColunasTabela, tarefa.id, tarefa.Titulo, tarefa.Prioridade, tarefa.PercentualConcluido, tarefa.DataCriacao);
            }

            Console.ReadLine();
        }

        private void VisualizarTarefasFechadas()
        {
            string configuracaColunasTabela = "{0,-05} | {1,-15} | {2,-10} | {3,-15} | {4,-20} | {5,-20}";

            MontarCabecalhoTabela(configuracaColunasTabela);

            List<Tarefa> todasTarefas = controlador.SelecionarTodosOsRegistrosDoBanco();
            List<Tarefa> tarefaFechadas = new List<Tarefa>();

            foreach (var item in todasTarefas)
            {
                if (item.DataConclusao > DateTime.MinValue)
                    tarefaFechadas.Add(item);
            }

            if (tarefaFechadas.Count < 1)
            {
                Console.WriteLine("Nenhuma tarefa fechada no momento!!");
                Console.ReadLine();
                return;
            }

            foreach (var tarefa in tarefaFechadas)
            {
                Console.WriteLine(configuracaColunasTabela, tarefa.id, tarefa.Titulo, tarefa.Prioridade, tarefa.PercentualConcluido, tarefa.DataCriacao, tarefa.DataConclusao);
            }

            Console.ReadLine();
        }

        private void VisualizarTodasTarefas()
        {
            string configuracaColunasTabela = "{0,-05} | {1,-15} | {2,-10} | {3,-15} | {4,-20} | {5,-20}";

            MontarCabecalhoTabela(configuracaColunasTabela);

            List<Tarefa> todasTarefas = controlador.SelecionarTodosOsRegistrosDoBanco();

            if (todasTarefas.Count < 1)
            {
                Console.WriteLine("Nenhuma tarefa criada até o momento!!");
                return;
            }

            foreach (var tarefa in todasTarefas)
            {
                Console.WriteLine(configuracaColunasTabela, tarefa.id, tarefa.Titulo, tarefa.Prioridade, tarefa.PercentualConcluido, tarefa.DataCriacao, tarefa.DataConclusao);
            }

            Console.ReadLine();
        }

        public override void EditarRegistro()
        {
            Console.Clear();

            VisualizarRegistros();

            Console.WriteLine();

            List<Tarefa> todasTarefas = controlador.SelecionarTodosOsRegistrosDoBanco();
            if (todasTarefas.Count == 0)
            {
                Console.ReadLine();
                return;
            }

            Console.Write("Digite o ID da tarefa que deseja editar: ");
            int idSelecionado;
            Int32.TryParse(Console.ReadLine(), out idSelecionado);

            Tarefa tarefaSelecionada = controlador.SelecionarRegistroPorId(idSelecionado);

            if (tarefaSelecionada == null)
            {
                Console.WriteLine("Id não encontrado, tente novamente!!");
                Console.ReadLine();
            }
            else
            {
                string resultadoValidacao = (GravarTarefa(idSelecionado, tarefaSelecionada.PercentualConcluido));

                if (resultadoValidacao == "Tarefa cadastrada com sucesso!!")
                {
                    Console.WriteLine("Tarefa editada com sucesso");
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

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o ID da tarefa que deseja excluir: ");
            int idSelecionado;
            Int32.TryParse(Console.ReadLine(), out idSelecionado);

            Tarefa tarefa = controlador.SelecionarRegistroPorId(idSelecionado);

            if (tarefa.Titulo == null)
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

        private string GravarTarefa(int id = 0, int porcentagemConluido = 0)
        {
            Tarefa tarefa;
            List<Tarefa> qtdDeRegistrosAntesDeAdicionarNovaTarefa = controlador.SelecionarTodosOsRegistrosDoBanco();
            List<Tarefa> qtdDeRegistrosDepoisDeAdicionarNovaTarefa;

            Console.WriteLine("Digite o titulo da tarefa: ");
            string titulo = Console.ReadLine();
            Console.WriteLine("Prioridade: 1 - BAIXA, 2 - MÉDIA, 3 - ALTA");
            int prioridade = Convert.ToInt32(Console.ReadLine());

            DateTime dataConclusao = DateTime.MinValue;
            if (id != 0)
            {
                Console.WriteLine("A tarefa está finalizada? S - SIM, N - NÃO");
                string tarefaFinalizada = Console.ReadLine().ToUpper();

                if (tarefaFinalizada != "S" && tarefaFinalizada != "N")
                    return "Campo inválido, tente novamente!!";
                else if (tarefaFinalizada == "S")
                {
                    qtdDeRegistrosDepoisDeAdicionarNovaTarefa = controlador.SelecionarTodosOsRegistrosDoBanco();
                    return ConcluirTarefa(id, out porcentagemConluido, out tarefa, qtdDeRegistrosAntesDeAdicionarNovaTarefa, qtdDeRegistrosDepoisDeAdicionarNovaTarefa, titulo, prioridade, out dataConclusao);
                }
                else
                {
                    string opcao = "";
                    do
                    {
                        Console.WriteLine("Digite a porcentagem de 0 a 100");
                        int porcentagem;
                        Int32.TryParse(Console.ReadLine(), out porcentagem);
                        if (porcentagem >= 100)
                        {
                            Console.WriteLine("A porcentagem digitada é considerada como concluida, deseja concluir a tarefa ou tentar novamente?");
                            Console.WriteLine("1 - Concluir, 2 - Tentar novamente");
                            opcao = Console.ReadLine();
                            Console.Clear();
                            if (opcao == "1")
                            {
                                qtdDeRegistrosDepoisDeAdicionarNovaTarefa = controlador.SelecionarTodosOsRegistrosDoBanco();
                                return ConcluirTarefa(id, out porcentagemConluido, out tarefa, qtdDeRegistrosAntesDeAdicionarNovaTarefa, qtdDeRegistrosDepoisDeAdicionarNovaTarefa, titulo, prioridade, out dataConclusao);
                            }
                        }
                        else
                            opcao = "1";
                    } while (opcao != "1");
                }
            }

            tarefa = new Tarefa(titulo, prioridade);

            controlador.Inserir(tarefa);
            qtdDeRegistrosDepoisDeAdicionarNovaTarefa = controlador.SelecionarTodosOsRegistrosDoBanco();
            return VerificaSeFoiAdicionado(qtdDeRegistrosAntesDeAdicionarNovaTarefa, qtdDeRegistrosDepoisDeAdicionarNovaTarefa);
        }

        private string ConcluirTarefa(int id, out int porcentagemConluido, out Tarefa tarefa, List<Tarefa> qtdDeRegistrosAntesDeAdicionarNovaTarefa, List<Tarefa> qtdDeRegistrosDepoisDeAdicionarNovaTarefa, string titulo, int prioridade, out DateTime dataConclusao)
        {
            dataConclusao = DateTime.Now;
            porcentagemConluido = 100;
            tarefa = new Tarefa(id, titulo, prioridade, dataConclusao, dataConclusao, porcentagemConluido);
            controlador.Editar(tarefa, id);
            string confimacaoEdicao = VerificaSeFoiAdicionado(qtdDeRegistrosAntesDeAdicionarNovaTarefa, qtdDeRegistrosDepoisDeAdicionarNovaTarefa);
            return confimacaoEdicao;
        }

        private static string VerificaSeFoiAdicionado(List<Tarefa> qtdDeRegistrosAntesDeAdicionarNovaTarefa, List<Tarefa> qtdDeRegistrosDepoisDeAdicionarNovaTarefa)
        {
            if (qtdDeRegistrosDepoisDeAdicionarNovaTarefa.Count > qtdDeRegistrosAntesDeAdicionarNovaTarefa.Count)
                return "Tarefa cadastrada com sucesso!!";
            else
                return "Ocorreu um erro ao tentar cadastrar a tarefa, tente novamente!!";
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
