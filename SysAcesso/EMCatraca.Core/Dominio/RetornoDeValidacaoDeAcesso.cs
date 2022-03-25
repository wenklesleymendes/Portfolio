using EMCatraca.Core.Dominio;

namespace ValidacaoExterna.Neokoros
{
    public class RetornoDeValidacaoDeAcesso
    {
        public string Mensagem1 { get; set; }

        public string Mensagem2 { get; set; }

        public AcessoEsperado AcessoEsperado { get; set; }

        public bool Sucesso { get; set; }

        public int GiroId { get; set; }
    }
}
