using EMCatraca.Core.Dominio;
using EMCatraca.Server;
using EMCatraca.Server.Interfaces;
using System;

namespace EMCatraca.WebApi.Fachadas
{
    public class FachadaDeAcessoPessoa
    {
        private readonly IRepositorioDeAcessoPessoa _repositorioAcessoPessoa = FabricaDeRepositorios.Instancia.CrieRepositorioAcesso();
        public void RegistreAcesso(RegistroAcesso registroDeAcesso)
        {
            _repositorioAcessoPessoa.RegistreAcesso(registroDeAcesso);
        }

        public DateTime ObtenhaUltimoAcessoDaPessoa(int idPessoa, int tipoPessoa)
        {
            return _repositorioAcessoPessoa.ObtenhaUltimoAcessoDaPessoa(idPessoa, tipoPessoa);
        }

        public SentidoGiro ObtenhaTipoDeAcessoDaPessoa(int idPessoa)
        {
            return _repositorioAcessoPessoa.ObtenhaTipoDeAcessoDaPessoa(idPessoa);
        }
    }
}