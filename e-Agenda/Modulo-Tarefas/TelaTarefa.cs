using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e_Agenda.Compartilhado;

namespace e_Agenda.Modulo_Tarefas
{
    internal class TelaTarefa : TelaBase, ICadastravel
    {

        private readonly Notificador notificador;
        private readonly RepositorioBase<Tarefa> repositorioTarefa;

        public TelaTarefa(RepositorioBase<Tarefa> repositorioTarefa, Notificador notificador)
            : base("Cadastro de Tarefas")
        {
            this.repositorioTarefa = repositorioTarefa;
            this.notificador = notificador;
        }

        public void InserirRegistro()
        {
            MostrarTitulo("Inserindo nova Tarefa");

            Tarefa novaTarefa = ObterTarefa();

            //string statusValidacao = repositorioTarefa.Inserir(novaTarefa);
            repositorioTarefa.Inserir(novaTarefa);

            //if (statusValidacao == "REGISTRO_VALIDO")
                notificador.ApresentarMensagem("Tarefa cadastrada com sucesso!", TipoMensagem.Sucesso);
            //else
            //    notificador.ApresentarMensagem(statusValidacao, TipoMensagem.Erro);
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

            Tarefa TarefaAtualizada = ObterTarefa();

            bool conseguiuEditar = repositorioTarefa.Editar(id,TarefaAtualizada);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Tarefa editada com sucesso", TipoMensagem.Sucesso);
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

        #region métodos privados;

        public Tarefa ObterTarefa()
        {
            Console.Write("Digite o título da Tarefa: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite o nível de prioridade \n\t[0] Baixa.\n\t[1] Media.\n\t[2] Alta. ");
            int prioridade = Convert.ToInt32(Console.ReadLine());

            Tarefa Tarefa = new Tarefa(titulo,(Prioridade)prioridade);

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

        #endregion
    }
}
