using Acesso.Dominio;
using Acesso.Interfaces.Repositorio;
using System;

namespace Acesso.Repositorio.API
{
    public class RepositorioAcessoPessoaAPI : IRepositorioAcessoPessoa
    {
        public void RegistreAcesso(RegistroAcesso registroAcesso)
        {
            Acesso.Interfaces.Repositorios.API.APIHelper.Instancia.Post("Acesso", "RegistreAcesso", registroAcesso);
        }

        public DateTime ObtenhaUltimoAcessoDaPessoa(int idPessoa, int tipoPessoa)
        {
            return APIHelper.Instancia.Get<DateTime>("Acesso", $"ObtenhaUltimoAcessoDaPessoa?idPessoa={idPessoa}&tipoPessoa={tipoPessoa}");
        }

        public SentidoGiro ObtenhaTipoDeAcessoDaPessoa(int idPessoa)
        {
            return APIHelper.Instancia.Get<SentidoGiro>("Acesso", $"ObtenhaTipoDeAcessoDaPessoa?idPessoa={idPessoa}");
        }

        public void RegistreAcesso(RegistroAcesso registroAcesso)
        {
            throw new NotImplementedException();
        }

        SentidoGiro IRepositorioAcessoPessoa.ObtenhaTipoDeAcessoDaPessoa(int idPessoa)
        {
            throw new NotImplementedException();
        }
    }
}
