using ModelPrincipal.Enumeradores;
using ModelPrincipal.Utilitarios;

namespace ModelPrincipal.Entidades
{
    public class Pessoa
    {
        public int Codigo { get; set; }

        public string Nome { get; set; }

        public string NomeMeio { get; set; }

        public string Sobrenome { get; set; }

        public int CodigoSexo { get; set; }

        public EnumSexo Sexo { get; set; }

        public int CodigoEtnia { get; set; }

        public EnumEtnia Etnia { get; set; }

        public string CPF { get; set; }

        public Endereco Endereco { get; set; }

        public string Nascimento { get; set; }
    }
}
