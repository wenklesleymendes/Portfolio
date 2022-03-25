using EscolaPro.Core.Model.ReguaContato;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.ReguaContato
{
    public class ReguaContatoFilaDto
    {
        public int Id { get; set; }
        public int ReguaContatoRegrasId { get; set; }
        public int AlunoId { get; set; }
        public int? UnidadeId { get; set; }
        public int PagamentoId { get; set; }
        public int Prioridade { get; set; }
        public StatusFila StatusFila { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime DataEnvio { get; set; }
        public string MensagemErro { get; set; }
        public bool EnviadaComSucesso { get; set; }
    }
}
