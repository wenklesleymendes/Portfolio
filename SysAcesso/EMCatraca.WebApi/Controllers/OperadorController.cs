using EMCatraca.WebApi.Fachadas;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EMCatraca.WebApi.Controllers
{
    public class OperadorController : ApiControllerAbstract
    {
        private readonly FachadaOperador _fachadaOperador = new FachadaOperador();

        [HttpGet]
        public async Task<HttpResponseMessage> ConsulteTodosOperadorAtivos()
        {
            return await AcaoDeProcessoAsync(() => _fachadaOperador.ConsulteTodosOperadorAtivos());
        }

        [HttpGet]
        public async Task<HttpResponseMessage> ValideOperador(int codigo, string senha)
        {
            return await AcaoDeProcessoAsync(() => _fachadaOperador.ValideOperador(codigo, senha));
        }
    }
}
