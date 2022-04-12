using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.Compartilhado
{
    public abstract class Entidadebase
    {
        public int numero;

        public virtual string Validar()
        {
            return "REGISTRO_VALIDO";
        }
    }
}
