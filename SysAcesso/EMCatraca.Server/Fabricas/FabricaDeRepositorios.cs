using EMCatraca.Core;
using EMCatraca.Core.Dominio;
using EMCatraca.Server.Interfaces;
using EMCatraca.Server.Repositorios.API;
using EMCatraca.Server.Repositorios.DB;
using EscolarManager.Framework.Conexao;
using EscolarManager.Logging;

namespace EMCatraca.Server
{
    public class FabricaDeRepositorios : FabricaDeRepositoriosAbstrata
    {
        public static IRepositorios Instancia = new FabricaDeRepositorios().Crie();

        protected override IRepositorios Crie()
        {
            var infConexao = MapeadorArquivoJson.CarreguerArquivoJson<InformacaoConexao>("Emcatraca.Servidor.cfg");
            if (infConexao.EhWebAPI)
            {
                IAPIConexao apiConexao = new APIConexao(infConexao.Conexao);
                return new RepositoriosAPI(apiConexao);
            }
            else
            {
                IDBHelper dbhelper = new DbHelper(ObtenhaStringDeConexao(infConexao.Conexao, infConexao.IP));
                return new RepositoriosBD(dbhelper);
            }
        }
    }
}
