using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.Enums
{
    public enum SituacaAgendamento
    {
        NaoConfirmado = 0,
        Confirmado = 1,
        CaixaPostal = 2,
        NaoAtende = 3,
        EstaIndeciso = 4,
        ParaOutraPessoa = 5,
        ConfirmarcomTerceiros = 6,
        Reagendou = 7,
        TelefoneIncorretoNaoExiste = 8,
        JaMatriculadoemNossaEscola = 9,
        RealizouMatriculaemOutraEscola = 10,
        NaoTemMaisInteresse = 11,
        NaoTemIdadeParaPatricula = 12,        
        Desempregado = 13,
        SemCondiçõesFinanceiras = 14,
        Outros = 15
    }
}
