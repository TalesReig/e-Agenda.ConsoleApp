using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.Modulo_Tarefas
{
    public enum Prioridade : int
    {
        [Description("Prioridade Baixa")]
        Baixa = 0,

        [Description("Prioridade Normal")]
        Normal = 1,

        [Description("Prioridade Alta")]
        Alta = 2
    }
}
