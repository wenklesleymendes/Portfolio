using EMCatraca.WebApi.Fachadas;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EMCatraca.WebApi.Controllers
{
    public class ResponsavelController : ApiControllerAbstract
    {
        private readonly FachadaResponsavel _fachadaDeResponsavel = new FachadaResponsavel();

        [HttpGet]
        public async Task<HttpResponseMessage> ConsulteResponsavel(int idResponsavel)
        {
            return await AcaoDeProcessoAsync(() => _fachadaDeResponsavel.ConsulteResponsavel(idResponsavel));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> ConsulteTodosResponsavelAtivos()
        {
            return await AcaoDeProcessoAsync(() => _fachadaDeResponsavel.ConsulteTodosResponsavelAtivos());
        }

        [HttpGet]
        public async Task<HttpResponseMessage> ResponsavelEstaAtivo(int idResponsavel)
        {
            return await AcaoDeProcessoAsync(() => _fachadaDeResponsavel.ResponsavelEstaAtivo(idResponsavel));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> ResponsavelEstaBloqueado(int idResponsavel)
        {
            return await AcaoDeProcessoAsync(() => _fachadaDeResponsavel.ResponsavelEstaBloqueado(idResponsavel));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> ResponsavelEstaInadimplenteDeCheques(int idResponsavel)
        {
            return await AcaoDeProcessoAsync(() => _fachadaDeResponsavel.ResponsavelEstaInadimplenteDeCheques(idResponsavel));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> ResponsavelEstaInadimplenteDuplicata(int idResponsavel)
        {
            return await AcaoDeProcessoAsync(() => _fachadaDeResponsavel.ResponsavelEstaInadimplenteDuplicata(idResponsavel));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> ResponsavelEstaPendenteDeDocumentos(int idResponsavel)
        {
            return await AcaoDeProcessoAsync(() => _fachadaDeResponsavel.ResponsavelEstaPendenteDeDocumentos(idResponsavel));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> ResponsavelEstaPendenteDeMateriais(int idResponsavel)
        {
            return await AcaoDeProcessoAsync(() => _fachadaDeResponsavel.ResponsavelEstaPendenteDeMateriais(idResponsavel));
        }
    }
}
