using ControleTarefasEContatos.ConsoleApp.Controlador;
using ControleTarefasEContatos.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleTarefasEContatos.ConsoleApp.Tela
{
    public class TelaContato : TelaBase
    {
        Controlador<Contato> controladorContato;

        public TelaContato(string titulo) : base(titulo)
        {
            controladorContato = new ControladorContato();
        }
    }
}
