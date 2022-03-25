using System;

namespace EMCatraca.Core.Dominio
{
    /// <summary>
    /// Dispositivo = Catraca
    /// </summary>
    [Serializable]
    public class Dispositivo
    {
        /// <value>Código=<see langword="1"/> E o número devidamente cadastrado no Firmware da catraca.</value>
        public int Codigo { get; set; }

        /// <value>IP=<see langword="192.168.12.001"/> Ip da rede do cliente devidamente cadastrado no Firmware da catraca.</value>
        public string IpCatraca { get; set; }

        /// <value>Porta=<see langword="3570"/> Porta padrão projeto TopData e no demais são livres sendo obrigatório informa uma porta.</value>
        public string PortaCatraca { get; set; }

        /// <value>Descrição=<see langword="Catraca 1"/> Descrição do nome e livre sem referência com Firmware.</value>
        public string Descricao { get; set; }

        /// <value>EhGiroInvertido=<see langword="false"/> Padrão e falso defini o giro do braço da catraca cadastro feito no Firmware.</value>
        public bool EhGiroNormal { get; set; }

        /// <value>EhGiroInvertido=<see langword="false"/> Padrão e falso defini o giro do braço da catraca cadastro feito no Firmware.</value>
        public bool EhGiroEntrada { get; set; }

        /// <value>EhGiroInvertido=<see langword="false"/> Padrão e falso defini o giro do braço da catraca cadastro feito no Firmware.</value>
        public bool EhGiroSaida { get; set; }

        /// <value>EhGiroInvertido=<see langword="false"/> Padrão e falso defini o giro do braço da catraca cadastro feito no Firmware.</value>
        public bool EhGiroInvertido { get; set; }
    }
}
