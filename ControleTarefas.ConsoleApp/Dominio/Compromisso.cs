using System;

namespace ControleTarefasEContatos.ConsoleApp.Dominio
{
    public class Compromisso : EntidadeBase
    {
        private string assunto;
        private string localizacao;
        private int idContato = 0;
        private DateTime dataInicioCompromisso = DateTime.MinValue;
        private DateTime dataFinalCompromisso = DateTime.MinValue;
        private string linkReuniao;

        public string Assunto { get => assunto; }
        public string Localizacao { get => localizacao; }
        public int IdContato { get => idContato; }
        public DateTime DataInicioCompromisso { get => dataInicioCompromisso; }
        public DateTime DataFinalCompromisso { get => dataFinalCompromisso; }
        public string LinkReuniao { get => linkReuniao; }

        public Compromisso(string assunto, string localizacao, DateTime dataInicioCompromisso, DateTime dataFinalCompromisso)
        {
            this.assunto = assunto;
            this.localizacao = localizacao;
            this.dataInicioCompromisso = dataInicioCompromisso;
            this.dataFinalCompromisso = dataFinalCompromisso;
        }

        public Compromisso(int id, string assunto, string localizacao, int idContato, DateTime dataInicioCompromisso, DateTime dataFinalCompromisso, string linkReuniao)
        {
            this.id = id;
            this.assunto = assunto;
            this.localizacao = localizacao;
            this.idContato = idContato;
            this.dataInicioCompromisso = dataInicioCompromisso;
            this.dataFinalCompromisso = dataFinalCompromisso;
            this.linkReuniao = linkReuniao;
        }

        public override bool Validar()
        {
            if (assunto.Length == 0)
                return false;
            if (dataInicioCompromisso == DateTime.MinValue)
                return false;
            if (dataFinalCompromisso == DateTime.MinValue)
                return false;
            if (localizacao.Length == 0 && linkReuniao.Length == 0)
                return false;
            return true;
        }
    }
}
