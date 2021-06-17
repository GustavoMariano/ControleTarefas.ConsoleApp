using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleTarefas.ConsoleApp.Dominio
{
    public class Tarefa : EntidadeBase
    {
        private string titulo = "";
        private int prioridade = 0;
        private DateTime dataCriacao = new DateTime(0001,01,01);
        private DateTime dataConclusao = new DateTime(0001, 01, 01);
        private int percentualConcluido = 0;

        public Tarefa(string titulo, int prioridade)
        {
            this.titulo = titulo;
            this.prioridade = prioridade;
        }
        public Tarefa(int prioridade, int percentualConcluido)
        {
            this.prioridade = prioridade;
            this.percentualConcluido = percentualConcluido;
        }
    }
}
