using EMCatraca.Core.Dominio;
using EMCatraca.WebApi.Fachadas;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EMCatraca.WebApi.Controllers
{
    public class AcessoController : ApiControllerAbstract
    {
        private readonly FachadaDeAcessoPessoa _fachadaDeAcessoPessoa = new FachadaDeAcessoPessoa();

        [HttpPost]
        public async Task<HttpResponseMessage> RegistreAcesso(RegistroAcesso registroAcesso)
        {
            return await AcaoDeProcessoAsync(() => _fachadaDeAcessoPessoa.RegistreAcesso(registroAcesso));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> ObtenhaUltimoAcessoDaPessoa(int idPessoa, int tipoPessoa)
        {
            return await AcaoDeProcessoAsync(() => _fachadaDeAcessoPessoa.ObtenhaUltimoAcessoDaPessoa(idPessoa, tipoPessoa));
        }


        [HttpGet]
        public async Task<HttpResponseMessage> ObtenhaTipoDeAcessoDaPessoa(int idPessoa)
        {
            return await AcaoDeProcessoAsync(() => _fachadaDeAcessoPessoa.ObtenhaTipoDeAcessoDaPessoa(idPessoa));
        }
    }
}
