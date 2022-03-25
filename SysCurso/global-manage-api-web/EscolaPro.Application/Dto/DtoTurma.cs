using EscolaPro.Core.Model;
using EscolaPro.Core.Model.CursoTurma;
using EscolaPro.Service.Dto.UnidadeVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto
{
    public class DtoTurma
    {
        public int Id { get; set; }
        public bool Presencial { get; set; }
        public string Ano { get; set; }
        public string Semestre { get; set; }
        public PeriodoEnum? Periodo { get; set; }
        public string HorarioInicio { get; set; }
        public string HorarioTermino { get; set; }
        public SalaEnum? Sala { get; set; }
        public int? QuantidadeVagas { get; set; }
        public bool Disponivel { get; set; }
        public bool Segunda { get; set; }
        public bool Terca { get; set; }
        public bool Quarta { get; set; }
        public bool Quinta { get; set; }
        public bool Sexta { get; set; }
        public bool Sabado { get; set; }
        public bool Domingo { get; set; }
        public List<DtoCurso> Curso { get; set; }
        public List<DtoUnidadeTurma> Unidade { get; set; }
        public DateTime? InicioTurma { get; set; }
        public DateTime? TerminoTurma { get; set; }
    }

    public class DtoTransferirTurma
    {
        public int MatriculaId { get; set; }
        public int TurmaId { get; set; }
        public int CursoId { get; set; }
    }

    public class DtoTurmaFiltrar
    {
        public int UnidadeId { get; set; }
        public int CursoId { get; set; }
        public bool NacionalTec { get; set; }
        public bool Modalidade { get; set; }
        public int Ano { get; set; }
        public DateTime DataVigencia { get; set; }
        public PeriodoEnum Periodo { get; set; }
        public string DiaDaSemana { get; set; }
    }
}
