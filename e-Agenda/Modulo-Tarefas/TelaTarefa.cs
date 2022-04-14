using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e_Agenda.Compartilhado;

namespace e_Agenda.Modulo_Tarefas
{
    public enum Tipo { cadastrar, editar}
    internal class TelaTarefa : TelaBase, ICadastravel
    {

        private readonly Notificador notificador;
        private readonly RepositorioTarefa repositorioTarefa;

        public TelaTarefa(RepositorioTarefa repositorioTarefa, Notificador notificador)
            : base("Cadastro de Tarefas")
        {
            this.repositorioTarefa = repositorioTarefa;
            this.notificador = notificador;
        }
        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Inserir");
            Console.WriteLine("Digite 2 para Editar");
            Console.WriteLine("Digite 3 para Excluir");
            Console.WriteLine("Digite 4 para Visualizar todas as tarefas");
            Console.WriteLine("Digite 5 para Visualizar as tarefas pendetes");
            Console.WriteLine("Digite 6 para Visualizar as tarefas concluidas");
            Console.WriteLine("Digite 7 para atualizar os itens de uma tarefa");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        public void InserirRegistro()
        {
            MostrarTitulo("Inserindo nova Tarefa");

            Tarefa novaTarefa = ObterTarefa(Tipo.cadastrar);

            repositorioTarefa.Inserir(novaTarefa);

            notificador.ApresentarMensagem("Tarefa cadastrada com sucesso!", TipoMensagem.Sucesso);
        }

        public void EditarRegistro()
        {
            MostrarTitulo("Editando Tarefa");

           bool Vazio = VisualizarRegistros("Pesquisando");

            if(Vazio != true)
            {
                notificador.ApresentarMensagem("Lista Vazia", TipoMensagem.Atencao);
            }

            int id = ObterNumeroTarefa();

            Tarefa edicao = repositorioTarefa.SelecionarRegistro(id);

            Tarefa TarefaAtualizada = ObterTarefa(Tipo.editar);

            TarefaAtualizada.setarDataExistente(edicao.DataAbertura);

            string conseguiuEditar = repositorioTarefa.Editar(id,TarefaAtualizada);

            if (conseguiuEditar != "REGISTRO_VALIDO")
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Tarefa editada com sucesso", TipoMensagem.Sucesso);
        }

        public void AtualizarRegistro()
        {
            MostrarTitulo("Atualizando Tarefa");

            bool Vazio = VisualizarRegistros("Pesquisando");

            if (Vazio != true)
            {
                notificador.ApresentarMensagem("Lista Vazia", TipoMensagem.Atencao);
            }

            int id = ObterNumeroTarefa();

            Tarefa edicao = repositorioTarefa.SelecionarRegistro(id);

            Tarefa TarefaAtualizada = AtualizarPorcentagem(edicao);

            TarefaAtualizada.CalcularPercentual();

            TarefaAtualizada.setarDataExistente(edicao.DataAbertura);

            string conseguiuEditar = repositorioTarefa.Editar(id, TarefaAtualizada);

            if (conseguiuEditar != "REGISTRO_VALIDO")
                notificador.ApresentarMensagem("Não foi possível atualizar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Tarefa atualizada com sucesso", TipoMensagem.Sucesso);
        }

        public void ExcluirRegistro()
        {
            MostrarTitulo("Excluindo Tarefa");

            bool temTarefasCadastradas = VisualizarRegistros("Pesquisando");

            if (temTarefasCadastradas == false)
            {
                notificador.ApresentarMensagem(
                    "Nenhuma Tarefa cadastrada para poder excluir", TipoMensagem.Atencao);
                return;
            }

            int id = ObterNumeroTarefa();

            repositorioTarefa.Excluir(id);

            notificador.ApresentarMensagem("Tarefa excluída com sucesso", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Tarefas");

            List<Tarefa> Tarefas = repositorioTarefa.SelecionarTodos();

            if (Tarefas.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhuma Tarefa disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Tarefa Tarefa in Tarefas)
                Console.WriteLine(Tarefa.ToString());

            Console.ReadLine();

            return true;
        }

        public bool VisualizarRegistrosConcluidos(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Tarefas Cloncluidas");

            List<Tarefa> TarefasConcluidas = repositorioTarefa.SelecionarEmAberto();

            if (TarefasConcluidas.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhuma Tarefa disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Tarefa Tarefa in TarefasConcluidas)
                Console.WriteLine(Tarefa.ToString());

            Console.ReadLine();

            return true;
        }

        public bool VisualizarRegistrosPendetes(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Tarefas em aberta");

            List<Tarefa> TarefasPendetes = repositorioTarefa.SelecionarFechado();

            if (TarefasPendetes.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhuma Tarefa disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Tarefa Tarefa in TarefasPendetes)
                Console.WriteLine(Tarefa.ToString());

            Console.ReadLine();

            return true;
        }



        #region métodos privados;

        public Tarefa ObterTarefa(Tipo tipo)
        {
            Console.Write("Digite o título da Tarefa: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite o nível de prioridade :[0] Baixa. [1] Media. [2] Alta. ");
            int prioridade = Convert.ToInt32(Console.ReadLine());

            List<Item> itens = ObterListaItens();

            Tarefa Tarefa = new Tarefa(titulo,(Prioridade)prioridade, itens);

            return Tarefa;
        }

        private int ObterNumeroTarefa()
        {
            int numeroTarefa;
            bool numeroTarefaEncontrado;

            do
            {
                Console.Write("Digite o número da Tarefa que deseja editar: ");
                numeroTarefa = Convert.ToInt32(Console.ReadLine());

                numeroTarefaEncontrado = repositorioTarefa.ExisteRegistro(x => x.numero == numeroTarefa);

                if (numeroTarefaEncontrado == false)
                    notificador.ApresentarMensagem("Número de Tarefa não encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroTarefaEncontrado == false);

            return numeroTarefa;
        }

        private Tarefa AtualizarPorcentagem(Tarefa tarefa)
        {
            List<Item> LItens = new List<Item>();
            foreach (Item item in tarefa._Itens)
            {
                Console.WriteLine(item.ToString());
                Console.WriteLine("O Item esta concluido (true/false): ");
                bool concluida = Convert.ToBoolean(Console.ReadLine().ToLower());
                string descricao = item.descricao;

                Item item1 = new Item(concluida, descricao);
                LItens.Add(item1);
            }
            tarefa._Itens = LItens;
            return tarefa;
        }

        private List<Item> ObterListaItens()
        {
            List<Item> itens = new List<Item>();

            Console.Write("Digite 's' ou informe a quantidade de itens da tarefa: ");
            string lerTela = Console.ReadLine();

            if (lerTela == "s")
                return itens;

            int quantidadeItens = Convert.ToInt32(lerTela);
            for (int i = 0; i < quantidadeItens; i++)
            {
                Item item = new Item();

                item.Id = i + 1;
                Console.Write($"Informe a descrição do {i + 1}º item: ");
                item.descricao = Console.ReadLine();
                item.concluida = false;
                itens.Add(item);
            }

            return itens;
        }

        #endregion
    }
}
