using EMCatraca.Core;
using EMCatraca.Core.Dominio;
using EMCatraca.Core.Logs;
using EMCatraca.Server.Excecoes;
using EMCatraca.Server.Interfaces;
using EscolarManager.Framework.Conexao;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace EMCatraca.Server.Repositorios.DB
{
    public class RepositorioAluno : IRepositorioAluno
    {
        private readonly IDBHelper _dbhelper;

        public RepositorioAluno(IDBHelper dbhelper)
        {
            _dbhelper = dbhelper;
        }

        public Aluno ConsulteAluno(int codigo)
        {
            var sql = $@"SELECT a.ALUNMATRICULA, a.ALUNNOME, i.TIMGIMAGEM
                        FROM TBALUNO a LEFT JOIN TBIMAGEM i
                        ON a.ALUNFOTO = i.TIMGID 
                        WHERE a.ALUNMATRICULA = {codigo}";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                var aluno = new Aluno();
                aluno.Id = dr.GetInteger("ALUNMATRICULA");
                aluno.Nome = dr.GetString("ALUNNOME");
                aluno.Foto = dr.GetBytes("TIMGIMAGEM");
                aluno.TipoPessoa = TipoPessoa.Aluno;
                return aluno;
            }


            throw new AcessoNegadoException("Registro não encontrado!");
        }

        public string ObtenhaMatriculaPorRFID(string codigo)
        {
            LogAuditoria.Escreva($"{nameof(ObtenhaMatriculaPorRFID)}: " +
                   $"Codigo do RFID={nameof(codigo)}={codigo}",
                   $"{nameof(RepositorioAluno)}");

            var sql = $@"SELECT ALUNMATRICULA, ALUNCODFRID FROM TBALUNO WHERE ALUNCODFRID = {codigo}";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                string matricula = dr.GetInteger("ALUNMATRICULA").ToString();

                LogAuditoria.Escreva($"dr.GetInteger: " +
                   $"Matricula={matricula}",
                   $"{nameof(RepositorioAluno)}");

                return dr.GetInteger("ALUNMATRICULA").ToString();
            }

            string menssagen = "Registro não encontrado!";

            LogAuditoria.Escreva($"{nameof(ObtenhaMatriculaPorRFID)}: " +
                   $"Aluno com Codigo RFID={codigo}2.l{menssagen}",
                   $"{nameof(RepositorioAluno)}");

            throw new AcessoNegadoException(menssagen);
        }

        public IEnumerable<Aluno> ConsulteAlunosAtivos()
        {
            var sql = $@"SELECT a.ALUNMATRICULA, a.ALUNNOME
                        FROM TBALUNO a RIGHT JOIN TBALUNOMATRICULADO m
                        ON a.ALUNMATRICULA = m.ALMTALUNMATRICULA 
                        WHERE m.ALMTDTFIM = 99991231
                        AND m.ALMTSITUACAO =  1
                        AND m.ALMTANORANO = (SELECT MAX(ANORANO) FROM TBANOREF WHERE ANORATUAL = 1)
                        ORDER BY a.ALUNNOME;";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();

            var alunos = new List<Aluno>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Aluno aluno = new Aluno
                    {
                        Id = dr.GetInteger("ALUNMATRICULA"),
                        Nome = dr.GetString("ALUNNOME"),
                        TipoPessoa = TipoPessoa.Aluno
                    };
                    alunos.Add(aluno);
                }
                return alunos;
            }

            throw new AcessoNegadoException("Registro não encontrado!");
        }

        public bool AlunoEstaAtivo(int idAluno)
        {
            var sql = $@"SELECT 1
                        FROM TBALUNOMATRICULADO
                        WHERE ALMTALUNMATRICULA = {idAluno}
                        AND ALMTDTFIM = 99991231
                        AND ALMTSITUACAO =  1
                        AND ALMTANORANO = (SELECT MAX(ANORANO) FROM TBANOREF WHERE ANORATUAL = 1)";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();

            return dr.Read();
        }

        public bool AlunoEstaBloqueado(int idAluno, int idAtributo)
        {
            var sql = $@"SELECT 1
                        FROM TBATRIBUTOADICIONALRESP res
                        INNER JOIN TBATRIBUTOADICIONAL att ON res.ATREIDATRIBUTO = att.ATADID
                        INNER JOIN TBATRIBUTOADICIONALLISTA lst ON res.ATREIDLISTA = lst.ATLIID
                        WHERE att.ATADID = {idAtributo}
                        AND UPPER(lst.ATLIVALOR) = 'SIM'
                        AND res.ATREIDCONCEITO = {idAluno}";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();

            return dr.Read();
        }

        public bool AlunoEstaInadimplenteDuplicata(int idAluno)
        {
            return AlunoEstaInadimplenteDuplicata(idAluno, 5);
        }

        public bool AlunoEstaInadimplenteDuplicata(int idAluno, int diasDeAtraso)
        {
            var sql = $@"SELECT 1
                        FROM TBDUPLICATA
                        INNER JOIN TBDUPLICATAMATRICULA ON DPMACODDUP = DPTACODIGO
                        WHERE DPTADTVENCTO <= {DateTime.Now.AddDays(-1 * diasDeAtraso).DataSql()}
                        AND DPTASTATUS IN (0, 4)
                        AND DPMAALUNMATRICULA = {idAluno}";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();
            return dr.Read();
        }

        public bool AlunoEstaInadimplenteDeCheques(int idAluno)
        {
            var sql = $@"SELECT 1 
                        FROM TBCHEQUE  
                        INNER JOIN TBALUNO ON CHEQALUNMATRICULA = ALUNMATRICULA    
                        WHERE CHEQALUNMATRICULA = {idAluno}
                        AND CHEQSITUACAO = 2";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();
            return dr.Read();
        }

        public bool AlunoEstaPendenteDeDocumentos(int idAluno)
        {
            var sql = $@"SELECT 1
                        FROM TBPENDENCIADOC
                        WHERE PEDOALUNMATRICULA = {idAluno}";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();
            return dr.Read();
        }

        public bool AlunoEstaPendenteDeMateriais(int idAluno)
        {
            var sql = $@"SELECT 1
                        FROM TBPENDENCIAMAT 
                        WHERE PDMAALUNMATRICULA = {idAluno} 
                        AND PDMAANORANO = {DateTime.Now.AnoSql()}";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();
            return dr.Read();
        }

        public SerieTurma ConsulteSerieTurmaPorAluno(int idAluno)
        {
            var sql = $@"SELECT s.SERICODIGO, s.SERIDESCRICAO, t.TURMCODIGO, t.TURMDESCRICAO
                        FROM TBALUNOMATRICULADO m
                        INNER JOIN TBTURMA t ON m.ALMTTURMCODIGO = t.TURMCODIGO
                        INNER JOIN TBSERIE s ON m.ALMTSERICODIGO = s.SERICODIGO
                        WHERE m.ALMTALUNMATRICULA = {idAluno} 
                        AND m.ALMTDTFIM = 99991231
                        AND m.ALMTSITUACAO =  1
                        AND m.ALMTANORANO = (SELECT MAX(ANORANO) FROM TBANOREF WHERE ANORATUAL = 1)";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                var serieTurma = new SerieTurma();
                serieTurma.Serie.Codigo = dr.GetInteger("SERICODIGO");
                serieTurma.Serie.Descricao = dr.GetString("SERIDESCRICAO");
                serieTurma.Turma.Codigo = dr.GetInteger("TURMCODIGO");
                serieTurma.Turma.Descricao = dr.GetString("TURMDESCRICAO");

                return serieTurma;
            }

            return null;
        }

        public TurmaMontada ConsulteTurmaMontadaPorAluno(int idAluno)
        {
            var sql = $@"SELECT s.SERICODIGO, s.SERIDESCRICAO, t.TURMCODIGO, t.TURMDESCRICAO
                        FROM TBALUNOMATRICULADO m
                        INNER JOIN TBTURMA t ON m.ALMTTURMCODIGO = t.TURMCODIGO
                        INNER JOIN TBSERIE s ON m.ALMTSERICODIGO = s.SERICODIGO
                        WHERE m.ALMTALUNMATRICULA = {idAluno} 
                        AND m.ALMTDTFIM = 99991231";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                var turmaMontada = new TurmaMontada
                {
                    SerieCodigo = dr.GetInteger("SERICODIGO"),
                    SerieDescricao = dr.GetString("SERIDESCRICAO"),
                    TurmaCodigo = dr.GetInteger("TURMCODIGO"),
                    TurmaDescricao = dr.GetString("TURMDESCRICAO")
                };

                return turmaMontada;
            }
            return null;
        }

        public IEnumerable<Aluno> ConsulteAlunosPorTurmaMontada(int codigoDaSerie, int codigoDaTurma)
        {
            var alunos = new List<Aluno>();

            var turma = "";
            if (codigoDaTurma > 0)
            {
                turma = $"AND m.ALMTTURMCODIGO = {codigoDaTurma}";
            }

            var sql = $@"SELECT a.ALUNMATRICULA, a.ALUNNOME FROM TBALUNOMATRICULADO m
                        INNER JOIN TBALUNO a ON a.ALUNMATRICULA = m.ALMTALUNMATRICULA
                        WHERE m.ALMTSERICODIGO = {codigoDaSerie}
                        {turma}
                        AND m.ALMTDTFIM = 99991231
                        AND m.ALMTSITUACAO =  1
                        AND m.ALMTANORANO = (SELECT MAX(ANORANO) FROM TBANOREF WHERE ANORATUAL = 1)
                        ORDER BY a.ALUNNOME";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();


            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    var aluno = new Aluno
                    {
                        Id = dr.GetInteger("ALUNMATRICULA"),
                        Nome = dr.GetString("ALUNNOME"),
                        TipoPessoa = TipoPessoa.Aluno
                    };
                    alunos.Add(aluno);
                }
                return alunos;
            }

            throw new AcessoNegadoException("Registro não encontrado!");
        }

        public bool AlunoPodeSairSozinho(int idAluno, int idAtributo)
        {
            var sql = $@"SELECT 1
                        FROM TBATRIBUTOADICIONALRESP res
                        INNER JOIN TBATRIBUTOADICIONAL att ON res.ATREIDATRIBUTO = att.ATADID
                        INNER JOIN TBATRIBUTOADICIONALLISTA lst ON res.ATREIDLISTA = lst.ATLIID
                        WHERE att.ATADID = {idAtributo}
                        AND (UPPER(lst.ATLIVALOR) = 'NAO' OR UPPER(lst.ATLIVALOR) = 'NÃO')
                        AND res.ATREIDCONCEITO = {idAluno}";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                return new RepositorioAcessoPessoa(_dbhelper).ObtenhaTipoDeAcessoDaPessoa(idAluno) == SentidoGiro.Entrada;
            }

            return true;
        }

        public bool AlunoPodeSerLiberadoPeloResponsavel(int idAluno, int codigoAutorizacao)
        {
            if (codigoAutorizacao == 0) return false;

            var sql = $@"SELECT 1
                        FROM TBRESPFINANCEIROALUNO ar
                        WHERE ar.RFALALUNMATRICULA = {idAluno}
                        AND ar.RFALREFICODIGO = {codigoAutorizacao}";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();
            return dr.Read();
        }

        public bool AlunoPodeSerLiberadoPeloAutorizado(int idAluno, int codigoAutorizacao)
        {
            if (codigoAutorizacao == 0) return false;

            var sql = $@"SELECT 1
                        FROM TBPESSOAAUTORIZADA pa
                        INNER JOIN TBPESSOAAUTORIZADAALUNO paa ON paa.PEAACODIGO = pa.PEAUCODIGO
                        WHERE paa.PEAAALUNMATRICULA = {idAluno}
                        AND paa.PEAACODIGO =  {codigoAutorizacao}";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();
            return dr.Read();
        }
    }
}
