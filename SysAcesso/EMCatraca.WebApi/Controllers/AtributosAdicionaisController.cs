using EMCatraca.WebApi.Fachadas;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EMCatraca.WebApi.Controllers
{
    public class AtributosAdicionaisController : ApiControllerAbstract
    {
        private readonly FachadaDeAtributosAdicionais _fachadaDeAtributosAdicionais = new FachadaDeAtributosAdicionais();

        [HttpGet]
        public async Task<HttpResponseMessage> ConsulteTodosAtributosAdcionais()
        {
            return await AcaoDeProcessoAsync(() => _fachadaDeAtributosAdicionais.ConsulteTodosAtributosAdcionais());
        }
    }
}
