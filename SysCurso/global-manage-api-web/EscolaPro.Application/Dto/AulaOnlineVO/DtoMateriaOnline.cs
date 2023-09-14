using EscolaPro.Core.Model.AlunoQuestionarioProva;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.AulaOnlineVO
{
    public class DtoMateriaOnline
    {
        public int Id { get; set; }
        public string NomeMateria { get; set; }
        public string NomeCurso { get; set; }
        public IEnumerable<DtoVideoAula> VideoAula { get; set; }
        public int Ordenacao { get; set; }
    }

    public class DtoGridMateriaOnline
    {
        public int AulaOnlineId { get; set; }
        public string NomeAulaOnline { get; set; }
        public List<DtoMateriaOnline> MateriaOnline { get; set; }
    }
}
