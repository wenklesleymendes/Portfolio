using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Core.Model.PainelMatricula.Enums;
using EscolaPro.Core.Model.Tickets;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.Solicitacoes
{
    public class Solicitacao : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public TipoSolicitacaoEnum TipoSolicitacao { get; set; }
        public decimal? Valor { get; set; }
        // Campos novos
        public bool IsBalcao { get; set; }
        public bool IsPreDefinida { get; set; }
        public bool IsAprovadoProva { get; set; }
        public ICollection<Anexo> Anexo { get; set; }
        public ICollection<SolicitacaoCurso> SolicitacaoCurso { get; set; }
        public ICollection<SolicitacaoFuncionarioTicket> SolicitacaoFuncionarioTicket { get; set; }
        public Unidade Unidade { get; set; }
        public int? UnidadeId { get; set; }
        public CentroCusto CentroCusto { get; set; }
        public int? CentroCustoId { get; set; }
        public int? QuantidadeParcelaPaga { get; set; }
        public bool IsCursoQuitado { get; set; }
        public bool EnviaTicket { get; set; }
        public bool EnviaTicketPosPgto { get; set; }

        public bool EnviaEmail { get; set; }
        public bool EnviaEmailPosPgto { get; set; }
        public ICollection<EmailDestinatario> EmailDestinatario { get; set; }
        public string EmailTitulo { get; set; }
        public string EmailConteudo { get; set; }
        public ICollection<StatusCertificado> StatusCertificado { get; set; }
        public ICollection<StatusProva> StatusProvaEnum { get; set; }
        public bool IsPendenciaDocumental { get; set; }
        public bool IsAnexo { get; set; }
        public byte[] Imagem { get; set; }
        public string Extensao { get; set; }
        public bool? NacionalTec { get; set; }
    }
}
