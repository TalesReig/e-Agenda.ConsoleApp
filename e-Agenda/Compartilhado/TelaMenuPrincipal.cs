using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e_Agenda.Modulo_Tarefas;
using e_Agenda.Modulo_Contatos;
using e_Agenda.Modulo_Compromissos;

namespace e_Agenda.Compartilhado
{
    internal class TelaMenuPrincipal
    {
        private RepositorioBase<Tarefa> repositorioTarefas;
        private TelaTarefa telaTarefas;

        private RepositorioBase<Contatos> repositorioContatos;
        private TelaContatos telaContatos;



        public TelaMenuPrincipal(Notificador notificador)
        {
            repositorioTarefas = new RepositorioTarefa();
            telaTarefas = new TelaTarefa(repositorioTarefas, notificador);
            
            repositorioContatos = new RepositorioContatos();
            telaContatos = new TelaContatos(repositorioContatos, notificador);
            // 
            // repositorioCompromisso = new ControladorCompromisso();
            // telaCompromisso = new TelaCompromisso(controladorCompromisso, telaContato, controladorContato);
        }
        public TelaBase ObterTela()
        {
            string opcao = MostrarOpcoes();

            TelaBase tela = null;

            switch (opcao)
            {
                case "1":
                    tela = telaTarefas;
                    return tela;
                case "2":
                    tela = telaContatos;
                    return tela;
                case "3":
                    //tela = TelaCompromissos;
                    return tela;
                case "s":
                    tela = null;
                    return tela;

            }
            return tela;
        }
       public string MostrarOpcoes()
        {
            string opcao;
            do
            {
                Console.Clear();

                Console.WriteLine("e-Agenda 1.0");

                Console.WriteLine();

                Console.WriteLine("Digite 1 para o Cadastro de Tarefas");
                Console.WriteLine("Digite 2 para o Cadastro de Contatos");
                Console.WriteLine("Digite 3 para o Cadastro de Compromissos");

                Console.WriteLine("Digite S para Sair");
                Console.WriteLine();

                Console.Write("Opção: ");
                opcao = Console.ReadLine();
            } while (opcao != "1" && opcao != "2" && opcao != "3" && opcao != "s" && opcao != "S");

            return opcao;
        }

    }
}
