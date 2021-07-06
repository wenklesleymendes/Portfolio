using TeraByte;

namespace Acesso.Interfaces
{
    public abstract class FabricaDeRepositoriosAbstrata
    {
        protected static string ObtenhaStringDeConexao(string conexaoDatabase, string conexaoDatasource)
        {
            return UtilitarioDeConexao.CrieConexao(conexaoDatabase, conexaoDatasource).ToString();
        }

        protected abstract IRepositorios Crie();
    }
}
