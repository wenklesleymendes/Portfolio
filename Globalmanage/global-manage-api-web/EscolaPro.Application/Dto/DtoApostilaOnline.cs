using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto
{
    public class DtoApostilaOnline
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public int MateriaId { get; set; }
        public int CursoId { get; set; }
        public string NomeApostila { get; set; }
    }
}
