using EMCatraca.WebApi.Fachadas;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EMCatraca.WebApi.Controllers
{
    public class SerieTurmaController : ApiControllerAbstract
    {
        private readonly FachadaDeSerieTurma _fachadaDeSerieTurma = new FachadaDeSerieTurma();

        [HttpGet]
        public async Task<HttpResponseMessage> ConsulteTodasSeriesTurmas()
        {
            return await AcaoDeProcessoAsync(() => _fachadaDeSerieTurma.ConsulteTodasSeriesTurmas());
        }
    }
}