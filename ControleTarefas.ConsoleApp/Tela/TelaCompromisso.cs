using ControleTarefasEContatos.ConsoleApp.Controlador;
using ControleTarefasEContatos.ConsoleApp.Dominio;
using System.Collections.Generic;
using System;

namespace ControleTarefasEContatos.ConsoleApp.Tela
{
    class TelaCompromisso : TelaBase
    {
        private Controlador<Compromisso> controlador;
        public TelaCompromisso(string tit) : base(tit)
        {
            this.controlador = new ControladorCompromisso();
        }

        public override void InserirNovoRegistro()
        {
            Console.Clear();

            string resultadoValidacao = "a";

            while (resultadoValidacao != "Sucesso!!")
            {
                resultadoValidacao = (GravarCompromisso());
                Console.WriteLine(resultadoValidacao);
                Console.ReadLine();
                Console.Clear();
            }
        }
        private string GravarCompromisso(int idSelecionado = 0, int idContato = 0)
        {
            Compromisso compromisso;
            //telaCompromisso.VisualizarRegistros();

            Console.WriteLine("Digite o assunto do compromisso ");
            string assunto = Console.ReadLine();

            Console.WriteLine("O assunto será remoto ou presencial? 1 - Presencial, 2 - Remoto");
            string opcao = Console.ReadLine();

            string localizacao = "";
            string link = "";
            if (opcao == "1")
            {
                Console.WriteLine("Digite o localizacao do compromisso ");
                localizacao = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Insira o link do compromisso ");
                link = Console.ReadLine();
            }

            Console.WriteLine("Digite data e hora de inicio Ex. yyyy/MM/dd HH:mm:SS ");
            DateTime dataInicio = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Digite a hora de encerramento do compromisso Ex. yyyy/MM/dd HH:mm:SS ");
            DateTime dataFinal = Convert.ToDateTime(Console.ReadLine());

            compromisso = new Compromisso(idSelecionado, assunto, localizacao, idContato, dataInicio, dataFinal, link);
            if (idSelecionado != 0)
            {
                controlador.Editar(compromisso, idSelecionado);
                return "Sucesso!!";
            }
            else if (compromisso.Validar())
            {
                controlador.Inserir(compromisso);
                return "Sucesso!!";
            }
            else
                return "Contato inválido";
        }
    }
}
