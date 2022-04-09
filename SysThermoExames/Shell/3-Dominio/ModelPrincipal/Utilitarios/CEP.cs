namespace ModelPrincipal.Utilitarios
{
    public class CEP
    {
        public CEP(string Cep)
        {
            this.cep = Cep;
        }

        public string cep { get; set; }

        public override string ToString()
        {
            return cep ?? "";
        }
    }
}
