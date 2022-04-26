using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Repository.Scripts
{
    public class AgendaProvaScript
    {
        public static string Filtrar(int unidadeId)
        {
            string sqlQueryFiltro = @"select DISTINCT agenda.Id from AgendaProva agenda
inner join UnidadeParticipanteProva unidadeParticipante
on agenda.Id = unidadeParticipante.AgendaProvaId
where agenda.IsDelete = 0
and TerminoInscricao <= GETDATE()
and unidadeParticipante.UnidadeId = '"+unidadeId+"'";


            //if (filtrarAluno.UnidadeId.HasValue)
            //{
            //    sqlQueryFiltro = sqlQueryFiltro + $"and aluno.UnidadeId = '{filtrarAluno.UnidadeId}'";
            //}

            //if (!string.IsNullOrEmpty(filtrarAluno.Nome))
            //{
            //    sqlQueryFiltro = sqlQueryFiltro + $"and aluno.Nome like '%{filtrarAluno.Nome}%'";
            //}

            return sqlQueryFiltro;
        }
    }
}
