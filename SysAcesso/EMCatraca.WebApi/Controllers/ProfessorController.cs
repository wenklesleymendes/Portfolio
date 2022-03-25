using EMCatraca.WebApi.Fachadas;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EMCatraca.WebApi.Controllers
{
    public class ProfessorController : ApiControllerAbstract
    {
        private readonly FachadaDeProfessor _fachadaDeProfessor = new FachadaDeProfessor();

        [HttpGet]
        public async Task<HttpResponseMessage> ConsulteProfessor(int idProfessor)
        {
            return await AcaoDeProcessoAsync(() => _fachadaDeProfessor.ConsulteProfessor(idProfessor));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> ConsulteTodosProfessorAtivos()
        {
            return await AcaoDeProcessoAsync(() => _fachadaDeProfessor.ConsulteTodosProfessorAtivos());
        }

        [HttpGet]
        public async Task<HttpResponseMessage> ProfessorEstaAtivo(int idProfessor)
        {
            return await AcaoDeProcessoAsync(() => _fachadaDeProfessor.ProfessorEstaAtivo(idProfessor));
        }
    }
}
