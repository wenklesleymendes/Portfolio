using EMCatraca.Core;
using EMCatraca.Core.Dominio;
using EMCatraca.Server.Excecoes;
using EMCatraca.Server.Interfaces;
using EscolarManager.Framework.Conexao;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace EMCatraca.Server.Repositorios.DB
{
    public class RepositorioAutorizadoBuscarAluno : IRepositorioAutorizadoBuscarAluno
    {
        private readonly IDBHelper _dbhelper;

        public RepositorioAutorizadoBuscarAluno(IDBHelper dbhelper)
        {
            _dbhelper = dbhelper;
        }

        public AutorizadoBuscarAluno ConsulteAutorizadoBuscarAluno(int codigo)
        {
            var sql = $@"SELECT pa.PEAUCODIGO, pa.PEAUNOME
                    FROM TBPESSOAAUTORIZADA pa 
                    INNER JOIN TBPESSOAAUTORIZADAALUNO paa ON paa.PEAACODIGO = pa.PEAUCODIGO 
                    INNER JOIN TBALUNO al ON al.ALUNMATRICULA = paa.PEAAALUNMATRICULA 
                    INNER JOIN TBALUNOMATRICULADO am ON am.ALMTALUNMATRICULA = al.ALUNMATRICULA
                    WHERE am.ALMTALUNMATRICULA = paa.PEAAALUNMATRICULA 
                    AND pa.PEAUCODIGO = {codigo}";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                var autorizadoBuscarAluno = new AutorizadoBuscarAluno
                {
                    Id = dr.GetInteger("PEAUCODIGO"),
                    Nome = dr.GetString("PEAUNOME"),
                    Foto = null,
                    TipoPessoa = TipoPessoa.AutorizadoBuscarAluno
                };
                return autorizadoBuscarAluno;
            }

            throw new AcessoNegadoException("Registro não encontrado!");
        }

        public IEnumerable<AutorizadoBuscarAluno> ConsulteAutorizadoBuscarAlunoAtivos()
        {
            var sql = $@"SELECT DISTINCT pa.PEAUCODIGO, pa.PEAUNOME
                        FROM TBPESSOAAUTORIZADA pa 
                        INNER JOIN TBPESSOAAUTORIZADAALUNO paa ON paa.PEAACODIGO = pa.PEAUCODIGO 
                        INNER JOIN TBALUNO a ON a.ALUNMATRICULA = paa.PEAAALUNMATRICULA 
                        INNER JOIN TBALUNOMATRICULADO m ON m.ALMTALUNMATRICULA = a.ALUNMATRICULA
                        WHERE m.ALMTALUNMATRICULA = paa.PEAAALUNMATRICULA
                        AND m.ALMTDTFIM = 99991231
                        AND m.ALMTSITUACAO =  1
                        AND m.ALMTANORANO = (SELECT MAX(ANORANO) FROM TBANOREF WHERE ANORATUAL = 1)
                        ORDER BY pa.PEAUNOME;";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                var autorizados = new List<AutorizadoBuscarAluno>();
                while (dr.Read())
                {
                    var autorizado = new AutorizadoBuscarAluno
                    {
                        Id = dr.GetInteger("PEAUCODIGO"),
                        Nome = dr.GetString("PEAUNOME"),
                        TipoPessoa = TipoPessoa.AutorizadoBuscarAluno
                    };
                    autorizados.Add(autorizado);
                }
                return autorizados;
            }

            throw new AcessoNegadoException("Autorizados não encontrados!");
        }

        public bool AutorizadoBuscarAlunoEstaBloqueado(int idAutorizado)
        {
            var sql = $@"SELECT 1
                    FROM TBPESSOAAUTORIZADAALUNO aut
                    INNER JOIN TBALUNO a ON aut.PEAAALUNMATRICULA = a.ALUNMATRICULA
                    INNER JOIN TBATRIBUTOADICIONALRESP res ON res.ATREIDCONCEITO = a.ALUNMATRICULA
                    INNER JOIN TBATRIBUTOADICIONAL att ON res.ATREIDATRIBUTO = att.ATADID
                    INNER JOIN TBATRIBUTOADICIONALLISTA lst ON res.ATREIDLISTA = lst.ATLIID
                    WHERE att.ATADNOME = 'BLOQUEAR ACESSO'
                    AND lst.ATLIVALOR = 'SIM'
                    AND aut.PEAACODIGO =   =  {idAutorizado}";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();
            return dr.Read();

        }

        public bool AutorizadoBuscarAlunoEstaAtivo(int idAutorizado)
        {
            var sql = $@"SELECT 1
                    FROM TBPESSOAAUTORIZADAALUNO paa
                    INNER JOIN TBALUNO al ON al.ALUNMATRICULA = paa.PEAAALUNMATRICULA 
                    INNER JOIN TBALUNOMATRICULADO am ON am.ALMTALUNMATRICULA = al.ALUNMATRICULA
                    WHERE am.ALMTALUNMATRICULA = paa.PEAAALUNMATRICULA
                    AND am.ALMTDTFIM = 99991231 
                    AND am.ALMTSITUACAO = 1 
                    AND am.ALMTANORANO = (SELECT MAX(ANORANO) FROM TBANOREF af WHERE ANORATUAL = 1) 
                    AND paa.PEAACODIGO =  {idAutorizado}";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();
            return dr.Read();
        }

        public bool AutorizadoBuscarAlunoEstaInadimplenteDeCheques(int idAutorizado)
        {
            var sql = $@"SELECT 1
                        FROM TBPESSOAAUTORIZADAALUNO aut
                        INNER JOIN TBALUNO a ON aut.PEAAALUNMATRICULA = a.ALUNMATRICULA
                        INNER JOIN TBCHEQUE c ON c.CHEQALUNMATRICULA = a.ALUNMATRICULA  
                        WHERE CHEQSITUACAO = 2
                        AND aut.PEAACODIGO = {idAutorizado}";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();
            return dr.Read();
        }

        public bool AutorizadoBuscarAlunoEstaInadimplenteDuplicata(int idAutorizado)
        {
            return EstaInadimplenteDuplicata(idAutorizado, 5);
        }

        public bool EstaInadimplenteDuplicata(int idAutorizado, int diasDeAtraso)
        {
            var sql = $@"SELECT 1
                        FROM TBPESSOAAUTORIZADAALUNO aut                                  
                        INNER JOIN TBALUNO a ON a.ALUNMATRICULA = aut.PEAAALUNMATRICULA
                        INNER JOIN TBDUPLICATAMATRICULA ON DPMAALUNMATRICULA = ALUNMATRICULA
                        INNER JOIN TBDUPLICATA  ON DPMACODDUP = DPTACODIGO
                        WHERE DPTADTVENCTO <= {DateTime.Now.AddDays(-1 * diasDeAtraso).DataSql()}
                        AND DPTASTATUS IN (0, 4)
                        AND aut.PEAACODIGO = {idAutorizado}";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();
            return dr.Read();

        }

        public bool AutorizadoBuscarAlunoEstaPendenteDeDocumento(int idAutorizado)
        {
            var sql = $@"SELECT 1
                        FROM TBPESSOAAUTORIZADAALUNO aut
                        INNER JOIN TBALUNO a ON aut.PEAAALUNMATRICULA = a.ALUNMATRICULA
                        INNER JOIN TBPENDENCIADOC d ON d.PEDOALUNMATRICULA = a.ALUNMATRICULA
                        WHERE aut.PEAACODIGO =  {idAutorizado}";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();
            return dr.Read();
        }

        public bool AutorizadoBuscarAlunoEstaPendenteDeMateriais(int idAutorizado)
        {
            var sql = $@"SELECT 1
                    FROM TBPESSOAAUTORIZADAALUNO aut
                    INNER JOIN TBALUNO a ON aut.PEAAALUNMATRICULA = a.ALUNMATRICULA
                    INNER JOIN TBPENDENCIAMAT m ON m.PDMAALUNMATRICULA = a.ALUNMATRICULA
                    WHERE aut.PEAACODIGO =  {idAutorizado}
                    AND PDMAANORANO = {DateTime.Now.AnoSql()}";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();
            return dr.Read();
        }
    }
}
