using EMCatraca.Core.Dominio;
using EMCatraca.Server.Excecoes;
using EMCatraca.Server.Interfaces;
using EscolarManager.Framework.Conexao;
using System.Collections.Generic;
using System.Data.Common;

namespace EMCatraca.Server.Repositorios.DB
{
    public class RepositorioProfessor : IRepositorioProfessor
    {
        private readonly IDBHelper _dbhelper;

        public RepositorioProfessor(IDBHelper dbhelper) 
        {
            _dbhelper = dbhelper;
        }

        public Professor ConsulteProfessor(int idProfessor)
        {
            var sql = $@"SELECT p.PROFCODIGO, p.PROFNOME, i.TIMGIMAGEM
                        FROM TBPROFESSOR p LEFT JOIN TBIMAGEM i
                        ON p.PROFFOTO = i.TIMGID
                        WHERE p.PROFCODIGO = {idProfessor}";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                var professor = new Professor
                {
                    Id = dr.GetInteger("PROFCODIGO"),
                    Nome = dr.GetString("PROFNOME"),
                    Foto = dr.GetBytes("TIMGIMAGEM"),
                    TipoPessoa = TipoPessoa.Professor
                };

                return professor;
            }

            throw new AcessoNegadoException("Registro não encontrado!");
        }

        public IEnumerable<Professor> ConsulteTodosProfessorAtivos()
        {
            var sql = $@"SELECT PROFCODIGO, PROFNOME 
                        FROM TBPROFESSOR 
                        WHERE PROFATIVO = 'S' 
                        ORDER BY PROFNOME;";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                var professores = new List<Professor>();
                while (dr.Read())
                {
                    var professor = new Professor
                    {
                        Id = dr.GetInteger("PROFCODIGO"),
                        Nome = dr.GetString("PROFNOME"),
                        TipoPessoa = TipoPessoa.Professor
                    };
                    professores.Add(professor);
                }
                return professores;
            }

            throw new AcessoNegadoException("Professores não encontrados!");
        }

        public bool ProfessorEstaAtivo(int idProfessor)
        {
            var sql = $@"SELECT 1
                        FROM TBPROFESSOR
                        WHERE PROFATIVO='S'
                        AND PROFCODIGO = {idProfessor}";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();
            return dr.Read();
        }
    }
}
