using EMCatraca.WebApi.Fachadas;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EMCatraca.WebApi.Controllers
{
    public class OcorrenciasController : ApiControllerAbstract
    {
        private readonly FachadaOcorrencias _fachadaDeOcorrencias = new FachadaOcorrencias();

        [HttpGet]
        public async Task<HttpResponseMessage> ExisteOcorrencias(int idPessoa, int tipoPessoa, string ocorrencias)
        {
            return await AcaoDeProcessoAsync(() => _fachadaDeOcorrencias.ExisteOcorrencias(idPessoa, tipoPessoa, ocorrencias));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> ConsulteTodosOcorrencias()
        {
            return await AcaoDeProcessoAsync(() => _fachadaDeOcorrencias.ConsulteTodosOcorrencias());
        }
    }
}
