using eAgenda.Controladores.Infra.Comum;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using eAgenda.Controladores.TarefaModule;
using eAgenda.Dominio.TarefaModule;

namespace ControleTarefas.Tests.TarefaModule
{
    [TestClass]
    public class ControladorTarefasTests
    {
        Db db;
        ControladorTarefa controleTarefa;

        public ControladorTarefasTests()
        {
            controleTarefa = new ControladorTarefa();

            db = new Db();
            db.ResetaDadosEIdDB();
        }

        [TestMethod]
        public void DeveAdicionarTarefa()
        {
            Tarefa tarefa = new Tarefa("DeveAdicionarTarefa", 1);
            controleTarefa.Inserir(tarefa);

            List<Tarefa> listaQtdTarefasBanco = controleTarefa.SelecionarTodosOsRegistrosDoBanco();

            Assert.AreEqual(1, listaQtdTarefasBanco.Count);
        }

        [TestMethod]
        public void DeveSelecionarTodasAsTarefas()
        {
            Tarefa tarefa1 = new Tarefa("DeveSelecionarTodasAsTarefa1", 1);
            controleTarefa.Inserir(tarefa1);
            Tarefa tarefa2 = new Tarefa("DeveSelecionarTodasAsTarefa2", 2);
            controleTarefa.Inserir(tarefa2);
            Tarefa tarefa3 = new Tarefa("DeveSelecionarTodasAsTarefa3", 3);
            controleTarefa.Inserir(tarefa3);

            List<Tarefa> list = controleTarefa.SelecionarTodosOsRegistrosDoBanco();
            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void DeveSelecionarPorIdUmaTarefa()
        {
            Tarefa tarefa1 = new Tarefa("DeveSelecionarPorIdUmaTarefa", 1);
            controleTarefa.Inserir(tarefa1);

            Tarefa tarefaSelecionada = controleTarefa.SelecionarRegistroPorId(1);

            Assert.AreEqual(1, tarefaSelecionada.id);
        }

        [TestMethod]
        public void DeveSelecionarTarefasFinalizadasPorPrioridade()
        {
            DateTime encerramento = new DateTime(2021, 12, 31);
            Tarefa tarefa1 = new Tarefa("Media - Aberta", 2);
            controleTarefa.Inserir(tarefa1);
            Tarefa tarefa2 = new Tarefa(0, "Alta - Fechada", 3, encerramento, DateTime.Now, 100);
            controleTarefa.Inserir(tarefa2);
            Tarefa tarefa3 = new Tarefa("Media - Aberta", 2);
            controleTarefa.Inserir(tarefa3);
            Tarefa tarefa4 = new Tarefa(0, "Baixa - Fechada", 1, encerramento, DateTime.Now, 100);
            controleTarefa.Inserir(tarefa4);
            Tarefa tarefa5 = new Tarefa("Alta - Aberta", 3);
            controleTarefa.Inserir(tarefa5);

            List<Tarefa> tarefasTotalNoBanco = controleTarefa.SelecionarTodosOsRegistrosDoBanco();

            Assert.AreEqual(5, tarefasTotalNoBanco.Count);

            List<Tarefa> tarefasFechadasEmOrdemDePrioridade = controleTarefa.SelecionarTarefasFinalizadas();
            Assert.AreEqual(2, tarefasFechadasEmOrdemDePrioridade.Count);
        }

        [TestMethod]
        public void DeveSelecionarTarefasAbertasPorPrioridade()
        {
            DateTime encerramento = new DateTime(2021, 12, 31);
            Tarefa tarefa1 = new Tarefa("Media - Aberta", 2);
            controleTarefa.Inserir(tarefa1);
            Tarefa tarefa2 = new Tarefa(0, "Alta - Fechada", 3, encerramento, DateTime.Now, 100);
            controleTarefa.Inserir(tarefa2);
            Tarefa tarefa3 = new Tarefa("Media - Aberta", 2);
            controleTarefa.Inserir(tarefa3);
            Tarefa tarefa4 = new Tarefa(0, "Baixa - Fechada", 1, encerramento, DateTime.Now, 100);
            controleTarefa.Inserir(tarefa4);
            Tarefa tarefa5 = new Tarefa("Alta - Aberta", 3);
            controleTarefa.Inserir(tarefa5);

            List<Tarefa> tarefasTotalNoBanco = controleTarefa.SelecionarTodosOsRegistrosDoBanco();
            Assert.AreEqual(5, tarefasTotalNoBanco.Count);

            List<Tarefa> tarefasFechadasEmOrdemDePrioridade = controleTarefa.SelecionarTarefasAbertas();
            Assert.AreEqual(3, tarefasFechadasEmOrdemDePrioridade.Count);
        }

        [TestMethod]
        public void DeveEditarUmaTarefa()
        {
            Tarefa inserirTarefaInicial = new Tarefa("Titulo inicial", 3);
            controleTarefa.Inserir(inserirTarefaInicial);
            Tarefa tarefaInicialDoBanco = controleTarefa.SelecionarRegistroPorId(1);

            Assert.AreEqual("Titulo inicial", tarefaInicialDoBanco.Titulo);

            DateTime dataConclusao = new DateTime(2021, 12, 31);
            Tarefa tarefaEditada = new Tarefa(1, "Titulo Editado", 1, tarefaInicialDoBanco.DataCriacao, dataConclusao, 100);
            controleTarefa.Editar(tarefaEditada, 1);
            Tarefa tarefaEditadaDoBanco = controleTarefa.SelecionarRegistroPorId(1);

            Assert.IsTrue(tarefaEditadaDoBanco.Titulo != tarefaInicialDoBanco.Titulo);
        }

        [TestMethod]
        public void DeveDeletarUmaTarefa()
        {
            Tarefa tarefa1 = new Tarefa("DeveSelecionarTodasAsTarefa1", 1);
            controleTarefa.Inserir(tarefa1);
            Tarefa tarefa2 = new Tarefa("DeveSelecionarTodasAsTarefa2", 2);
            controleTarefa.Inserir(tarefa2);
            Tarefa tarefa3 = new Tarefa("DeveSelecionarTodasAsTarefa3", 3);
            controleTarefa.Inserir(tarefa3);
            List<Tarefa> listaCom3Tarefas = controleTarefa.SelecionarTodosOsRegistrosDoBanco();

            controleTarefa.Excluir(2);
            List<Tarefa> listaAposDeletarUmaTarefa = controleTarefa.SelecionarTodosOsRegistrosDoBanco();

            Assert.IsTrue(listaAposDeletarUmaTarefa.Count < listaCom3Tarefas.Count);
        }
    }
}
