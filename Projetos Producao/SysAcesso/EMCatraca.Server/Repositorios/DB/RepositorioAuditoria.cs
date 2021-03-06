using EMCatraca.Core.Dominio;
using EMCatraca.Server.Interfaces;
using EscolarManager.Framework.Conexao;
using System;

namespace EMCatraca.Server.Repositorios.DB
{
    public class RepositorioAuditoria : IRepositorioAuditoria
    {
        private readonly IDBHelper _dbhelper;

        public RepositorioAuditoria(IDBHelper dbhelper)
        {
            _dbhelper = dbhelper;
        }

        public void RegistreAuditoria(Auditoria auditoria)
        {
            var sql = $@"INSERT INTO TBAUDITORIA (AUDIGUID,AUDIOPERADOR,AUDIDATA,AUDIHORA,AUDIFUNCAO,AUDIOPERACAO,AUDILOG) 
                        VALUES (
                        '{auditoria.Id}',
                        {auditoria.Operador.Codigo},
                        {Convert.ToInt32(auditoria.Data.ToString("yyyyMMdd"))},
                        {Convert.ToInt32(new DateTime(auditoria.Hora.Ticks).ToString("HHmm"))},
                        {(int)auditoria.Funcao},
                        '{auditoria.Operacao}',
                        '{auditoria.Log}')";

            using (var cn = _dbhelper.CrieConexao())
            using (var cmd = cn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
        }

        public void RegistreAuditoriaDeAcesso(AuditoriaAcesso auditoriaAcesso)
        {
            if ((TipoPessoa)auditoriaAcesso.TipoPessoa == TipoPessoa.Aluno)
            {
                var log = $"Aluno(a) - Código: {auditoriaAcesso.IdPessoa}" +
                    $" - Nome: {auditoriaAcesso.NomePessoa}" +
                    $" - Data: {DateTime.Now.ToString("dd/MM/yyyy")}" +
                    $" - Hora: {DateTime.Now.ToString("HH:mm")}" +
                    $" - Giro: {((SentidoGiro)auditoriaAcesso.SentidoDoGiro).GetDescription()}";

                RegistreAuditoria(new Auditoria(FuncaoAcesso.AcessoAluno, "Salvar", log));
            }
            else if ((TipoPessoa)auditoriaAcesso.TipoPessoa == TipoPessoa.Professor)
            {
                var log = $"Professor(a) - Código: {auditoriaAcesso.IdPessoa}" +
                    $" - Nome: {auditoriaAcesso.NomePessoa}" +
                    $" - Data: {DateTime.Now.ToString("dd/MM/yyyy")}" +
                    $" - Hora: {DateTime.Now.ToString("HH:mm")}" +
                    $" - Giro: {((SentidoGiro)auditoriaAcesso.SentidoDoGiro).GetDescription()}";

                RegistreAuditoria(new Auditoria(FuncaoAcesso.AcessoProfessoresColaboradores, "Salvar", log));
            }
            else if ((TipoPessoa)auditoriaAcesso.TipoPessoa == TipoPessoa.Profissional)
            {
                var log = $"Colaborador(a) - Código: {auditoriaAcesso.IdPessoa}" +
                    $" - Nome: {auditoriaAcesso.NomePessoa}" +
                    $" - Data: {DateTime.Now.ToString("dd/MM/yyyy")}" +
                    $" - Hora: {DateTime.Now.ToString("HH:mm")}" +
                    $" - Giro: {((SentidoGiro)auditoriaAcesso.SentidoDoGiro).GetDescription()}";

                RegistreAuditoria(new Auditoria(FuncaoAcesso.AcessoProfessoresColaboradores, "Salvar", log));
            }
        }

    }
}