using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e_Agenda.Compartilhado;
using e_Agenda.Modulo_Contatos;

namespace e_Agenda.Modulo_Compromissos
{
    public class Compromissos : Entidadebase
    {
        public string assunto;
        public string local;
        public DateTime data;
        public TimeSpan horaInicio;
        public TimeSpan horaTermino;
        public Contatos contato;


        public Compromissos(string assunto, string local, DateTime data, TimeSpan horaInicio, TimeSpan horaTermino, Contatos contato)
        {
            this.assunto = assunto;
            this.local = local;
            this.data = data;
            this.horaInicio = horaInicio;
            this.horaTermino = horaTermino;
            this.contato = contato;
        }

        public override string Validar()
        {
            return "REGISTRO_VALIDO";
        }

        public override string ToString()
        {
            string mensagem = $"ID: {this.numero} | Assunto: {this.assunto} | Local: {this.local} | Data: {this.data.ToString("dd/MM/yyyy")} | Inicio: {this.horaInicio.ToString(@"hh\:mm")} | Termino: {this.horaTermino.ToString(@"hh\:mm")}\n";
            mensagem += $"Nome: {this.contato._Nome} | Telefone: {this.contato._Telefone} | Empresa: {this.contato._Empresa}";
            return mensagem;
        }
    }
}
