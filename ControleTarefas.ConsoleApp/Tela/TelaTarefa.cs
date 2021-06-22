using ControleTarefasEContatos.ConsoleApp.Controlador;
using ControleTarefasEContatos.ConsoleApp.Dominio;
using System.Collections.Generic;
using System;

namespace ControleTarefasEContatos.ConsoleApp.Tela
{
    public class TelaTarefa : TelaBase
    {
        private Controlador<Tarefa> controladorTarefa;
        public TelaTarefa(string titulo) : base(titulo)
        {
            this.controladorTarefa = new ControladorTarefa();
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
            Console.Clear();

            string configuracaColunasTabela = "{0,-05} | {1,-15} | {2,-10} | {3,-15} | {4,-20} | {5,-20}";

            MontarCabecalhoTabela(configuracaColunasTabela);

            List<Tarefa> todasTarefas = controladorTarefa.SelecionarTodosOsRegistrosDoBanco();

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

            Console.Write("Digite o ID da tarefa que deseja editar: ");
            int idSelecionado;
            Int32.TryParse(Console.ReadLine(), out idSelecionado);

            Tarefa tarefaSelecionada = controladorTarefa.SelecionarRegistroPorId(idSelecionado);

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

            Tarefa tarefa = controladorTarefa.SelecionarRegistroPorId(idSelecionado);

            if (tarefa.Titulo == null)
            {
                Console.WriteLine("Id não encontrado, tente novamente!!");
                Console.ReadLine();
            }
            else
            {
                controladorTarefa.Excluir(idSelecionado);
                Console.WriteLine("Registro excluído com sucesso");
                Console.ReadLine();
            }
        }


        private string GravarTarefa(int id = 0, int porcentagemConluido = 0)
        {
            Tarefa tarefa;
            List<Tarefa> qtdDeRegistrosAntesDeAdicionarNovaTarefa = controladorTarefa.SelecionarTodosOsRegistrosDoBanco();
            List<Tarefa> qtdDeRegistrosDepoisDeAdicionarNovaTarefa = controladorTarefa.SelecionarTodosOsRegistrosDoBanco();

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
                    dataConclusao = DateTime.Now;
                    porcentagemConluido = 100;
                    tarefa = new Tarefa(id, titulo, prioridade, dataConclusao, dataConclusao, porcentagemConluido);
                    controladorTarefa.Editar(tarefa, id);
                    string confimacaoEdicao = VerificaSeFoiAdicionado(qtdDeRegistrosAntesDeAdicionarNovaTarefa, qtdDeRegistrosDepoisDeAdicionarNovaTarefa);
                    return confimacaoEdicao;
                }
            }

            tarefa = new Tarefa(titulo, prioridade);

            controladorTarefa.Inserir(tarefa);
            return VerificaSeFoiAdicionado(qtdDeRegistrosAntesDeAdicionarNovaTarefa, qtdDeRegistrosDepoisDeAdicionarNovaTarefa);
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

