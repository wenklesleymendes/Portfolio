using  Acesso.Dominio;
using System.Collections.Generic;

namespace Acesso.Interfaces.Repositorios.API
{
    public class RepositorioResponsavelAPI : IRepositorioResponsavel
    {
        public Responsavel ConsulteResponsavel(int idResponsavel)
        {
            return APIHelper.Instancia.Get<Responsavel>("Responsavel", $"ConsulteResponsavel?idResponsavel={idResponsavel}");
        }

        public IEnumerable<Responsavel> ConsulteTodosResponsavelAtivos()
        {
            return APIHelper.Instancia.Get<List<Responsavel>>("Responsavel", "ConsulteTodosResponsavelAtivos");
        }

        public bool ResponsavelEstaAtivo(int idResponsavel)
        {
            return APIHelper.Instancia.Get<bool>("Responsavel", $"ResponsavelEstaAtivo?idResponsavel={idResponsavel}");
        }

        public bool ResponsavelEstaBloqueado(int idResponsavel)
        {
            return APIHelper.Instancia.Get<bool>("Responsavel", $"ResponsavelEstaBloqueado?idResponsavel={idResponsavel}");
        }

        public bool ResponsavelEstaInadimplenteDeCheques(int idResponsavel)
        {
            return APIHelper.Instancia.Get<bool>("Responsavel", $"ResponsavelEstaInadimplenteDeCheques?idResponsavel={idResponsavel}");
        }

        public bool ResponsavelEstaInadimplenteDuplicata(int idResponsavel)
        {
            return APIHelper.Instancia.Get<bool>("Responsavel", $"ResponsavelEstaInadimplenteDuplicata?idResponsavel={idResponsavel}");
        }

        public bool ResponsavelEstaPendenteDeDocumentos(int idResponsavel)
        {
            return APIHelper.Instancia.Get<bool>("Responsavel", $"ResponsavelEstaPendenteDeDocumentos?idResponsavel={idResponsavel}");
        }

        public bool ResponsavelEstaPendenteDeMateriais(int idResponsavel)
        {
            return APIHelper.Instancia.Get<bool>("Responsavel", $"ResponsavelEstaPendenteDeDocumentos?idResponsavel={idResponsavel}");
        }
    }
}
