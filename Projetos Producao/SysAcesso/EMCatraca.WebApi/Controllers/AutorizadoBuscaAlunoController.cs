using EMCatraca.WebApi.Fachadas;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EMCatraca.WebApi.Controllers
{
    public class AutorizadoBuscaAlunoController : ApiControllerAbstract
    {
        private readonly FachadaDeAutorizadoBuscaAluno _fachadaDeAutorizadoBuscaAluno = new FachadaDeAutorizadoBuscaAluno();

        [HttpGet]
        public async Task<HttpResponseMessage> ConsulteAutorizadoBuscarAluno(int idAutorizado)
        {
            return await AcaoDeProcessoAsync(() => _fachadaDeAutorizadoBuscaAluno.ConsulteAutorizadoBuscarAluno(idAutorizado));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> ConsulteAutorizadoBuscarAlunoAtivos()
        {
            return await AcaoDeProcessoAsync(() => _fachadaDeAutorizadoBuscaAluno.ConsulteAutorizadoBuscarAlunoAtivos());
        }

        [HttpGet]
        public async Task<HttpResponseMessage> AutorizadoBuscarAlunoEstaBloqueado(int idAutorizado)
        {
            return await AcaoDeProcessoAsync(() => _fachadaDeAutorizadoBuscaAluno.AutorizadoBuscarAlunoEstaBloqueado(idAutorizado));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> AutorizadoBuscarAlunoEstaInadimplenteDeCheques(int idAutorizado)
        {
            return await AcaoDeProcessoAsync(() => _fachadaDeAutorizadoBuscaAluno.AutorizadoBuscarAlunoEstaInadimplenteDeCheques(idAutorizado));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> AutorizadoBuscarAlunoEstaInadimplenteDuplicata(int idAutorizado)
        {
            return await AcaoDeProcessoAsync(() => _fachadaDeAutorizadoBuscaAluno.AutorizadoBuscarAlunoEstaInadimplenteDuplicata(idAutorizado));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> AutorizadoBuscarAlunoEstaPendenteDeDocumentos(int idAutorizado)
        {
            return await AcaoDeProcessoAsync(() => _fachadaDeAutorizadoBuscaAluno.AutorizadoBuscarAlunoEstaPendenteDeDocumentos(idAutorizado));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> AutorizadoBuscarAlunoEstaPendenteDeMateriais(int idAutorizado)
        {
            return await AcaoDeProcessoAsync(() => _fachadaDeAutorizadoBuscaAluno.AutorizadoBuscarAlunoEstaPendenteDeMateriais(idAutorizado));
        }
    }
}
