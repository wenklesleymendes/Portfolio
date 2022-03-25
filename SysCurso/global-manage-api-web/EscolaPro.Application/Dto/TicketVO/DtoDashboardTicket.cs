using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.TicketVO
{
    public class DtoDashboardTicket
    {
        public int TotalOcorrencias { get; set; }
        public int Resolvidos { get; set; }
        public int Abertos { get; set; }
        public int Atrasados { get; set; }
    }
}
