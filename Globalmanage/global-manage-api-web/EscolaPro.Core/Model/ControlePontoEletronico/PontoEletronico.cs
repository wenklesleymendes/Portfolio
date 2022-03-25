using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.Funcionario;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.ControlePontoEletronico
{
    public class PontoEletronico : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public DateTime? Entrada1 { get; set; }
        public DateTime? Saida1 { get; set; }

        public DateTime? Entrada2 { get; set; }
        public DateTime? Saida2 { get; set; }

        public DateTime? Entrada3 { get; set; }
        public DateTime? Saida3 { get; set; }

        public DateTime? Entrada4 { get; set; }
        public DateTime? Saida4 { get; set; }
        public TipoOcorrenciaPontoEnum TipoOcorrenciaPonto { get; set; }
        public string Observacao { get; set; }
        public ICollection<Anexo?> Anexo { get; set; }
        public int? UsuarioId { get; set; }
        public int? FeriasId { get; set; }
        public bool ApenasFerias { get; set; }
        public int? FuncionarioId { get; set; }
        public string NumeroPIS { get; set; }
        public string NomeArquivo { get; set; }
        public RegimeContratacaoEnum RegimeContratacao { get; set; }
        public bool Pago { get; set; }
        public int? FolhaPagamentoId { get; set; }
    }
}
