using Microsoft.VisualStudio.TestTools.UnitTesting;
using ControleTarefasEContatos.ConsoleApp.Controlador;
using ControleTarefasEContatos.ConsoleApp.Dominio;
using System.Collections.Generic;
using ControleTarefasEContatos.ConsoleApp.Infra.Comum;
using System;

namespace ControleTarefas.Tests
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


    }
}
