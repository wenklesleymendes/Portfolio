using EMCatraca.Core.Dominio;
using EMCatraca.Server.Excecoes;
using EMCatraca.Server.Interfaces;
using EscolarManager.Framework.Conexao;
using System.Collections.Generic;
using System.Data.Common;

namespace EMCatraca.Server.Repositorios.DB
{
    public class RepositorioColaborador : IRepositorioColaborador
    {
        private readonly IDBHelper _dbhelper;

        public RepositorioColaborador(IDBHelper dbhelper)
        {
            _dbhelper = dbhelper;
        }

        public Colaborador ConsulteColaborador(int idColaborador)
        {
            var sql = $@"SELECT o.PRFICODIGO, o.PRFINOME, i.TIMGIMAGEM 
                            FROM TBPROFISSIONAL o LEFT JOIN TBIMAGEM i
                            ON o.PRFIFOTO = i.TIMGID
                            WHERE o.PRFICODIGO = {idColaborador}";


            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                var colaborador = new Colaborador
                {
                    Id = dr.GetInteger("PRFICODIGO"),
                    Nome = dr.GetString("PRFINOME"),
                    Foto = dr.GetBytes("TIMGIMAGEM"),
                    TipoPessoa = TipoPessoa.Profissional
                };

                return colaborador;
            }

            throw new AcessoNegadoException("Registro não encontrado!");
        }

        public IEnumerable<Colaborador> ConsulteTodosColaboradorAtivos()
        {
            var sql = $@"SELECT PRFICODIGO, PRFINOME 
                        FROM TBPROFISSIONAL 
                        WHERE PRFIATIVO='S'
                        ORDER BY PRFINOME";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                var colaboradores = new List<Colaborador>();
                while (dr.Read())
                {
                    var colaborador = new Colaborador
                    {
                        Id = dr.GetInteger("PRFICODIGO"),
                        Nome = dr.GetString("PRFINOME"),
                        TipoPessoa = TipoPessoa.Profissional
                    };
                    colaboradores.Add(colaborador);
                }
                return colaboradores;
            }

            throw new AcessoNegadoException("Colaborador não encontrado!");
        }

        public bool ColaboradorEstaAtivo(int idColaborador)
        {
            var sql = $@"SELECT 1
                        FROM TBPROFISSIONAL
                        WHERE PRFIATIVO = 'S'
                        AND PRFICODIGO = {idColaborador}";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();
            return dr.Read();
        }
    }
}
