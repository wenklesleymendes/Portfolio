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
    public class RepositorioResponsavel : IRepositorioResponsavel
    {
        private readonly IDBHelper _dbhelper;

        public RepositorioResponsavel(IDBHelper dbhelper)
        {
            _dbhelper = dbhelper;
        }

        public Responsavel ConsulteResponsavel(int idResponsavel)
        {
            var sql = $@"SELECT r.REFICODIGO, r.REFINOME
                        FROM TBRESPFINANCEIROALUNO ar 
                        INNER JOIN TBRESPFINANCEIRO r ON ar.RFALREFICODIGO = r.REFICODIGO
                        INNER JOIN TBALUNO a ON a.ALUNMATRICULA = ar.RFALALUNMATRICULA
                        INNER JOIN  TBALUNOMATRICULADO m ON m.ALMTALUNMATRICULA = a.ALUNMATRICULA 
                        AND ar.RFALREFICODIGO = {idResponsavel}";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                var responsavel = new Responsavel
                {
                    Id = dr.GetInteger("REFICODIGO"),
                    Nome = dr.GetString("REFINOME"),
                    Foto = null,
                    TipoPessoa = TipoPessoa.Responsavel
                };
                return responsavel;
            }

            throw new AcessoNegadoException("Registro não encontrado!");
        }

        public IEnumerable<Responsavel> ConsulteTodosResponsavelAtivos()
        {
            var sql = $@"SELECT DISTINCT r.REFICODIGO, r.REFINOME
                            FROM TBRESPFINANCEIROALUNO ar 
                            INNER JOIN TBRESPFINANCEIRO r ON ar.RFALREFICODIGO = r.REFICODIGO
                            INNER JOIN TBALUNO a ON a.ALUNMATRICULA = ar.RFALALUNMATRICULA
                            INNER JOIN  TBALUNOMATRICULADO m ON m.ALMTALUNMATRICULA = a.ALUNMATRICULA 
	                        WHERE m.ALMTDTFIM = 99991231
                            AND m.ALMTSITUACAO =  1
                            AND m.ALMTANORANO = (SELECT MAX(ANORANO) FROM TBANOREF WHERE ANORATUAL = 1)
                            ORDER BY r.REFINOME;";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                var responsaveis = new List<Responsavel>();
                while (dr.Read())
                {
                    var responsavel = new Responsavel
                    {
                        Id = dr.GetInteger("REFICODIGO"),
                        Nome = dr.GetString("REFINOME"),
                        TipoPessoa = TipoPessoa.Responsavel
                    };
                    responsaveis.Add(responsavel);
                }
                return responsaveis;
            }

            throw new AcessoNegadoException("Responsáveis não encontrados!");
        }

        public bool ResponsavelEstaAtivo(int idResponsavel)
        {
            var sql = $@"SELECT 1
                        FROM TBRESPFINANCEIROALUNO rfa 
                        INNER JOIN TBRESPFINANCEIRO r ON rfa.RFALREFICODIGO = r.REFICODIGO
                        INNER JOIN TBALUNO a ON a.ALUNMATRICULA = rfa.RFALALUNMATRICULA
                        INNER JOIN  TBALUNOMATRICULADO m ON m.ALMTALUNMATRICULA = a.ALUNMATRICULA 
                        WHERE m.ALMTDTFIM = 99991231
                        AND m.ALMTSITUACAO =  1
                        AND m.ALMTANORANO = (SELECT MAX(ANORANO) FROM TBANOREF WHERE ANORATUAL = 1)
                        AND rfa.RFALREFICODIGO = {idResponsavel}";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();
            return dr.Read();

        }

        public bool ResponsavelEstaBloqueado(int idResponsavel)
        {
            var sql = $@"SELECT 1
                        FROM TBRESPFINANCEIROALUNO rfa
                        INNER JOIN TBALUNO a ON rfa.RFALALUNMATRICULA = a.ALUNMATRICULA
                        INNER JOIN TBATRIBUTOADICIONALRESP res ON res.ATREIDCONCEITO = a.ALUNMATRICULA
                        INNER JOIN TBATRIBUTOADICIONAL att ON res.ATREIDATRIBUTO = att.ATADID
                        INNER JOIN TBATRIBUTOADICIONALLISTA lst ON res.ATREIDLISTA = lst.ATLIID
                        WHERE att.ATADNOME = 'BLOQUEAR ACESSO'
                        AND lst.ATLIVALOR = 'SIM'
                        AND rfa.RFALREFICODIGO = {idResponsavel}";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();
            return dr.Read();

        }



        public bool ResponsavelEstaInadimplenteDeCheques(int idResponsavel)
        {
            var sql = $@"SELECT 1
                        FROM TBRESPFINANCEIROALUNO rfa
                        INNER JOIN TBALUNO a ON rfa.RFALALUNMATRICULA = a.ALUNMATRICULA
                        INNER JOIN TBCHEQUE c ON c.CHEQALUNMATRICULA = a.ALUNMATRICULA  
                        WHERE rfa.RFALREFICODIGO = {idResponsavel}
                        AND CHEQSITUACAO = 2";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();
            return dr.Read();

        }

        public bool ResponsavelEstaInadimplenteDuplicata(int idResponsavel)
        {
            return EstaInadimplenteDuplicata(idResponsavel, 5);
        }

        public bool EstaInadimplenteDuplicata(int idResponsavel, int diasDeAtraso)
        {
            var sql = $@"SELECT 1
                        FROM TBRESPFINANCEIROALUNO ar                                       
                        INNER JOIN TBRESPFINANCEIRO r ON ar.RFALREFICODIGO = r.REFICODIGO                                      
                        INNER JOIN TBALUNO a ON a.ALUNMATRICULA = ar.RFALALUNMATRICULA
                        INNER JOIN TBDUPLICATAMATRICULA ON DPMAALUNMATRICULA = ALUNMATRICULA
                        INNER JOIN TBDUPLICATA  ON DPMACODDUP = DPTACODIGO
                        WHERE DPTADTVENCTO <= {DateTime.Now.AddDays(-1 * diasDeAtraso).DataSql()}
                        AND DPTASTATUS IN (0, 4)
                        AND ar.RFALREFICODIGO = {idResponsavel}";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();
            return dr.Read();

        }

        public bool ResponsavelEstaPendenteDeDocumentos(int idResponsavel)
        {
            var sql = $@"SELECT 1
                        FROM TBRESPFINANCEIROALUNO rfa
                        INNER JOIN TBALUNO a ON rfa.RFALALUNMATRICULA = a.ALUNMATRICULA
                        INNER JOIN TBPENDENCIADOC d ON d.PEDOALUNMATRICULA = a.ALUNMATRICULA
                        WHERE rfa.RFALREFICODIGO = {idResponsavel}";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();
            return dr.Read();

        }

        public bool ResponsavelEstaPendenteDeMateriais(int idResponsavel)
        {
            var sql = $@"SELECT 1
                        FROM TBRESPFINANCEIROALUNO rfa
                        INNER JOIN TBALUNO a ON rfa.RFALALUNMATRICULA = a.ALUNMATRICULA
                        INNER JOIN TBPENDENCIAMAT m ON m.PDMAALUNMATRICULA = a.ALUNMATRICULA
                        WHERE rfa.RFALREFICODIGO = {idResponsavel}
                        AND PDMAANORANO = {DateTime.Now.AnoSql()}";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();
            return dr.Read();

        }
    }
}
