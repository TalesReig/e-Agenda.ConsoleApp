using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e_Agenda.Compartilhado;

namespace e_Agenda.Modulo_Tarefas
{
    public class RepositorioTarefa : RepositorioBase<Tarefa>
    {
        public List<Tarefa> SelecionarEmAberto()
        {
            List<Tarefa> listaDeAbertos= new List<Tarefa>();

            foreach (Tarefa tarefa in registros)
            {
                if (tarefa.TarefaEmAberto)
                    listaDeAbertos.Add(tarefa);
            }
            return OrdenarPorPrioridade(listaDeAbertos);
        }

        public List<Tarefa> SelecionarFechado()
        {
            List<Tarefa> listaDeFechado = new List<Tarefa>();

            foreach (Tarefa tarefa in registros)
            {
                if (tarefa.TarefaEmAberto == false)
                    listaDeFechado.Add(tarefa);
            }
            return OrdenarPorPrioridade(listaDeFechado);
        }

        public List<Tarefa> OrdenarPorPrioridade(List<Tarefa> lista)
        {
            return lista.OrderByDescending(x => (int)x._Prioridade).ToList();
        }
    }
}
