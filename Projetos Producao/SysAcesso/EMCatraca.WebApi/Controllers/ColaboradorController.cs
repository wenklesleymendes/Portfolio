using EMCatraca.WebApi.Fachadas;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EMCatraca.WebApi.Controllers
{
    public class ColaboradorController : ApiControllerAbstract
    {
        private readonly FachadaDeColaborador _fachadaDeAluno = new FachadaDeColaborador();

        [HttpGet]
        public async Task<HttpResponseMessage> ConsulteColaborador(int idColaborador)
        {
            return await AcaoDeProcessoAsync(() => _fachadaDeAluno.ConsulteColaborador(idColaborador));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> ConsulteTodosColaboradorAtivos()
        {
            return await AcaoDeProcessoAsync(() => _fachadaDeAluno.ConsulteTodosColaboradorAtivos());
        }

        [HttpGet]
        public async Task<HttpResponseMessage> ColaboradorEstaAtivo(int idColaborador)
        {
            return await AcaoDeProcessoAsync(() => _fachadaDeAluno.ColaboradorEstaAtivo(idColaborador));
        }
    }
}
