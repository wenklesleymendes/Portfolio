namespace ModelPrincipal.Utilitarios
{
    public class Endereco
    {
        public string Logradouro { get; set; }

        public int? Numero { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public Cidade Cidade { get; set; }

        public CEP Cep { get; set; }
    }
}
