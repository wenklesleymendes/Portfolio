using EscolaPro.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Repository.Scripts
{
    public static class TurmaScript
    {
        public static string Filtrar(int cursoId, int unidadeId, int? usuarioLogadoId)
        {
            string sqlQueryFiltro = @"select DISTINCT turma.Id, turmaUnidade.UnidadeId  from Turma
                                        inner join TurmaCurso turmaCurso
                                        on turma.Id = turmaCurso.TurmaId
                                        inner join TurmaUnidade turmaUnidade
                                        on turma.Id = turmaUnidade.TurmaId 
                                        inner join Usuario usuariologado
                                        on usuariologado.UnidadeId = turmaUnidade.UnidadeId
                                        where turma.IsDelete = 0 and turma.disponivel = 1 " +
                                        $"and turmaCurso.CursoId = '{cursoId}' ";

            if(usuarioLogadoId != -1)
            {
                sqlQueryFiltro += @$"and usuariologado.Id = '{usuarioLogadoId.Value}'";
            }

            return sqlQueryFiltro;
        }
    }
}
