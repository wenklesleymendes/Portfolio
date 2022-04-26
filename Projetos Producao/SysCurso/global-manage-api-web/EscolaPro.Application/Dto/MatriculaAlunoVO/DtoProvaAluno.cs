using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Core.Model.PainelMatricula.Enums;
using EscolaPro.Core.Model.Provas;
using EscolaPro.Service.Dto.AgendaProvaVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.MatriculaAlunoVO
{
    public class DtoProvaAluno
    {
        public int Id { get; set; }
        public StatusProvaEnum StatusProva { get; set; }
        public int ColegioAutorizadoId { get; set; }
        public DtoColegioAutorizado ColegioAutorizado { get; set; }
        public string LocalProva { get; set; }
        public TipoProvaEnum TipoProva { get; set; }
        public DateTime? DataProva { get; set; }
        public DateTime? DataInscricao { get; set; }
        public TipoTransporteEnum? TipoTransporte { get; set; }
        public int MatriculaAlunoId { get; set; }
        public int? AgendaProvaId { get; set; }
        public DtoAgendaProva AgendaProva { get; set; }
        public string Observacao { get; set; }
        public string IdentificacaoUsuario { get; set; }
        public string SenhaProva { get; set; }
        public int UsuarioLogadoId { get; set; }
        public int? UnidadeTransporteProvaId { get; set; }
        public DtoUnidadeTransporteProva UnidadeTransporteProva { get; set; }
        public string InscricaoProvaDocumento { get; set; }
        public IEnumerable<DtoProvaMateriaAluno> ProvaMateriaAluno { get; set; }
    }

    public class DtoProvaAlunoRequest
    {
        public int Id { get; set; }
        public StatusProvaEnum StatusProva { get; set; }
        public int ColegioAutorizadoId { get; set; }
        public string LocalProva { get; set; }
        public TipoProvaEnum TipoProva { get; set; }
        public DateTime? DataProva { get; set; }
        public DateTime? DataInscricao { get; set; }
        public TipoTransporteEnum? TipoTransporte { get; set; }
        public int MatriculaAlunoId { get; set; }
        public int? AgendaProvaId { get; set; }
        public string Observacao { get; set; }
        public string IdentificacaoUsuario { get; set; }
        public string SenhaProva { get; set; }
        public int UsuarioLogadoId { get; set; }
        public int? UnidadeTransporteProvaId { get; set; }
        public DtoUnidadeTransporteProva UnidadeTransporteProva { get; set; }
    }
}
