using SysCar.Dominio;
using SysCar.Models;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SysCar.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/Motorista")]
    public class MotoristaController : ApiController
    {
        [HttpGet]
        [Route("Recuperar")]
        [Authorize]
        public IHttpActionResult Recuperar()
        {
            try
            {
                MotoristaModel Motorista = new MotoristaModel();
                var Motoristas = Motorista.ObtenhaMotoristas();
                return Ok(Motoristas);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //[HttpGet]
        //[Route("Recuperar/{nome:string}")]
        //public IHttpActionResult RecuperarPorNome(string nome)
        //{
        //    try
        //    {
        //        MotoristaModel Motorista = new MotoristaModel();
        //        return Ok(Motorista.ObtenhaMotoristas(nome).FirstOrDefault());
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}

        [HttpPost]
        public IHttpActionResult Post(MotoristaDTO Motorista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                MotoristaModel _Motorista = new MotoristaModel();
                _Motorista.Inserir(Motorista);
                return Ok(_Motorista.ObtenhaMotoristas());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //[HttpPut]
        //public IHttpActionResult Put(int id, [FromBody]MotoristaDTO Motorista)
        //{
        //    try
        //    {
        //        MotoristaModel _Motorista = new MotoristaModel();
        //        Motorista.Id = id;
        //        _Motorista.Atualizar(Motorista);

        //        return Ok(_Motorista.ObtenhaMotoristas(id).FirstOrDefault());
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                MotoristaModel _Motorista = new MotoristaModel();
                _Motorista.Deletar(id);
                return Ok("Deletado com Sucesso");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
