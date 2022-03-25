using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.Atendimentos
{
    public class FiltroAtendimentos
    {
        public string NomedoCliente { get; set; }
        public string DatadoAtendimento { get; set; }
        public int? CanaldeAtendimento { get; set; }
        public string Celular { get; set; }
        public string TelefoneFixo { get; set; }
        public int? StatusdoAtendimento { get; set; }
        public string PesquisarDataInicial { get; set; }
        public string PesquisarDataFinal { get; set; }
        public int? IdAtendente { get; set; }
        public int? IdUnidade { get; set; }
    }
}
