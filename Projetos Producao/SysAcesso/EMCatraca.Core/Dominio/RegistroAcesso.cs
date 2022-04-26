namespace EMCatraca.Core.Dominio
{
    public class RegistroAcesso
    {
        public int IdPessoa { get; set; }
        public int CodigoPessoaAutorizou { get; set; }
        public int TipoPessoaAutorizou { get; set; }
        public int SentidoDoGiro { get; set; }
    }
}