using System;

namespace ControleTarefas.ConsoleApp.Dominio
{
    public class Tarefa : EntidadeBase
    {
        private int id;
        private string titulo = "";
        private int prioridade = 0;
        private DateTime dataCriacao = DateTime.Now;
        private DateTime? dataConclusao;
        private int percentualConcluido = 0;

        public int Id { get => id; }
        public string Titulo{ get => titulo; }
        public int Prioridade { get => prioridade;}
        public DateTime DataCriacao { get => dataCriacao; }
        public DateTime? DataConclusao { get => dataConclusao; }
        public int PercentualConcluido { get => percentualConcluido; }

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
        //public Tarefa(string titulo, int prioridade, DateTime dataAbertura, DateTime dataConclusao, int percentualConclusao) : this(titulo, prioridade)
        //{
        //    this.dataCriacao = dataAbertura;
        //    this.dataConclusao = dataConclusao;
        //    this.percentualConcluido = percentualConclusao;
        //}
        public Tarefa(int id, string titulo, int prioridade, DateTime dataCriacao, DateTime dataConclusao, int percentualConcluido)
        {
            this.id = id;
            this.titulo = titulo;
            this.prioridade = prioridade;
            this.dataCriacao = dataCriacao;
            this.dataConclusao = dataConclusao;
            this.percentualConcluido = percentualConcluido;
        }
    }
}
