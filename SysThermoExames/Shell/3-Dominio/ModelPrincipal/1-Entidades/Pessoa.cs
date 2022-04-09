using ModelPrincipal._2_Enumeradores;
using ModelPrincipal._3_Utilitarios;
using System;

namespace ModelPrincipal._1_Entidades
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

        public CPF CPF { get; set; }

        public Endereco Endereco { get; set; }

        public string Nascimento { get; set; }
    }
}
