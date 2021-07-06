namespace Acesso.Interfaces.Repositorios.API
{
    public static class APIHelper
    {
        public static EMAPIConexao InstanciadorAPIHelper = new EMAPIConexao();

        public static APIConexao Instancia
        {
            get
            {
                return InstanciadorAPIHelper.Instancie();
            }
        }
    }
}
