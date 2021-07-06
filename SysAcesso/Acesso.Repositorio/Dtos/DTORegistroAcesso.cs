using  Acesso.Dominio;

namespace Acesso.Interfaces.Dtos
{
    public class DTORegistroAcesso
    {
        public int IdPessoa { get; set; }

        public string NomePessoa { get; set; }

        public TipoPessoa TipoPessoa { get; set; }

        public SentidoGiro SentidoDoGiro { get; set; }
    }
}
