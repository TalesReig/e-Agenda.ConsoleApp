using System;
using e_Agenda.Compartilhado;
using e_Agenda.Modulo_Contatos;
using e_Agenda.Modulo_Tarefas;

namespace e_Agenda
{
    internal class Program
    {
        static Notificador notificador = new Notificador();
        static TelaMenuPrincipal menuPrincipal = new TelaMenuPrincipal(notificador);
        static void Main(string[] args)
        {
            while (true)
            {
                TelaBase telaSelecionada = menuPrincipal.ObterTela();

                if (telaSelecionada is null)
                    return;

                string opcaoSelecionada = telaSelecionada.MostrarOpcoes();

                if (telaSelecionada is ICadastravel)
                    GerenciarCadastroBasico(telaSelecionada, opcaoSelecionada);

                //else if (telaSelecionada is TelaCadastroEspecial)
                //    GerenciarCadastroEspecial(telaSelecionada, opcaoSelecionada);
            }


        }

        private static void GerenciarCadastroEspecial(TelaBase telaSelecionada, string opcaoSelecionada)
        {
            throw new NotImplementedException();
        }

        private static void GerenciarCadastroBasico(TelaBase telaSelecionada, string opcaoSelecionada)
        {
            ICadastravel telaCadastroBasico = (ICadastravel)telaSelecionada;

            if (opcaoSelecionada == "1")
                telaCadastroBasico.InserirRegistro();

            else if (opcaoSelecionada == "2")
                telaCadastroBasico.EditarRegistro();

            else if (opcaoSelecionada == "3")
                telaCadastroBasico.ExcluirRegistro();

            else if (opcaoSelecionada == "4")
            {
                bool temRegistros = telaCadastroBasico.VisualizarRegistros("Tela");

                if (!temRegistros)
                    notificador.ApresentarMensagem("Nenhum registro disponível!", TipoMensagem.Atencao);
            }

            if (telaSelecionada is TelaContatos)
            {
             //   TelaTarefa TelaTarefas = (TelaTarefa)telaSelecionada;
             //
             //   if (opcaoSelecionada == "5")
             //   {
             //       bool temRegistros = TelaTarefas.VisualizarAmigosComMulta("Tela");
             //
             //       if (!temRegistros)
             //           notificador.ApresentarMensagem("Não há nenhum amigo com multa aberta.", TipoMensagem.Atencao);
             //   }
             //
             //   else if (opcaoSelecionada == "6")
             //       TelaTarefas.PagarMulta();
            }
        }
    }
}
