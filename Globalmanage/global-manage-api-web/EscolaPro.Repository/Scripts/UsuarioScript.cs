using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Repository.Scripts
{
    public static class UsuarioScript
    {
        public static string Login()
        {
            return @"select *
                from Usuario
                Where UserName = @UserName and Password = @Password";
        }

        public static string InserirUsuario()
        {
            return "Insert into Usuario" +
                    "(UserName,Password)" +
                    "Values" +
                    "(@UserName,@Password)" +
                    "Select @@Identity";
        }
        public static string ValidarUsuario()
        {
            return @"select 
                Convert(bit, (case when Count(*) > 0 then 1 else 0 end)) as Existente
                from Usuario 
                Where UserName = @UserName";
        }

        public static string Filtrar(int? funcionarioId, int? departamentoId, int? unidadeId)
        {
            string sqlQueryFiltro = @"select DISTINCT Id from Usuario where IsDelete = 0 and IsAluno = 0 ";


            if (funcionarioId.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and FuncionarioId = '{funcionarioId.Value}'";
            }

            if (departamentoId.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and DepartamentoId = '{departamentoId.Value}'" +
                                                  $" and IsActive = 1 ";
            }

            if (unidadeId.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $" and UnidadeId = '{unidadeId.Value}'";
            }

            return sqlQueryFiltro;
        }
    }
}
