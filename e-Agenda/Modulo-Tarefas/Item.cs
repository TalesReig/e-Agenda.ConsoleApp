using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.Modulo_Tarefas
{
    public class Item
    {
        public int Id { get; set; }
        public bool concluida; //|true = concluida | false = pendente;
        public string descricao;

        public Item(bool concluida, string descricao)
        {
            this.concluida = concluida;
            this.descricao = descricao;
        }

        public Item()
        {
        }

        public override string ToString()
        {
            string status = concluida == true ? "Concluida" : "Aberta";
            return $"Id {Id},  Descricao: {descricao},{status}"; 
        }
    }
}
