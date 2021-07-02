using Microsoft.VisualStudio.TestTools.UnitTesting;
using eAgenda.Dominio;
using System;
using eAgenda.Dominio.CompromissoModule;

namespace eAgenda.Tests
{
    [TestClass]
    public class CompromissoTests
    {
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
        public void DeveRetornarTrueCompromissoCompleto()
        {
            Compromisso compromisso = new Compromisso(0, "Assunto", "Localizacao", 1, DateTime.Now, DateTime.Now.AddDays(2), "Link");

            Assert.AreEqual(true, compromisso.Validar());
        }
    }
}
