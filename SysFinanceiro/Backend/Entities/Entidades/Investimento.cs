namespace Entities.Entidades
{
    public class Investimento
    {
        public decimal VF { get; set; }

        public decimal ValorLiquido { get; set; }

        public decimal VI { get; set; }

        public int Meses { get; set; }

        public decimal TB { get; private set; } = 1.8M;

        public decimal CDI { get; private set; } = 0.009M;

        public decimal Taxa { get; private set; } = 0.015M;

        public Investimento(decimal valorInicial, int meses)
        {
            if (valorInicial <= 0)
                throw new ArgumentException("O valor inicial deve ser positivo.", nameof(valorInicial));

            if (meses <= 1)
                throw new ArgumentException("O prazo deve ser maior que 1 mês.", nameof(meses));

            VI = valorInicial;
            Meses = meses;
        }

        public void CalculaValorFinal()
        {
            decimal valorFinal = VI;

            for (int i = 0; i < Meses; i++)
            {
                valorFinal = valorFinal * (1 + (CDI * TB));
            }

            VF = valorFinal;

            ValorLiquido = valorFinal * (1 - Taxa);
        }
    }
}
