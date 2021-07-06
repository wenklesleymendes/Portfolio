using Acesso.Dominio;
using Acesso.Interfaces.Repositorios.API;
using Acesso.Interfaces.Repositorios.DB;

namespace Acesso.Interfaces
{
    public class FabricaDeRepositorios : FabricaDeRepositoriosAbstrata
    {
        public static IRepositorios Instancia = new FabricaDeRepositorios().Crie();
        protected override IRepositorios Crie()
        {
            var infConexao = MapeadorArquivoJson.CarreguerArquivoJson<InformacaoConexao>("Acesso.Servidor.cfg");
            if (infConexao.EhWebAPI)
            {
                APIHelper.InstanciadorAPIHelper.Url = () => infConexao.Conexao;
                return new RepositoriosAPI();
            }
            else
            {
                DBHelper.InstanciadorDBHelper.ConnectionString = () => ObtenhaStringDeConexao(infConexao.Conexao, infConexao.IP);
                return new RepositoriosBD();
            }
        }
    }
}
