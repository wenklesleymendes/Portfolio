using System.Collections.Generic;
using TeraByte.Core.Negocio.Enumeradores;

namespace Acesso.Core.Negocio.Enumeradores
{
    public class EnumeradorTipoCFG : EnumeradorSeguro<EnumeradorTipoCFG, int>
    {
        public static readonly EnumeradorTipoCFG InformacaoConexao = new EnumeradorTipoCFG(1, "Acesso.Servidor.cfg");
        public static readonly EnumeradorTipoCFG ConfiguracaoCatracaLoader = new EnumeradorTipoCFG(2, "Acesso.Loader.cfg");
        public static readonly EnumeradorTipoCFG RegrasDeAcesso = new EnumeradorTipoCFG(3, "Acesso.Acesso.cfg");
        public static readonly EnumeradorTipoCFG Dispositivo = new EnumeradorTipoCFG(4, "Acesso.Catracas.cfg");
        public static readonly EnumeradorTipoCFG Liberacoes = new EnumeradorTipoCFG(4, "Acesso.Liberacao.cfg");

        private EnumeradorTipoCFG(int Codigo, string Descricao) : base(Codigo, Descricao) { }

        public static IEnumerable<EnumeradorTipoCFG> ObtenhaTodosTiposCFG() => new List<EnumeradorTipoCFG> { InformacaoConexao, ConfiguracaoCatracaLoader, RegrasDeAcesso };
    }
}
