using EMCatraca.Server.Interfaces;
using EscolarManager.Framework.Conexao;
using TeraByte;

namespace EMCatraca.Server
{
    public abstract class FabricaDeRepositoriosAbstrata
    {
        protected IConnectionStrings ObtenhaStringDeConexao(string conexaoDatabase, string conexaoDatasource)
        {
            var sb = UtilitarioDeConexao.CrieConexao(conexaoDatabase, conexaoDatasource);
            return ConnectionStringsFactory.Crie(sb.ToString);
        }

        protected abstract IRepositorios Crie();
    }
}
