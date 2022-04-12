using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.Compartilhado
{
    public class RepositorioBase<T> where T : Entidadebase
    {
        protected readonly List<T> registros;

        protected int contadorNumero;

        public RepositorioBase()
        {
            registros = new List<T>();
        }

        public virtual string Inserir(T entidade)
        {
            string mensagemValidacao = entidade.Validar();
            if (mensagemValidacao == "REGISTRO_VALIDO")
            {
                entidade.numero = ++contadorNumero;
                registros.Add(entidade);
            }
            return "REGISTRO_VALIDO";
        }

        public bool Editar(int numeroSelecionado, T novaEntidade)
        {
            foreach (T entidade in registros)
            {
                if (numeroSelecionado == entidade.numero)
                {
                    novaEntidade.numero = entidade.numero;

                    int posicaoParaEditar = registros.IndexOf(entidade);
                    registros[posicaoParaEditar] = novaEntidade;

                    return true;
                }
            }

            return false;
        }

        public bool Excluir(int numeroSelecionado)
        {
            registros.RemoveAll(x => x.numero == numeroSelecionado);
            return true;
        }

        public T SelecionarRegistro(int numeroSelecionado)
        {
            foreach (T entidade in registros)
            {
                if (numeroSelecionado == entidade.numero)
                    return entidade;
            }

            return null;
        }

        public T EncontrarRegistro(int numeroRegistro)
        {
            return registros.Find(x => x.numero == numeroRegistro);
        }

        public List<T> SelecionarTodos()
        {
            return registros;
        }

        public bool ExisteRegistro(int numeroSelecionado)
        {
            foreach (T entidade in registros)
                if (numeroSelecionado == entidade.numero)
                    return true;

            return false;
        }

        public bool ExisteRegistro(Predicate<T> condicao)
        {
            foreach (T entidade in registros)
                if (condicao(entidade))
                    return true;

            return false;
        }
    }
}
