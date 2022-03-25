using System.Collections.Generic;

namespace EMCatraca.Simuladores.Entity
{
    class Horarios
    {
        public byte Codigo { get; set; }
        public byte Faixa { get; set; }
        public byte Dia { get; set; }
        public byte Hora { get; set; }
        public byte Minuto { get; set; }
        public byte Horario { get; set; }

        public static List<Horarios> MontarListaHorarios()
        {
            List<Horarios> ListaHorarios = new List<Horarios>();

            //Horario 1, dia 1 = segunda
            ListaHorarios.Add(new Horarios() { Horario = 1, Dia = 1, Faixa = 1, Hora = 8, Minuto = 0 });
            ListaHorarios.Add(new Horarios() { Horario = 1, Dia = 1, Faixa = 2, Hora = 12, Minuto = 0 });
            ListaHorarios.Add(new Horarios() { Horario = 1, Dia = 1, Faixa = 3, Hora = 13, Minuto = 0 });
            ListaHorarios.Add(new Horarios() { Horario = 1, Dia = 1, Faixa = 4, Hora = 18, Minuto = 0 });

            //Horario 1, dia 2 = terça
            ListaHorarios.Add(new Horarios() { Horario = 1, Dia = 2, Faixa = 1, Hora = 8, Minuto = 0 });
            ListaHorarios.Add(new Horarios() { Horario = 1, Dia = 2, Faixa = 2, Hora = 12, Minuto = 0 });
            ListaHorarios.Add(new Horarios() { Horario = 1, Dia = 2, Faixa = 3, Hora = 13, Minuto = 0 });
            ListaHorarios.Add(new Horarios() { Horario = 1, Dia = 2, Faixa = 4, Hora = 18, Minuto = 0 });

            //Horario 1, dia 3 = quarta
            ListaHorarios.Add(new Horarios() { Horario = 1, Dia = 3, Faixa = 1, Hora = 8, Minuto = 0 });
            ListaHorarios.Add(new Horarios() { Horario = 1, Dia = 3, Faixa = 2, Hora = 12, Minuto = 0 });
            ListaHorarios.Add(new Horarios() { Horario = 1, Dia = 3, Faixa = 3, Hora = 13, Minuto = 0 });
            ListaHorarios.Add(new Horarios() { Horario = 1, Dia = 3, Faixa = 4, Hora = 18, Minuto = 0 });

            //Horario 1, dia 4 = quinta
            ListaHorarios.Add(new Horarios() { Horario = 1, Dia = 4, Faixa = 1, Hora = 8, Minuto = 0 });
            ListaHorarios.Add(new Horarios() { Horario = 1, Dia = 4, Faixa = 2, Hora = 12, Minuto = 0 });
            ListaHorarios.Add(new Horarios() { Horario = 1, Dia = 4, Faixa = 3, Hora = 13, Minuto = 0 });
            ListaHorarios.Add(new Horarios() { Horario = 1, Dia = 4, Faixa = 4, Hora = 18, Minuto = 0 });

            //Horario 1, dia 5 = sexta
            ListaHorarios.Add(new Horarios() { Horario = 1, Dia = 5, Faixa = 1, Hora = 8, Minuto = 0 });
            ListaHorarios.Add(new Horarios() { Horario = 1, Dia = 5, Faixa = 2, Hora = 12, Minuto = 0 });
            ListaHorarios.Add(new Horarios() { Horario = 1, Dia = 5, Faixa = 3, Hora = 13, Minuto = 0 });
            ListaHorarios.Add(new Horarios() { Horario = 1, Dia = 5, Faixa = 4, Hora = 18, Minuto = 0 });

            //Horario 1, dia 6 = sabado
            ListaHorarios.Add(new Horarios() { Horario = 1, Dia = 6, Faixa = 1, Hora = 8, Minuto = 0 });
            ListaHorarios.Add(new Horarios() { Horario = 1, Dia = 6, Faixa = 2, Hora = 12, Minuto = 0 });
            ListaHorarios.Add(new Horarios() { Horario = 1, Dia = 6, Faixa = 3, Hora = 13, Minuto = 0 });
            ListaHorarios.Add(new Horarios() { Horario = 1, Dia = 6, Faixa = 4, Hora = 18, Minuto = 0 });

            //Horario 1, dia 7 = domingo
            ListaHorarios.Add(new Horarios() { Horario = 1, Dia = 7, Faixa = 1, Hora = 8, Minuto = 0 });
            ListaHorarios.Add(new Horarios() { Horario = 1, Dia = 7, Faixa = 2, Hora = 12, Minuto = 0 });
            ListaHorarios.Add(new Horarios() { Horario = 1, Dia = 7, Faixa = 3, Hora = 13, Minuto = 0 });
            ListaHorarios.Add(new Horarios() { Horario = 1, Dia = 7, Faixa = 4, Hora = 18, Minuto = 0 });
            return ListaHorarios;
        }
    }
}
