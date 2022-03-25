using EMCatraca.Core.Dominio;
using System;

namespace EMCatraca.Core
{
    public static class MetodosDeExtensao
    {
        public static string DataSql(this DateTime data)
        {
            return data.ToString("yyyyMMdd");
        }
        public static string AnoSql(this DateTime data)
        {
            return data.ToString("yyyy");
        }

        public static string HoraSql(this DateTime data)
        {
            return data.ToString("HH:mm:ss");
        }

        public static TipoPessoa RecuperaTipo(this Pessoa pessoa)
        {
            return (TipoPessoa)Enum.Parse(typeof(TipoPessoa), pessoa.GetType().Name);
        }
    }
}
