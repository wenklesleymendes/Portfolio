namespace EMCatraca.Server.Repositorios.API
{
    public interface IAPIConexao
    {
        string Delete(string controle, string acao);
        T Get<T>(string controle, string acao);
        void Post(string controle, string acao, object valor);
        T Post<T>(string controle, string acao, object valor);
    }
}