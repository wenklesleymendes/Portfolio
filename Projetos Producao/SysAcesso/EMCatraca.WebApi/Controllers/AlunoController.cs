using EMCatraca.WebApi.Fachadas;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EMCatraca.WebApi.Controllers
{
    public class AlunoController : ApiControllerAbstract
    {
        private readonly FachadaDeAluno _fachadaAluno = new FachadaDeAluno();

        [HttpGet]
        public async Task<HttpResponseMessage> ConsulteAluno(int idAluno)
        {
            return await AcaoDeProcessoAsync(() => _fachadaAluno.ConsulteAluno(idAluno));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> ConsulteMatriculaAlunoPorRFID(string codigo)
        {
            return await AcaoDeProcessoAsync(() => _fachadaAluno.ConsulteMatriculaAlunoPorRFID(codigo));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> ConsulteAlunosAtivos()
        {
            return await AcaoDeProcessoAsync(() => _fachadaAluno.ConsulteAlunosAtivos());
        }

        [HttpGet]
        public async Task<HttpResponseMessage> AlunoEstaAtivo(int idAluno)
        {
            return await AcaoDeProcessoAsync(() => _fachadaAluno.AlunoEstaAtivo(idAluno));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> AlunoEstaBloqueado(int idAluno, int idAtributo)
        {
            return await AcaoDeProcessoAsync(() => _fachadaAluno.AlunoEstaBloqueado(idAluno, idAtributo));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> AlunoEstaInadimplenteDuplicata(int idAluno)
        {
            return await AcaoDeProcessoAsync(() => _fachadaAluno.AlunoEstaInadimplenteDuplicata(idAluno));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> AlunoEstaInadimplenteDuplicata(int idAluno, int diasDeAtraso)
        {
            return await AcaoDeProcessoAsync(() => _fachadaAluno.AlunoEstaInadimplenteDuplicata(idAluno, diasDeAtraso));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> AlunoEstaInadimplenteDeCheques(int idAluno)
        {
            return await AcaoDeProcessoAsync(() => _fachadaAluno.AlunoEstaInadimplenteDeCheques(idAluno));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> AlunoEstaPendenteDeDocumentos(int idAluno)
        {
            return await AcaoDeProcessoAsync(() => _fachadaAluno.AlunoEstaPendenteDeDocumentos(idAluno));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> AlunoEstaPendenteDeMateriais(int idAluno)
        {
            return await AcaoDeProcessoAsync(() => _fachadaAluno.AlunoEstaPendenteDeMateriais(idAluno));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> ConsulteAlunosPorTurmaMontada(int codigoDaSerie, int codigoDaTurma)
        {
            return await AcaoDeProcessoAsync(() => _fachadaAluno.ConsulteAlunosPorTurmaMomtada(codigoDaSerie, codigoDaTurma));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> ConsulteTurmaMontadaPorAluno(int idAluno)
        {
            return await AcaoDeProcessoAsync(() => _fachadaAluno.ConsulteTurmaMontadaPorAluno(idAluno));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> AlunoPodeSairSozinho(int idAluno, int idAtributo)
        {
            return await AcaoDeProcessoAsync(() => _fachadaAluno.AlunoPodeSairSozinho(idAluno, idAtributo));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> AlunoPodeSerLiberadoPeloAutorizado(int idAluno, int codigoAutorizacao)
        {
            return await AcaoDeProcessoAsync(() => _fachadaAluno.AlunoPodeSerLiberadoPeloAutorizado(idAluno, codigoAutorizacao));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> AlunoPodeSerLiberadoPeloResponsavel(int idAluno, int codigoResponsavel)
        {
            return await AcaoDeProcessoAsync(() => _fachadaAluno.AlunoPodeSerLiberadoPeloResponsavel(idAluno, codigoResponsavel));
        }
    }
}
