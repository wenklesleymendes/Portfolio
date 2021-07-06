using Acesso.Dominio;
using Acesso.Interfaces.Repositorio;
using System;

namespace Acesso.Interfaces.Repositorios.DB
{
    public class RepositorioAcessoPessoa : IRepositorioAcessoPessoa
    {
        public void RegistreAcesso(RegistroAcesso registroAcesso)
        {
            string sqlCodigoResponsavelAutorizou = "NULL";
            string sqlCodigoAutorizadoAutorizou = "NULL";

            if (((TipoPessoa)registroAcesso.TipoPessoaAutorizou) == TipoPessoa.Responsavel) sqlCodigoResponsavelAutorizou = registroAcesso.CodigoPessoaAutorizou.ToString();
            if (((TipoPessoa)registroAcesso.TipoPessoaAutorizou) == TipoPessoa.AutorizadoBuscarAluno) sqlCodigoAutorizadoAutorizou = registroAcesso.CodigoPessoaAutorizou.ToString();

            var sentido = "I";
            if (((SentidoGiro)registroAcesso.SentidoDoGiro) == SentidoGiro.Entrada) sentido = "E";
            if (((SentidoGiro)registroAcesso.SentidoDoGiro) == SentidoGiro.Saida) sentido = "S";

            var sql = $@"INSERT INTO TBREGISTROACESSO (REGACMATRICULA,REGACTIPOPESSOA,REGACDIA,REGACHORA,REGACINFORMADOMANUAL,REGACGIRO,REGACRESCODIGO,REGACAUCODIGO)
                                VALUES({registroAcesso.IdPessoa},{registroAcesso.TipoPessoaAutorizou},{DateTime.Now.DataSql()},'{DateTime.Now.HoraSql()}','N','{sentido}',{sqlCodigoResponsavelAutorizou},{sqlCodigoAutorizadoAutorizou})";

            using (var cn = DBHelper.Instancia.CrieConexao())
            using (var cmd = cn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
        }

        public DateTime ObtenhaUltimoAcessoDaPessoa(int idPessoa, int tipoPessoa)
        {
            var sql = $@"SELECT FIRST 1 REGACDIA, REGACHORA
                        FROM TBREGISTROACESSO
                        WHERE REGACMATRICULA = {idPessoa}
                        AND REGACTIPOPESSOA = {tipoPessoa}
                        ORDER BY REGACDIA DESC, REGACHORA DESC";

            using (var dr = DBHelper.Instancia.ExecuteReader(sql))
            {
                if (dr.Read())
                {
                    return dr.GetIntegerToDate("REGACDIA") + dr.GetDateTime("REGACHORA").TimeOfDay;
                }
            }

            return DateTime.MinValue;
        }

        public SentidoGiro ObtenhaTipoDeAcessoDaPessoa(int idPessoa)
        {
            var sql = $@"SELECT FIRST 1 REGACGIRO
                            FROM TBREGISTROACESSO
                            WHERE REGACMATRICULA = {idPessoa}
                            AND REGACDIA = {DateTime.Now.HoraSql()}
                            ORDER BY REGACDIA DESC, REGACHORA DESC";

            using (var dr = DBHelper.Instancia.ExecuteReader(sql))
            {
                if (dr.Read())
                {
                    return dr.GetString("REGACGIRO") == "E" ? SentidoGiro.Saida : SentidoGiro.Entrada;
                }
            }
            return SentidoGiro.Indefinido;
        }
    }
}
