using eAgenda.Dominio.Shared;

namespace eAgenda.Dominio.ContatoModule
{
    public class Contato : EntidadeBase
    {
        private string nome = "";
        private string email = "";
        private string telefone = "";
        private string empresa = "";
        private string cargo = "";

        public string Nome { get => nome;}
        public string Email { get => email; }
        public string Telefone { get => telefone; }
        public string Empresa { get => empresa; }
        public string Cargo { get => cargo; }
        public Contato(int id, string nome, string email, string telefone, string empresa, string cargo)
        {
            this.id = id;
            this.nome = nome;
            this.email = email;
            this.telefone = telefone;
            this.empresa = empresa;
            this.cargo = cargo;
        }
        public Contato(string nome, string email, string telefone, string empresa, string cargo)
        {
            this.nome = nome;
            this.email = email;
            this.telefone = telefone;
            this.empresa = empresa;
            this.cargo = cargo;
        }
        public override bool Validar()
        {
            if (!email.Contains("@") || !email.Contains(".com"))
                return false;
            if (telefone.Length < 8)
                return false;
            return true;
        }
    }
}
