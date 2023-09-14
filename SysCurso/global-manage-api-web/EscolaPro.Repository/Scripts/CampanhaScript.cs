using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Repository.Scripts
{
    public class CampanhaScript
    {
        public static string Filtrar(int unidadeId, int cursoId, int tipoPagamento)
        {
            string sqlQueryFiltro = @"select DISTINCT campanha.ID from Campanha campanha
                                        inner join CampanhaUnidade campanhaUnidade
                                        on campanha.Id = campanhaUnidade.CampanhaId
                                        inner join CampanhaTipoPagamento campanhaTipoPagamento
                                        on campanha.Id = campanhaTipoPagamento.CampanhaId
                                        where campanha.IsDelete = 0"+
                                        $" and campanha.CursoId = '{cursoId}'"+
                                        $" and campanhaTipoPagamento.TipoPagamento = '{tipoPagamento}'"+
                                        $" and campanhaUnidade.UnidadeId = '{unidadeId}' " +
                                        $" and campanha.TerminoCampanha >= GETDATE()";

            return sqlQueryFiltro;
        }
    }
}
