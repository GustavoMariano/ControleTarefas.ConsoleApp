using Microsoft.VisualStudio.TestTools.UnitTesting;
using ControleTarefas.ConsoleApp.Controlador;
using ControleTarefas.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using ControleTarefas.ConsoleApp.Infra;

namespace ControleTarefas.Tests
{
    [TestClass]
    public class ControleTarefasTests
    {
        Controlador<Tarefa> controleTarefas;
        Db db; 

        public ControleTarefasTests()
        {
            controleTarefas = new Controlador<Tarefa>();

            db = new Db();
            db.ResetaDadosEIdDB();            
        }

        [TestMethod]
        public void DeveAdicionarTarefa()
        {
            Tarefa tarefa = new Tarefa("DeveAdicionarTarefa", 1);
            controleTarefas.InserirTarefa(tarefa);

            List<Tarefa> listaQtdTarefasBanco = controleTarefas.VisualizarTodasTarefas();

            Assert.AreEqual(1, listaQtdTarefasBanco.Count);
        }

        [TestMethod]
        public void DeveSelecionarTodasAsTarefas()
        {
            Tarefa tarefa1 = new Tarefa("DeveSelecionarTodasAsTarefa1", 1);
            controleTarefas.InserirTarefa(tarefa1);
            Tarefa tarefa2 = new Tarefa("DeveSelecionarTodasAsTarefa2", 2);
            controleTarefas.InserirTarefa(tarefa2);
            Tarefa tarefa3 = new Tarefa("DeveSelecionarTodasAsTarefa3", 3);
            controleTarefas.InserirTarefa(tarefa3);

            List<Tarefa> list = controleTarefas.VisualizarTodasTarefas();
            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void DeveSelecionarPorIdUmaTarefa()
        {
            Tarefa tarefa1 = new Tarefa("DeveSelecionarPorIdUmaTarefa", 1);
            controleTarefas.InserirTarefa(tarefa1);

            Tarefa tarefaSelecionada = controleTarefas.SelecionarPorId(1);

            Assert.AreEqual(1, tarefaSelecionada.Id);
        }

        [TestMethod]
        public void DeveEditarUmaTarefa()
        {
            Tarefa inserirTarefaInicial = new Tarefa("Titulo inicial", 3);
            controleTarefas.InserirTarefa(inserirTarefaInicial);
            Tarefa tarefaInicialDoBanco = controleTarefas.SelecionarPorId(1);

            Assert.AreEqual("Titulo inicial", tarefaInicialDoBanco.Titulo);

            DateTime dataConclusao = new DateTime(2021, 12, 31);
            Tarefa tarefaEditada = new Tarefa(1, "Titulo Editado", 1, tarefaInicialDoBanco.DataCriacao, dataConclusao, 100);
            controleTarefas.EditarTarefa(tarefaEditada);
            Tarefa tarefaEditadaDoBanco = controleTarefas.SelecionarPorId(1);

            Assert.IsTrue(tarefaEditadaDoBanco.Titulo != tarefaInicialDoBanco.Titulo);
        }

        [TestMethod]
        public void DeveDeletarUmaTarefa()
        {
            Tarefa tarefa1 = new Tarefa("DeveSelecionarTodasAsTarefa1", 1);
            controleTarefas.InserirTarefa(tarefa1);
            Tarefa tarefa2 = new Tarefa("DeveSelecionarTodasAsTarefa2", 2);
            controleTarefas.InserirTarefa(tarefa2);
            Tarefa tarefa3 = new Tarefa("DeveSelecionarTodasAsTarefa3", 3);
            controleTarefas.InserirTarefa(tarefa3);
            List<Tarefa> listaCom3Tarefas = controleTarefas.VisualizarTodasTarefas();

            controleTarefas.DeletarTarefa(2);
            List<Tarefa> listaAposDeletarUmaTarefa = controleTarefas.VisualizarTodasTarefas();

            Assert.IsTrue(listaAposDeletarUmaTarefa.Count < listaCom3Tarefas.Count);
        }
    }
}
