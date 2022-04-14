using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e_Agenda.Compartilhado;

namespace e_Agenda.Modulo_Contatos
{
    internal class RepositorioContatos : RepositorioBase<Contatos>
    {
        public List<Contatos> OrdenadosPorCargo()
        {
            List<Contatos> contatosOrdenadosPorCargo = registros;

            contatosOrdenadosPorCargo.Sort((x, y) => string.Compare(x._Cargo, y._Cargo));

            return contatosOrdenadosPorCargo;
        }
    }
}
