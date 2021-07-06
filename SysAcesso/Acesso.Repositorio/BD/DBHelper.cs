using EM.Infra.EMConexao;

namespace Acesso.Interfaces.Repositorios.DB
{
    public static class DBHelper
    {
        public static IInstanciador InstanciadorDBHelper = new EMDBConexaoPorProcesso();

        public static IDBHelper Instancia
        {
            get
            {
                return InstanciadorDBHelper.Instancie();
            }
        }
    }
}
