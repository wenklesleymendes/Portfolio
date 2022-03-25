using EscolaPro.Core.Model.Provas;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.AgendaProvaVO
{
    public class DtoAgendaGrid
    {
        public int Id { get; set; }
        public TipoProvaEnum TipoProva  { get; set; }
        public string Unidade { get; set; }
        public DateTime? DataInicioInscricao { get; set; }
        public DateTime? DataTerminoInscricao { get; set; }
        public DateTime? DataProva { get; set; }
        public int QuantidadeVagas { get; set; }

    }
}
