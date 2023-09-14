using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.PagamentosVO
{
    public enum TipoRetornoCartaoCredito
    {
        OperacaoRealizadaComSuceso = 1,
        NaoAutorizada = 2,
        CartaoExpirado = 3,
        CartaoBloqueado = 4,
        TimeOut = 5,
        CartaoCancelado = 6,
        ProblemasComCartaoCredito = 7,
        TransacaoJaConfirmada = 8,
        ExceceuLimiteTransacoes = 9,
        ValidadeCartaoExpirada = 10,
        CodigoSegurancaInvalido = 11
    }
}
