using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto
{
    public class AppSettings
    {
        public bool HomologItau { get; set; }
        public bool EnviarEmail { get; set; }
        public string BoletoServiceUrl { get; set; }

        public int SolicitacaoId { get; set; }
        public int AssuntoTicketAnaliseDoc { get; set; }
        public int UsuarioLogadoTesteProcessos { get; set; }
        public int[] UsuariosBaixaManual { get; set; }
        public int TimePauseWhatsApp { get; set; }
    }
}
