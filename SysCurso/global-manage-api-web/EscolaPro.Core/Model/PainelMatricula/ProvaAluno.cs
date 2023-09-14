using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.Provas;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.PainelMatricula
{
    public class ProvaAluno : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public StatusProvaEnum StatusProva { get; set; }
        public int ColegioAutorizadoId { get; set; }
        public ColegioAutorizado ColegioAutorizado { get; set; }
        public TipoProvaEnum TipoProva { get; set; }
        public string LocalProva { get; set; }
        public DateTime? DataProva { get; set; }
        public DateTime? DataInscricao { get; set; }
        public TipoTransporteEnum? TipoTransporte { get; set; }
        public int MatriculaAlunoId { get; set; }
        public AgendaProva AgendaProva { get; set; }
        public int? AgendaProvaId { get; set; }
        public string Observacao { get; set; }
        public string IdentificacaoUsuario { get; set; }
        public string SenhaProva { get; set; }
        public int UsuarioLogadoId { get; set; }
        public int? UnidadeTransporteProvaId { get; set; }
        public UnidadeTransporteProva UnidadeTransporteProva { get; set; }
        public IEnumerable<ProvaMateriaAluno> ProvaMateriaAluno { get; set; }
    }
}
