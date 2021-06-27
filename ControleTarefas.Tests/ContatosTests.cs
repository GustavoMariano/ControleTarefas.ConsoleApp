using Microsoft.VisualStudio.TestTools.UnitTesting;
using ControleTarefasEContatos.ConsoleApp.Controlador;
using ControleTarefasEContatos.ConsoleApp.Dominio;
using System.Collections.Generic;
using ControleTarefasEContatos.ConsoleApp.Infra.Comum;
using System;

namespace ControleTarefasEContatos.Tests
{
    [TestClass]
    public class ContatosTests
    {
        Db db;
        ControladorContato controleContato = new ControladorContato();

        public ContatosTests()
        {
            controleContato = new ControladorContato();

            db = new Db();
            db.ResetaDadosEIdDB();
        }

        #region Testes CRUD
        [TestMethod]
        public void DeveAdicionarContato()
        {
            Contato contato = new Contato("nome", "email@dominio.com", "3251-8000", "ndd", "Dev");
            controleContato.Inserir(contato);

            List<Contato> listaQtdTarefasBanco = controleContato.SelecionarTodosOsRegistrosDoBanco();

            Assert.AreEqual(1, listaQtdTarefasBanco.Count);
        }

        [TestMethod]
        public void DeveSelecionarTodosOsContatos()
        {
            Contato contato1 = new Contato("", "email1@email.com", "1111-1111", "", "");
            controleContato.Inserir(contato1);
            Contato contato2 = new Contato("", "email2@email.com", "2222-2222", "", "");
            controleContato.Inserir(contato2);
            Contato contato3 = new Contato("", "email3@email.com", "3333-3333", "", "");
            controleContato.Inserir(contato3);

            List<Contato> list = controleContato.SelecionarTodosOsRegistrosDoBanco();
            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void DeveSelecionarPorIdUmContato()
        {
            Contato contato = new Contato("Yudi da massa", "yudi@bomdiaecia.com", "4002-8922", "sbt", "apresentador");
            controleContato.Inserir(contato);

            Contato contatoSelecionada = controleContato.SelecionarRegistroPorId(1);

            Assert.AreEqual("Yudi da massa", contatoSelecionada.Nome);
        }

        [TestMethod]
        public void DeveSelecionarOsContatosPorCargo()
        {
            Contato contato1 = new Contato("Gustavo", "gustavo@ndd.com", " 3251-8000", "ndd", "dev");
            controleContato.Inserir(contato1);
            Contato contato2 = new Contato("Joao", "joao@ndd.com", " 3251-8000", "ndd", "dev");
            controleContato.Inserir(contato2);
            Contato contato3 = new Contato("Andrey", "andrey@ndd.com", " 3251-8000", "ndd", "dev");
            controleContato.Inserir(contato3);
            Contato contato4 = new Contato("Valmir", "valmir@ndd.com", " 3251-8000", "ndd", "presidente");
            controleContato.Inserir(contato4);

            List<Contato> listaDevs= controleContato.ListarPorCargo("dev");

            Assert.AreEqual(3, listaDevs.Count);

            List<Contato> listaPresidente = controleContato.ListarPorCargo("presidente");

            Assert.AreEqual(1, listaPresidente.Count);
        }

        [TestMethod]
        public void DeveEditarUmaTarefa()
        {
            Contato inserirContatoInicial = new Contato("Nome Inicial", "email@.com", "0800", "empresa", "cargo");
            controleContato.Inserir(inserirContatoInicial);
            Contato tarefaInicialDoBanco = controleContato.SelecionarRegistroPorId(1);

            Assert.AreEqual("Nome Inicial", tarefaInicialDoBanco.Nome);

            DateTime dataConclusao = new DateTime(2021, 12, 31);
            Contato tarefaEditada = new Contato(1, "Nome Editado", "gustavo@email.com", "123456789", "NDD", "DEV");
            controleContato.Editar(tarefaEditada, 1);
            Contato tarefaEditadaDoBanco = controleContato.SelecionarRegistroPorId(1);

            Assert.IsTrue(tarefaEditadaDoBanco.Nome != tarefaInicialDoBanco.Nome);
        }

        [TestMethod]
        public void DeveDeletarUmaTarefa()
        {
            Contato contato1 = new Contato("", "email1@contato1.com", "11 11111-1111", "", "");
            controleContato.Inserir(contato1);
            Contato contato2 = new Contato("", "email2@contato2.com", "22 22222-2222", "", "");
            controleContato.Inserir(contato2);
            Contato contato3 = new Contato("", "email3@contato3.com", "33 33333-3333", "", "");
            controleContato.Inserir(contato3);
            List<Contato> listaCom3Tarefas = controleContato.SelecionarTodosOsRegistrosDoBanco();

            controleContato.Excluir(2);
            List<Contato> listaAposDeletarUmaTarefa = controleContato.SelecionarTodosOsRegistrosDoBanco();

            Assert.IsTrue(listaAposDeletarUmaTarefa.Count < listaCom3Tarefas.Count);
        }
        #endregion

        #region Testes Validação
        [TestMethod]
        public void DeveRetornarFalseTudoVazio()
        {
            Contato contato = new Contato("", "", "", "", "");

            Assert.AreEqual(false, contato.Validar());
        }

        [TestMethod]
        public void DeveRetornarFalseEmailVazio()
        {
            Contato contato = new Contato("Nome", "", "3251-8000", "Empresa", "Cargo");

            Assert.AreEqual(false, contato.Validar());
        }

        [TestMethod]
        public void DeveRetornarFalseTelefoneVazio()
        {
            Contato contato = new Contato("Nome", "email@.com", "", "Empresa", "Cargo");

            Assert.AreEqual(false, contato.Validar());
        }
        [TestMethod]
        public void DeveRetornarFalseEmailNaoContemPontoCom()
        {
            Contato contato = new Contato("Nome", "email@", "3251-8000", "Empresa", "Cargo");

            Assert.AreEqual(false, contato.Validar());
        }

        [TestMethod]
        public void DeveRetornarFalseEmailNaoContemArroba()
        {
            Contato contato = new Contato("Nome", "email.com", "3251-8000", "Empresa", "Cargo");

            Assert.AreEqual(false, contato.Validar());
        }

        [TestMethod]
        public void DeveRetornarFalseEmailNaoContemArrobaEPontoCom()
        {
            Contato contato = new Contato("Nome", "email", "3251-8000", "Empresa", "Cargo");

            Assert.AreEqual(false, contato.Validar());
        }

        [TestMethod]
        public void DeveRetornarTrueContatoValido()
        {
            Contato contato = new Contato("Nome", "email@email.com", "3251-8000", "Empresa", "Cargo");

            Assert.AreEqual(true, contato.Validar());
        }
        #endregion
    }
}
