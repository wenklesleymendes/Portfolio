using System;

namespace Acesso.Dominio
{
    [Serializable]
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public byte[] Foto { get; set; }
        public TipoPessoa TipoPessoa { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Pessoa pessoa && Id == pessoa.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }

        public override string ToString()
        {
            return Nome;
        }
    }
}
