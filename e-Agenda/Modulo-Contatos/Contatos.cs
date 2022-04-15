using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using e_Agenda.Compartilhado;

namespace e_Agenda.Modulo_Contatos
{
    public class Contatos : Entidadebase
    {
        public string _Nome;
        private string _Email;
        public string _Telefone;
        public string _Empresa;
        public string _Cargo;

        public Contatos(string nome, string email, string telefone, string empresa, string cargo)
        {
            this._Nome = nome;
            this._Email = email;
            this._Telefone = telefone;
            this._Empresa = empresa;
            this._Cargo = cargo;
        }
        public override string Validar()
        {
            string mensagem = "";

            if (ValidarEmail(_Email) == false)
                mensagem = "EMAIL_INVALIDO";

            if (ValidaTelefone(_Telefone) == false)
                mensagem += "TELEFONE_INVALIDO";

            if (mensagem != "")
                return mensagem;

            return "REGISTRO_VALIDO";
        }

        public override string ToString()
        {
            return "ID: "+numero+" | Nome : "+this._Nome+" | Email : "+this._Email+ "| Telefone : " +this._Telefone+" | Empresa : "+this._Empresa+" | Cargo : "+this._Cargo;
        }
        #region metodos privados
        private bool ValidaTelefone(string telefone)
        {
            Regex Rgx = new Regex(@"^\(\d{2}\)\d{5}-\d{4}$"); //formato (XX)XXXXX-XXXX

            if (Rgx.IsMatch(telefone) == false)
                return false;

            else
                return true;
        }

        private bool ValidarEmail(string email)
        { 
            int indexArr = email.IndexOf("@");
            bool EmailValido = false;

            if (indexArr > 0)
                EmailValido = true; 

            return EmailValido;
        }

        
        #endregion
    }
}
