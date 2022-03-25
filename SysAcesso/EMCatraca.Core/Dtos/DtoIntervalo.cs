using System.Collections.Generic;

namespace EMCatraca.Core.Dtos
{
    public class DtoIntervalo
    {
        public int NumeroDia { get; set; }

        public string SemanaDescricao { get; set; }

        public string HoraInicial { get; set; }

        public string HoraFinal { get; set; }

        public string TipoAcesso { get; set; }

        public string ObtenhaDiaSemanaPeloNumero(int diaSemana)
        {
            var semanaSelecionada = string.Empty;

            switch (diaSemana)
            {
                case 0:

                    semanaSelecionada = "Domingo";
                    break;

                case 1:
                    semanaSelecionada = "Segunda-feira";
                    break;

                case 2:
                    semanaSelecionada = "Terça-feira";
                    break;

                case 3:
                    semanaSelecionada = "Quarta-feira";
                    break;

                case 4:
                    semanaSelecionada = "Quinta-feira";
                    break;

                case 5:
                    semanaSelecionada = "Sexta-feira";
                    break;

                case 6:
                    semanaSelecionada = "Sábado";
                    break;

                default:
                    break;
            }

            return semanaSelecionada;
        }
    }
}
