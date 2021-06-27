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
        public void DeveAdicionarCompromissoSemContato()
        {
            Compromisso compromisso = new Compromisso(0,"Assunto 1", "Localizacao 1", 0, DateTime.Today, DateTime.Today.AddDays(2), "");
            controladorCompromisso.Inserir(compromisso);

            List<Compromisso> listaQtdCompromossoBanco = controladorCompromisso.SelecionarTodosOsRegistrosDoBanco();

            Assert.AreEqual(1, listaQtdCompromossoBanco.Count);
        }

        [TestMethod]
        public void DeveAdicionarCompromissoComContato()
        {
            ControladorContato controladorContato = new ControladorContato();
            Contato contato = new Contato("Gustavo", "gustavomariano@ndd.tech", "3251-8000", "ndd", "dev");
            controladorContato.Inserir(contato);

            List<Contato> listaQtdContatoBanco = controladorContato.SelecionarTodosOsRegistrosDoBanco();

            Assert.AreEqual(1, listaQtdContatoBanco.Count);

            Compromisso compromisso = new Compromisso(0, "Assunto 1", "Localizacao 1", 1, DateTime.Today, DateTime.Today.AddDays(2), "");
            controladorCompromisso.Inserir(compromisso);

            List<Compromisso> listaQtdCompromissoBanco = controladorCompromisso.SelecionarTodosOsRegistrosDoBanco();

            Assert.AreEqual(1, listaQtdCompromissoBanco.Count);
        }

        [TestMethod]
        public void DeveExcluirUmCompromisso()
        {
            Compromisso compromisso = new Compromisso(0, "Assunto 1", "Localizacao 1", 0, DateTime.Today, DateTime.Today.AddDays(2), "");
            
            controladorCompromisso.Inserir(compromisso);

            List<Compromisso> listaQtdCompromissoInseridoBanco = controladorCompromisso.SelecionarTodosOsRegistrosDoBanco();

            Assert.AreEqual(1, listaQtdCompromissoInseridoBanco.Count);

            controladorCompromisso.Excluir(1);

            List<Compromisso> listaQtdCompromissoDeletadoBanco = controladorCompromisso.SelecionarTodosOsRegistrosDoBanco();

            Assert.IsTrue(listaQtdCompromissoDeletadoBanco.Count < listaQtdCompromissoInseridoBanco.Count);
        }
        #endregion
    }
}
