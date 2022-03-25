using EMCatraca.Core.Dominio;

namespace EMCatraca.Server.Dtos
{
    public class DTORegistroAcesso
    {
        public int IdPessoa { get; set; }

        public string NomePessoa { get; set; }

        public TipoPessoa TipoPessoa { get; set; }

        public SentidoGiro SentidoDoGiro { get; set; }
    }
}
