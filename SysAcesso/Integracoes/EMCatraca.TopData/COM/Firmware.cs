namespace EMCatraca.TopData
{
    public class Firmware
    {
        public Firmware()
        {
            Linha = 0;
            Variacao = 0;
            VersaoAlta = 0;
            VersaoBaixa = 0;
            VersaoSufixo = 0;
            InnerAcessoBio = 0;
            TipoModBio = 0;
        }
        public byte Linha { get; set; }
        public short Variacao { get; set; }
        public byte VersaoAlta { get; set; }
        public byte VersaoBaixa { get; set; }
        public byte VersaoSufixo { get; set; }
        public byte InnerAcessoBio { get; set; }
        public byte TipoModBio { get; set; }

    }
}
