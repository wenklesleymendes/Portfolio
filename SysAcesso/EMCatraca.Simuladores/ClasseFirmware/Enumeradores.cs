namespace EMCatraca.Simuladores.ClasseFirmware
{
    public class Enumeradores
    {
        public enum EnumeradorEstadoDaCatraca
        {
            Conectando = 1,
            Executando = 2,
            Validacao = 3,
            Negado = 4,
            Aguardando = 5,
            Girou = 6,
            Parado = 7
        }

        public enum AcessoCatraca
        {
            Negado = 1,
            Liberado = 2,
            LiberadoComRestricoes = 3,
            MudancaEstado = 4
        }
        public enum SentidoDoGiro
        {
            Entrada = 1,
            Saida = 2,
            Indefinido = 3
        }
    }
}
