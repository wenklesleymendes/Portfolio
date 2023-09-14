using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.ControlePontoEletronico
{
    public enum TipoOcorrenciaPontoEnum
    {
        Falta = 1,
        Atestado = 2,
        Declaracao = 3,
        Atrasado = 4,
        HoraExtra = 5,
        Ferias = 6,
        DSR = 7,
        CorrecaoPontoEletronico = 8,
        Outros = 9
    }
}
