using System.Collections.Generic;
using TeraByte.Core.Negocio.Enumeradores;

namespace EMCatraca.Core.Negocio.Enumeradores
{
    public class EnumeradorTipoCFG : EnumeradorSeguro<EnumeradorTipoCFG, int>
    {
        public static readonly EnumeradorTipoCFG Conexao = new EnumeradorTipoCFG(1, "EmCatraca.Servidor.cfg");
        public static readonly EnumeradorTipoCFG Loader = new EnumeradorTipoCFG(2, "EmCatraca.Loader.cfg");
        public static readonly EnumeradorTipoCFG Acesso = new EnumeradorTipoCFG(3, "EmCatraca.RegraAcesso.cfg");
        public static readonly EnumeradorTipoCFG Dispositivo = new EnumeradorTipoCFG(4, "EmCatraca.Catracas.cfg");
        public static readonly EnumeradorTipoCFG Liberacoes = new EnumeradorTipoCFG(5, "EmCatraca.Liberacao.cfg");
        public static readonly EnumeradorTipoCFG CTipoPessoa = new EnumeradorTipoCFG(6, "EmCatraca.CustomizacaoTipoPessoa.cfg");

        private EnumeradorTipoCFG(int Codigo, string Descricao) : base(Codigo, Descricao) { }

        public static IEnumerable<EnumeradorTipoCFG> ObtenhaTodosTiposCFG() => new List<EnumeradorTipoCFG> { Conexao, Loader, Acesso };
    }
}
