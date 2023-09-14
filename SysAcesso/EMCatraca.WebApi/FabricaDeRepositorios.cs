using EMCatraca.Server;
using EMCatraca.Server.Interfaces;
using EMCatraca.Server.Repositorios.DB;
using EscolarManager.Framework.Conexao;
using EscolarManager.Logging;
using System.Web.Configuration;

namespace EMCatraca.WebApi
{
    public class FabricaDeRepositorios : FabricaDeRepositoriosAbstrata
    {
        public static IRepositorios Instancia = new FabricaDeRepositorios().Crie();
        protected override IRepositorios Crie()
        {
            string dataSource = WebConfigurationManager.AppSettings["DataSource"];
            string dataBase = WebConfigurationManager.AppSettings["DataBase"];

            IDBHelper dbhelper = new DbHelper(ObtenhaStringDeConexao(dataBase, dataSource), FabricaDeLogging.CrieLoggerFactory());
            return new RepositoriosBD(dbhelper);
        }
    }
}