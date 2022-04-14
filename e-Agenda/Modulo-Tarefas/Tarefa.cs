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
        private double _Percentual;
        private readonly string _Titulo;
        public readonly Prioridade _Prioridade;
        private DateTime _DataAbertura { get; set; }
        private readonly DateTime _DataConclusao;
        public List<Item> _Itens;


        public void setarDataExistente(DateTime data)
        {
            _DataAbertura = data;
        }

        public DateTime DataAbertura{ get{ return _DataAbertura;} }

        public bool TarefaEmAberto { get { return _EmAberto; } }

        public Tarefa(string titulo, Prioridade prioridade, List<Item> itens)
        {
            _EmAberto = true;
            _Titulo = titulo;
            _Itens = itens;
            _Prioridade = prioridade;
            _DataAbertura = DateTime.Today;
        }

        public double CalcularPercentual()
        {
            int totConcluidas = 0;
            foreach(Item item in _Itens)
            {
                if (item.concluida == true)
                    totConcluidas++;
            }

            this._Percentual = totConcluidas / _Itens.Count();

            if (_Percentual == 100)
                fechar();

            return this._Percentual;
        }

        public override string ToString()
        {
            return "Número: " + numero +" Titulo: "+ _Titulo +" Prioridade: "+ _Prioridade +" Data de Inicio: "+ _DataAbertura+" Estado: ";
        }

        public override string Validar()
        {
            return "REGISTRO_VALIDO";
        }

        public void fechar()
        {
            _EmAberto = false;
        }
    }
}
