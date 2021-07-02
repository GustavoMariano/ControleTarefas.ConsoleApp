using Microsoft.VisualStudio.TestTools.UnitTesting;
using eAgenda.Dominio.TarefaModule;

namespace eAgenda.Tests
{
    [TestClass]
    public class TarefasTests
    {
        [TestMethod]
        public void DeveRetornarFalseTituloTarefa()
        {
            Tarefa tarefa = new Tarefa("", 1);

            Assert.AreEqual(false, tarefa.Validar());
        }

        [TestMethod]
        public void DeveRetornarFalsePrioridadeTarefaForaDoRange()
        {
            Tarefa tarefa = new Tarefa("Titulo", 0);

            Assert.AreEqual(false, tarefa.Validar());
        }

        [TestMethod]
        public void DeveRetornarTureTarefaCorreta()
        {
            Tarefa tarefa = new Tarefa("Titulo", 1);

            Assert.AreEqual(true, tarefa.Validar());
        }
    }
}
