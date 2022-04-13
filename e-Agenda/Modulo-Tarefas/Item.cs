using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.Modulo_Tarefas
{
    internal class Item
    {
        public int Id { get; set; }
        public bool concluida; //|true = concluida | false = pendente;
        public string descuicao;
    }
}
