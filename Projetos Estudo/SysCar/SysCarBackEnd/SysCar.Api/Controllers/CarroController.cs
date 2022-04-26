using SysCar.Dominio;
using SysCar.Models;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SysCar.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/Carro")]
    public class CarroController : ApiController
    {
        [HttpGet]
        [Route("Recuperar")]
        [Authorize]
        public IHttpActionResult Recuperar()
        {
            try
            {
                CarroModel Carro = new CarroModel();
                var Carros = Carro.ObtenhaCarros();
                return Ok(Carros);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("Recuperar/{id:int}")]
        public IHttpActionResult RecuperarPorId(int id)
        {
            try
            {
                CarroModel Carro = new CarroModel();
                return Ok(Carro.ObtenhaCarros(id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("Recuperar/{cor?}/{marca?}")]
        public IHttpActionResult RecuperarPorMarcaEhCor(string cor,string marca)
        {
            try
            {
                CarroModel Carro = new CarroModel();
                return Ok(Carro.ObtenhaCarrosPorMarcaEhCor(cor, marca));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        public IHttpActionResult Post(CarroDTO Carro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                CarroModel _Carro = new CarroModel();
                _Carro.Inserir(Carro);
                return Ok(_Carro.ObtenhaCarros());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]CarroDTO Carro)
        {
            try
            {
                CarroModel _Carro = new CarroModel();
                Carro.Id = id;
                _Carro.Atualizar(Carro);

                return Ok(_Carro.ObtenhaCarros(id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                CarroModel _Carro = new CarroModel();
                _Carro.Deletar(id);
                return Ok("Deletado com Sucesso");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
