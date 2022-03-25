using EMCatraca.Core.Dominio;
using EMCatraca.Server.Interfaces;
using System;

namespace EMCatraca.Server.Repositorios.API
{
    public class RepositorioAcessoPessoaAPI : IRepositorioDeAcessoPessoa
    {
        private readonly IAPIConexao _apiConexao;

        public RepositorioAcessoPessoaAPI(IAPIConexao apiConexao)
        {
            _apiConexao = apiConexao;
        }

        public void RegistreAcesso(RegistroAcesso registroAcesso)
        {
            _apiConexao.Post("Acesso", "RegistreAcesso", registroAcesso);
        }

        public DateTime ObtenhaUltimoAcessoDaPessoa(int idPessoa, int tipoPessoa)
        {
            return _apiConexao.Get<DateTime>("Acesso", $"ObtenhaUltimoAcessoDaPessoa?idPessoa={idPessoa}&tipoPessoa={tipoPessoa}");
        }

        public SentidoGiro ObtenhaTipoDeAcessoDaPessoa(int idPessoa)
        {
            return _apiConexao.Get<SentidoGiro>("Acesso", $"ObtenhaTipoDeAcessoDaPessoa?idPessoa={idPessoa}");
        }
    }
}
