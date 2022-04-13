using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e_Agenda.Compartilhado;

namespace e_Agenda.Modulo_Contatos
{
    internal class TelaContatos : TelaBase, ICadastravel
    {
        RepositorioContatos repositorioContato;
        Notificador notificador;
        public TelaContatos(RepositorioBase<Contatos> repositorioContatos, Notificador notificador) : base("Cadastro de Contatos")
        {
            this.repositorioContato = (RepositorioContatos)repositorioContatos;
            this.notificador = notificador;
        }

        public override string MostrarOpcoes()
        {

            MostrarTitulo("Menu de Contatos:");

            Console.WriteLine("\nDigite 1 para Cadastrar");
            Console.WriteLine("Digite 2 para Editar");
            Console.WriteLine("Digite 3 para Excluir");
            Console.WriteLine("Digite 4 para Visualizar");
            Console.WriteLine("Digite 5 para Visualizar Ordenado Cargo");

            Console.WriteLine("Digite s para sair");

            string opcaoSelecionada = Console.ReadLine();

            return opcaoSelecionada;
        }

        public void InserirRegistro()
        {
            MostrarTitulo("Inserindo novo contato");

            Contatos novoContato = ObterContato();

            string mensagemValidacao = repositorioContato.Inserir(novoContato);

            if (mensagemValidacao == "REGISTRO_VALIDO")
                notificador.ApresentarMensagem("Contato inserido !!!", TipoMensagem.Sucesso);

            else
                notificador.ApresentarMensagem("Contato não inserido !!! Erro de Validação Encontrado !!!", TipoMensagem.Erro);
        }

        public void EditarRegistro()
        {
            MostrarTitulo("Editando nova tarefa");

            bool temTarefaCadastrada = VisualizarRegistros("Pesquisando");

            if (!temTarefaCadastrada)
            {
                notificador.ApresentarMensagem("Nenhuma tarefa cadastrada para poder editar", TipoMensagem.Atencao);
                return;
            }

            int idContato = ObterNumeroContato();

            Contatos novaTarefa = ObterContato();

            string mensagemValidacao = repositorioContato.Editar(idContato, novaTarefa);

            if (mensagemValidacao == "REGISTRO_VALIDO")
                notificador.ApresentarMensagem("Contato inserido com sucesso !!!", TipoMensagem.Sucesso);

            else
                notificador.ApresentarMensagem("Contato não inserido, erro de validação !!!", TipoMensagem.Erro);

        }

        public void ExcluirRegistro()
        {
            MostrarTitulo("Excluindo Conato: ");

            bool temTarefaCadastrada = VisualizarRegistros("Pesquisando");

            if (!temTarefaCadastrada)
            {
                notificador.ApresentarMensagem("Nenhuma tarefa cadastrada para poder editar", TipoMensagem.Atencao);
                return;
            }

            int idTarefa = ObterNumeroContato();

            repositorioContato.Excluir(idTarefa);

            notificador.ApresentarMensagem("Contato exlcuido !!!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizado)
        {
            if (tipoVisualizado == "Tela")
                MostrarTitulo("Visualização de Contato");

            List<Contatos> contatos = repositorioContato.SelecionarTodos();

            if (contatos.Count == 0)
                return false;

            foreach (Contatos contato in contatos)
            {
                Console.WriteLine(contato.ToString());
            }
            return true;
        }

        #region métodos privados

        private Contatos ObterContato()
        {
            Console.Write("Digite o nome do Contato: ");
            string nome = Console.ReadLine();

            Console.Write("\nDigite o email no seguinte formato (conta@dominio.com)");
            string email = Console.ReadLine();

            Console.Write("\nDigite o telefone no seguindo o seguinte formato (00)00000-0000");
            string telefone = Console.ReadLine();

            Console.Write("Digite a empresa: ");
            string empresa = Console.ReadLine();

            Console.Write("Digite o cargo: ");
            string cargo = Console.ReadLine();


            Contatos contato = new Contatos(nome, email, telefone, empresa, cargo);

            return contato;
        }

        private int ObterNumeroContato()
        {
            int id;
            bool numeroContatoEncontrado;

            do
            {
                Console.Write("Digite o número do contatp que deseja editar: ");
                id = Convert.ToInt32(Console.ReadLine());

                numeroContatoEncontrado = repositorioContato.ExisteRegistro(id);

                if (numeroContatoEncontrado == false)
                    notificador.ApresentarMensagem("Registro não encontrado, tente de novo: ", TipoMensagem.Atencao);

            } while (numeroContatoEncontrado == false);
            return id;
        }
        #endregion

    }
}
