using Acesso.Dominio;
using System;


namespace Acesso.Interfaces.Repositorio
{
    public interface IRepositorioAcessoPessoa
    {
        void RegistreAcesso(RegistroAcesso registroAcesso);

        DateTime ObtenhaUltimoAcessoDaPessoa(int idPessoa, int tipoPessoa);

        SentidoGiro ObtenhaTipoDeAcessoDaPessoa(int idPessoa);
    }
}
