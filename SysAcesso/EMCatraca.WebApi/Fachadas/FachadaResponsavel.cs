using EMCatraca.Core.Dominio;
using EMCatraca.Server;
using EMCatraca.Server.Interfaces;
using System.Collections.Generic;

namespace EMCatraca.WebApi.Fachadas
{
    public class FachadaResponsavel
    {
        private readonly IRepositorioResponsavel _repositorioDeResponsavel = FabricaDeRepositorios.Instancia.CrieRepositorioResponsavel();

        public Responsavel ConsulteResponsavel(int idResponsavel)
        {
            return _repositorioDeResponsavel.ConsulteResponsavel(idResponsavel);
        }

        public IEnumerable<Responsavel> ConsulteTodosResponsavelAtivos()
        {
            return _repositorioDeResponsavel.ConsulteTodosResponsavelAtivos();
        }

        public bool ResponsavelEstaAtivo(int idResponsavel)
        {
            return _repositorioDeResponsavel.ResponsavelEstaAtivo(idResponsavel);
        }

        public bool ResponsavelEstaBloqueado(int idResponsavel)
        {
            return _repositorioDeResponsavel.ResponsavelEstaBloqueado(idResponsavel);
        }

        public bool ResponsavelEstaInadimplenteDeCheques(int idResponsavel)
        {
            return _repositorioDeResponsavel.ResponsavelEstaInadimplenteDeCheques(idResponsavel);
        }

        public bool ResponsavelEstaInadimplenteDuplicata(int idResponsavel)
        {
            return _repositorioDeResponsavel.ResponsavelEstaInadimplenteDuplicata(idResponsavel);
        }

        public bool ResponsavelEstaPendenteDeDocumentos(int idResponsavel)
        {
            return _repositorioDeResponsavel.ResponsavelEstaPendenteDeDocumentos(idResponsavel);
        }

        public bool ResponsavelEstaPendenteDeMateriais(int idResponsavel)
        {
            return _repositorioDeResponsavel.ResponsavelEstaPendenteDeMateriais(idResponsavel);
        }
    }
}