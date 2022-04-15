using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e_Agenda.Compartilhado;
using e_Agenda.Modulo_Contatos;

namespace e_Agenda.Modulo_Compromissos
{
    public class TelaCompromissos : TelaBase, ICadastravel
    {
        Notificador notificador;
        RepositorioCompromissos repositorioCompromisso;

        RepositorioContatos repositorioContato;
        TelaContatos telaContato;

        public TelaCompromissos( RepositorioCompromissos repositorioCompromisso, RepositorioContatos repositorioContato, TelaContatos telaContato, Notificador notificador) : base("Tela Compromisso")
        {
            this.notificador = notificador;
            this.repositorioCompromisso = repositorioCompromisso;
            this.repositorioContato = repositorioContato;
            this.telaContato = telaContato;
        }

        public override string MostrarOpcoes()
        {
            string opcaoSelecionada;

            MostrarTitulo("Menu de Compromissos:");

            Console.WriteLine("Digite 1 para Cadastrar");
            Console.WriteLine("Digite 2 para Editar");
            Console.WriteLine("Digite 3 para Excluir");
            Console.WriteLine("Digite 4 para Visualizar");
            // 
            // Console.WriteLine("Digite 5 para Compromissos do dia");
            // Console.WriteLine("Digite 6 para Compromissos da semana");
            // Console.WriteLine("Digite 7 para Compromissos passados");
            // Console.WriteLine("Digite 8 para Compromissos futuros");

            Console.WriteLine("Digite s para sair");

            opcaoSelecionada = Console.ReadLine();

            return opcaoSelecionada;
        }

        public void InserirRegistro()
        {
            MostrarTitulo("Inserindo novo compromisso:");
            if (!telaContato.VisualizarRegistros(""))
            {
                notificador.ApresentarMensagem("Nenhum contato cadastrado", TipoMensagem.Atencao);
                return;
            }
            Contatos contato = ObterContato();
            Compromissos novoCompromisso = ObterCompromisso(contato);

            string mensagemValidacao = repositorioCompromisso.Inserir(novoCompromisso);

            if (mensagemValidacao == "REGISTRO_VALIDO")
                notificador.ApresentarMensagem("Contato inserido com sucesso!", TipoMensagem.Sucesso);

            else
                notificador.ApresentarMensagem("Contato não inserido, erro na validação dos campos", TipoMensagem.Erro);
        }

        public void EditarRegistro()
        {
            MostrarTitulo("Editando compromisso:");
            if (!VisualizarRegistros("Tela"))
            {
                notificador.ApresentarMensagem("Nenhum compromisso cadastrado", TipoMensagem.Atencao);
                return;
            }

            int idCompromisso = ObterNumeroCompromisso();

            if (!telaContato.VisualizarRegistros("Tela"))
            {
                notificador.ApresentarMensagem("Nenhum compromisso cadastrado", TipoMensagem.Atencao);
                return;
            }

            Contatos contato = ObterContato();

            Compromissos novoCompromisso = ObterCompromisso(contato);

            string mensagemValidacao = repositorioCompromisso.Editar(idCompromisso, novoCompromisso);

            if (mensagemValidacao == "REGISTRO_VALIDO")
                notificador.ApresentarMensagem("Compromisso editado com sucesso !!!", TipoMensagem.Sucesso);

            else
                notificador.ApresentarMensagem("Compromisso não editado, erro de validação !!!", TipoMensagem.Erro);
        }

        public void ExcluirRegistro()
        {
            MostrarTitulo("Excluindo compromisso:");
            if (!telaContato.VisualizarRegistros("Tela"))
            {
                notificador.ApresentarMensagem("Nenhum compromisso cadastrado", TipoMensagem.Atencao);
                return;
            }

            int idCompromisso = ObterNumeroCompromisso();

            repositorioCompromisso.Excluir(idCompromisso);

            notificador.ApresentarMensagem("Compromisso excluído com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizado)
        {
            if (tipoVisualizado == "Tela")
                MostrarTitulo("Visualização de compromisso");

            List<Compromissos> compromissos = repositorioCompromisso.SelecionarTodos();

            if (compromissos.Count == 0)
                return false;

            foreach (Compromissos compromisso in compromissos)
            {
                Console.WriteLine(compromisso.ToString());

                Console.WriteLine();
            }

            return true;
        }

        #region métodos privados
        private Contatos ObterContato()
        {
            while (true)
            {
                Console.Write("Informe o id do contato que deseja anexar ao compromisso: ");
                int id = Convert.ToInt32(Console.ReadLine());

                if (repositorioContato.ExisteRegistro(id))
                    return repositorioContato.SelecionarRegistro(id);
                else
                    notificador.ApresentarMensagem("Contato não encontrado, tente novamente!.\n", TipoMensagem.Atencao);
            }
        }

        private Compromissos ObterCompromisso(Contatos contato)
        {
            Console.Write("Digite o assunto: ");
            string assunto = Console.ReadLine();

            Console.Write("\nDigite o local : ");
            string local = Console.ReadLine();

            DateTime dataCompromisso;
            while (true)
            {
                Console.Write("\nDigite a data: ");
                string data = Console.ReadLine();

                bool conversaoRealizada = DateTime.TryParse(data, out dataCompromisso);

                if (conversaoRealizada)
                    break;

                Console.WriteLine("Formato invalido, tente novamente");
            }

            TimeSpan horaInicio;
            while (true)
            {
                Console.Write("Digite a hora de inicio (Ex: 00:00): ");
                string hora = Console.ReadLine();

                bool conversaoRealizada = TimeSpan.TryParse(hora, out horaInicio);

                if (conversaoRealizada)
                    break;

                Console.WriteLine("Formato invalido, tente novamente");
            }


            TimeSpan horaFim;
            while (true)
            {
                Console.Write("Digite a hora de término: (Ex: 00:00): ");
                string hora = Console.ReadLine();

                bool conversaoRealizada = TimeSpan.TryParse(hora, out horaFim);

                if (conversaoRealizada && horaInicio < horaFim)
                    break;

                Console.WriteLine("Formato invalido ou término inferior a hora de inicio, tente novamente");
            }
            Compromissos compromisso = new Compromissos(assunto, local, dataCompromisso, horaInicio, horaFim, contato);

            return compromisso;
        }

        private int ObterNumeroCompromisso()
        {
            int id;
            bool numeroContatoEncontrado;

            do
            {
                Console.Write("Digite o número do compromisso que deseja editar: ");
                id = Convert.ToInt32(Console.ReadLine());

                numeroContatoEncontrado = repositorioCompromisso.ExisteRegistro(id);

                if (numeroContatoEncontrado == false)
                    notificador.ApresentarMensagem("Número do compromisso não encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroContatoEncontrado == false);
            return id;
        }

        #endregion
    }
}
