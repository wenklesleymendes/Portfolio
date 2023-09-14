using System;

namespace EMCatraca.Core.Dominio
{
    [Serializable]
    public class InformacaoConexao
    {
        public string IP { get; set; }
        public string PortaTcpIp { get; set; }
        public string Conexao { get; set; }
        public bool EhWebAPI { get; set; }
    }
}
