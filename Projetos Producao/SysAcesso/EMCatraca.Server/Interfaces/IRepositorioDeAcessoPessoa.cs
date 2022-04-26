using EMCatraca.Core.Dominio;
using System;

namespace EMCatraca.Server.Interfaces
{
    public interface IRepositorioDeAcessoPessoa
    {
        void RegistreAcesso(RegistroAcesso registroAcesso);

        DateTime ObtenhaUltimoAcessoDaPessoa(int idPessoa, int tipoPessoa);

        SentidoGiro ObtenhaTipoDeAcessoDaPessoa(int idPessoa);
    }
}
