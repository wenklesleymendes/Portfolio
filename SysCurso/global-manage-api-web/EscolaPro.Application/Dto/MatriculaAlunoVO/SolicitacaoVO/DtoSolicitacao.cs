using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Core.Model.Solicitacoes;
using EscolaPro.Service.Dto.MatriculaAlunoVO.SolicitacaoVO;
using EscolaPro.Service.Dto.UnidadeVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.MatriculaAlunoVO
{
    public class DtoSolicitacao
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public TipoSolicitacaoEnum TipoSolicitacao { get; set; }
        public decimal? Valor { get; set; }
        // Campos novos
        public bool IsBalcao { get; set; }
        public bool IsPreDefinida { get; set; }
        public bool IsAprovadoProva { get; set; }
        //public ICollection<Anexo> Anexo { get; set; }
        public int[] SolicitacaoCursoIds { get; set; }
        public int?[] FuncionarioIds { get; set; }
        public ICollection<DtoSolicitacaoCurso> SolicitacaoCurso { get; set; }
        public ICollection<DtoSolicitacaoFuncionarioTicket> SolicitacaoFuncionarioTicket { get; set; }
        public DtoUnidadeTurma Unidade { get; set; }
        public int? UnidadeId { get; set; }
        public DtoCentroCusto CentroCusto { get; set; }
        public int? CentroCustoId { get; set; }
        //public bool IsCertificadoStatusEntregue { get; set; }
        //public bool IsInscritoProva { get; set; }
        public int? QuantidadeParcelaPaga { get; set; }
        public bool IsCursoQuitado { get; set; }

        public string[] EmailDestinatarios { get; set; }
        public int[] StatusCertificados { get; set; }
        public int[] StatusProvas { get; set; }

        public bool EnviaTicket { get; set; }
        public bool EnviaTicketPosPgto { get; set; }

        public bool EnviaEmail { get; set; }
        public bool EnviaEmailPosPgto { get; set; }
        public ICollection<EmailDestinatario> EmailDestinatario { get; set; }
        public string EmailTitulo { get; set; }
        public string EmailConteudo { get; set; }
        public ICollection<StatusCertificado> StatusCertificado { get; set; }
        public ICollection<StatusProva> StatusProvaEnum { get; set; }
        
        public bool ExistePendencia { get; set; }
        public string Mensagem { get; set; }
        public bool IsPendenciaDocumental { get; set; }
        public bool IsAnexo { get; set; }
        public byte[] Imagem { get; set; }
        public string Extensao { get; set; }
        public bool? NacionalTec { get; set; }
    }
}
