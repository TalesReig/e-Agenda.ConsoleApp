using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e_Agenda.Compartilhado;

namespace e_Agenda.Modulo_Tarefas
{
    public class Tarefa : Entidadebase
    {
        private bool _EmAberto; // true
        private readonly string _Titulo;
        private readonly Prioridade _Prioridade;
        private readonly DateTime _DataAbertura;
        private readonly DateTime _DataConclusao;

        public Tarefa(string titulo, Prioridade prioridade)
        {
            _EmAberto = true;
            _Titulo = titulo;
            _Prioridade = prioridade;
            _DataAbertura = DateTime.Today;
        }

        public override string ToString()
        {
            return "Número: " + numero +" Titulo: "+ _Titulo +" Prioridade: "+ _Prioridade +" Data de Inicio: "+ _DataAbertura+" Estado: ";
        }

        public bool Equals(Tarefa other)
        {
            throw new NotImplementedException();
        }
    }
}
