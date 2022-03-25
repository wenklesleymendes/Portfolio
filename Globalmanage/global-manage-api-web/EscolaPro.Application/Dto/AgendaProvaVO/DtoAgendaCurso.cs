using EscolaPro.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.AgendaProvaVO
{
    public class DtoAgendaCurso
    {
        public int Id { get; set; }
        public DtoCurso Curso { get; set; }
        public int CursoId { get; set; }
        public int AgendaProvaId { get; set; }
    }
}
