using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.ControlePontoEletronico
{
    public enum TipoFeriasFolgaFalta
    {
        Folga = 1,
        Falta = 2,
        FeriasGozadas30Dias = 3, //Férias Gozadas 30 dias de Descanso
        FeriasVendidas30Dias = 4, //Férias Vencidas 30 dias
        FeriasGozadas15Dias15Vendidos = 5, //Férias Gozadas 15 dias de Descanso + 15 dias vendidos
        FeriasGozadas20Dias10Vendidos = 6, // Férias Gozadas 20 dias de Descanso + 10 dias vendidos
    }
}
