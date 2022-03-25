using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.Tickets
{
    public enum StatusTicketEnum
    {
        Todos = 0,
        Aberto = 1,
        Devolvido = 2,
        EmAtendimento = 3,
        Finalizado = 4,
        Atrasados = 5,
        Ocorrencia = 6
    }
}
