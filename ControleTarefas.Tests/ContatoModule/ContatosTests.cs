using Microsoft.VisualStudio.TestTools.UnitTesting;
using eAgenda.Dominio.ContatoModule;

namespace eAgenda.Tests
{
    [TestClass]
    public class ContatosTests
    {       

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
    }
}
