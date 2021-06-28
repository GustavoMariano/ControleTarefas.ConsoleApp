using Microsoft.VisualStudio.TestTools.UnitTesting;
using ControleTarefasEContatos.ConsoleApp.Controlador;
using ControleTarefasEContatos.ConsoleApp.Dominio;
using System.Collections.Generic;
using ControleTarefasEContatos.ConsoleApp.Infra.Comum;
using System;

namespace ControleTarefasEContatos.Tests
{
    [TestClass]
    public class CompromissoTests
    {
        Db db;
        ControladorCompromisso controladorCompromisso;

        public CompromissoTests()
        {
            controladorCompromisso = new ControladorCompromisso();

            db = new Db();
            db.ResetaDadosEIdDB();
        }

        #region Testes CRUD
        [TestMethod]
        public void DeveAdicionarCompromissoComContato()
        {
            ControladorContato controladorContato = new ControladorContato();
            Contato contato = new Contato("Gustavo", "gustavomariano@ndd.tech", "3251-8000", "ndd", "dev");
            controladorContato.Inserir(contato);

            List<Contato> listaQtdContatoBanco = controladorContato.SelecionarTodosOsRegistrosDoBanco();

            Assert.AreEqual(1, listaQtdContatoBanco.Count);

            Compromisso compromisso = new Compromisso(0, "Assunto 1", "Localizacao 1", 1, DateTime.Today, DateTime.Today.AddDays(2), "", "");
            controladorCompromisso.Inserir(compromisso);

            List<Compromisso> listaQtdCompromissoBanco = controladorCompromisso.SelecionarTodosOsRegistrosDoBanco();

            Assert.AreEqual(1, listaQtdCompromissoBanco.Count);
            Assert.AreEqual("Gustavo", listaQtdCompromissoBanco[0].Nome);
        }
        [TestMethod]
        public void DeveAdicionarCompromissoSemContato()
        {
            Compromisso compromisso = new Compromisso(0, "Assunto 1", "Localizacao 1", 0, DateTime.Today, DateTime.Today.AddDays(2), "", "");
            controladorCompromisso.Inserir(compromisso);

            List<Compromisso> listaQtdCompromissoBanco = controladorCompromisso.SelecionarTodosOsRegistrosDoBanco();

            Assert.AreEqual(1, listaQtdCompromissoBanco.Count);
        }
        [TestMethod]
        public void DeveEditarCompromisso()
        {
            ControladorContato controladorContato = new ControladorContato();
            Contato contato = new Contato("Gustavo", "gustavomariano@ndd.tech", "3251-8000", "ndd", "dev");
            controladorContato.Inserir(contato);

            Compromisso compromisso = new Compromisso(0,"Assunto 1", "Localizacao 1", 1, DateTime.Today, DateTime.Today.AddDays(2), "");
            controladorCompromisso.Inserir(compromisso);

            List<Compromisso> compromissoAntesDeEditar = controladorCompromisso.SelecionarTodosOsRegistrosDoBanco();

            compromisso = new Compromisso(0, "Assunto Editado", "", 1, DateTime.Now, DateTime.Now.AddHours(3), "Link");
            controladorCompromisso.Editar(compromisso, 1);

            List<Compromisso> compromissoDepoisDeEditar = controladorCompromisso.SelecionarTodosOsRegistrosDoBanco();

            Assert.IsTrue(compromissoAntesDeEditar[0].Assunto != compromissoDepoisDeEditar[0].Assunto);
        }
        [TestMethod]
        public void DeveExcluirUmCompromisso()
        {
            ControladorContato controladorContato = new ControladorContato();
            Contato contato = new Contato("Gustavo", "gustavomariano@ndd.tech", "3251-8000", "ndd", "dev");
            controladorContato.Inserir(contato);
            List<Contato> listaQtdContatoBanco = controladorContato.SelecionarTodosOsRegistrosDoBanco();

            Compromisso compromisso = new Compromisso(0, "Assunto 1", "Localizacao 1", 1, DateTime.Today, DateTime.Today.AddDays(2), "", "");            
            controladorCompromisso.Inserir(compromisso);
            List<Compromisso> listaQtdCompromissoInseridoBanco = controladorCompromisso.SelecionarTodosOsRegistrosDoBanco();

            Assert.AreEqual(1, listaQtdCompromissoInseridoBanco.Count);

            controladorCompromisso.Excluir(1);

            List<Compromisso> listaQtdCompromissoDeletadoBanco = controladorCompromisso.SelecionarTodosOsRegistrosDoBanco();

            Assert.IsTrue(listaQtdCompromissoDeletadoBanco.Count < listaQtdCompromissoInseridoBanco.Count);
        }
        #endregion

        #region Testes Validação
        [TestMethod]
        public void DeveRetornarFalseCompromissoNulo()
        {
            Compromisso compromisso = new Compromisso(0, "", "", 0, DateTime.MinValue, DateTime.MinValue, "", ""); 

            Assert.AreEqual(false, compromisso.Validar());
        }

        [TestMethod]
        public void DeveRetornarFalseLocalizacaoELinkNulos()
        {
            Compromisso compromisso = new Compromisso(0, "Assunto", "", 0, DateTime.Now, DateTime.Now, "");

            Assert.AreEqual(false, compromisso.Validar());
        }

        [TestMethod]
        public void DeveRetornarFalseDataInicialMinima()
        {
            Compromisso compromisso = new Compromisso(0, "Assunto", "Localizcao", 0, DateTime.MinValue, DateTime.Now, "Link");

            Assert.AreEqual(false, compromisso.Validar());
        }

        [TestMethod]
        public void DeveRetornarFalseDataFinalMinima()
        {
            Compromisso compromisso = new Compromisso(0, "Assunto", "Localizacao", 0, DateTime.Now, DateTime.MinValue, "Link");

            Assert.AreEqual(false, compromisso.Validar());
        }

        [TestMethod]
        public void DeveRetornarFalseDataJaUsada()
        {
            Compromisso compromissoBanco = new Compromisso(0, "Assunto", "Localizacao", 0, DateTime.Now.AddHours(-1), DateTime.Now.AddHours(1), "Link");
            controladorCompromisso.Inserir(compromissoBanco);

            Compromisso compromisso = new Compromisso(0, "Assunto", "Localizacao", 0, DateTime.Now, DateTime.Now.AddSeconds(10), "Link");

            Assert.AreEqual(false, compromisso.Validar());
        }

        [TestMethod]
        public void DeveRetornarTrueCompromissoCompleto()
        {
            Compromisso compromisso = new Compromisso(0, "Assunto", "Localizacao", 1, DateTime.Now, DateTime.Now.AddDays(2), "Link");

            Assert.AreEqual(true, compromisso.Validar());
        }
        #endregion
    }
}
