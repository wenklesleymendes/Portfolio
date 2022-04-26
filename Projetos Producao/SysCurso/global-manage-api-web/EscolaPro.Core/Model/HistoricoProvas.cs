using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model
{
    public class HistoricoProvas : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string parceiro { get; set; }
        public string nomecompleto { get; set; }
        public string datadenascimento { get; set; }
        public string sexo { get; set; }
        public string estadocivil { get; set; }
        public string naturalidade { get; set; }
        public string nacionalidade { get; set; }
        public string nomedamae { get; set; }
        public string nomedopai { get; set; }
        public string email { get; set; }
        public string rg { get; set; }
        public string orgaoexpedidor { get; set; }
        public string ufrg { get; set; }
        public string cpf { get; set; }
        public string titulodeeleitor { get; set; }
        public string zonaeleitoral { get; set; }
        public string secaoeleitoral { get; set; }
        public string enderecoresidencial { get; set; }
        public string numeroresidencial { get; set; }
        public string complementoresidencial { get; set; }
        public string bairroresidencial { get; set; }
        public string cidaderesidencial { get; set; }
        public string ufresidencial { get; set; }
        public string cepresidencial { get; set; }
        public string dddfixoresidencial { get; set; }
        public string telefonefixoresidencial { get; set; }
        public string dddcelularresidencial { get; set; }
        public string celularresidencial { get; set; }
        public string curso { get; set; }

        //listaGeralDeInscritosParaProva
        public string statusprova { get; set; }
        public string unidade { get; set; }
        public string docconferidopor { get; set; }
        public string dataconferenciadoc { get; set; }
        public string emailporcpf { get; set; }
    }
}