using EscolaPro.Service.Dto.FuncionarioVO;
using EscolaPro.Service.Dto.UnidadeVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.AulaOnlineVO
{
    public class DtoAulaOnline
    {
        public int Id { get; set; }
        public string NomeAulaOnline { get; set; }
        public IEnumerable<DtoCursoOnline> Curso { get; set; }
        //public IEnumerable<DtoMateriaOnline> Materia { get; set; }
        public bool IsDelete { get; set; }
    }
}
