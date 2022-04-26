using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.AulaOnlineVO
{
    public class DtoVideoPausado
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public int MatriculaId { get; set; }
        public int VideoId { get; set; }
        public float Tempo { get; set; }
        public DateTime DataUltimaVisualizacao { get; set; }
    }
}
