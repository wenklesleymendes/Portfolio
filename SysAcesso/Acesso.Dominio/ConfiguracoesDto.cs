using System.Collections.Generic;

namespace Acesso.Dominio
{
    public class ConfiguracoesDto
    {
        public InformacaoConexao InformacaoConexao { get; set; }

        public List<Dispositivo> TodosDispositivos { get; set; }

        public Dispositivo Dispositivo { get; set; }

        public RegrasAcesso RegrasAcesso { get; set; }

        public List<Liberacao> Liberacoes { get; set; }

        public string TipoIntegracao { get; set; }

        public bool EhDesenvolvedor { get; set; }
    }
}
