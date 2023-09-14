using EMCatraca.Core.Dominio;
using System.Collections.Generic;

namespace EMCatraca.Server.Interfaces
{
    public interface IRepositorioResponsavel
    {
        Responsavel ConsulteResponsavel(int codigo);

        IEnumerable<Responsavel> ConsulteTodosResponsavelAtivos();

        bool ResponsavelEstaAtivo(int idResponsavel);

        bool ResponsavelEstaBloqueado(int idResponsavel);

        bool ResponsavelEstaInadimplenteDuplicata(int idResponsavel);

        bool ResponsavelEstaInadimplenteDeCheques(int idResponsavel);

        bool ResponsavelEstaPendenteDeDocumentos(int idResponsavel);

        bool ResponsavelEstaPendenteDeMateriais(int residResponsavel);
    }
}
