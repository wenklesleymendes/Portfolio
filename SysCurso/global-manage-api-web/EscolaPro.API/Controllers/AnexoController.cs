using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EscolaPro.API.Dto.Anexos;
using EscolaPro.Core.Model.Anexos;
using EscolaPro.Service.Dto;
using EscolaPro.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnexoController : ControllerBase
    {
        private readonly IAnexoService _anexoService;

        public AnexoController(
            IAnexoService anexoService
            )
        {
            _anexoService = anexoService;
        }

        [Route("Cadastrar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromForm]DtoAnexo dtoAnexo, IFormFile file)
        {
            try
            {

                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        string s = Convert.ToBase64String(fileBytes);
                        string extension = Path.GetExtension(file.FileName);

                        dtoAnexo.Arquivo = fileBytes;
                        dtoAnexo.Extensao = file.ContentType;
                        dtoAnexo.DataAnexo = DateTime.Now;
                        dtoAnexo.ArquivoString = file.FileName;

                    }
                }

                var retorno = await _anexoService.Inserir(dtoAnexo);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject(dtoAnexo);
                RegistraLog.Log(ex.Message + "\n" + json, TipoResquisicao.Json, "Inserir", "Anexo");
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "Inserir", "Anexo");
                throw ex;
            }
        }

        [Route("Download")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Download(int idAnexo)
        {
            try
            {
                var retorno = await _anexoService.Download(idAnexo);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject($"idAnexo: {idAnexo}\n");
                RegistraLog.Log(ex.Message + "\n" + json, TipoResquisicao.Json, "Download", "Anexo");
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "Download", "Anexo");
                throw ex;
            }
        }

        [Route("BuscarAnexo")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> BuscarAnexo(AnexoFiltrar dtoAnexoFiltrar)
        {
            try
            {
                var anexos = await _anexoService.BuscarPorFiltro(dtoAnexoFiltrar);

                return Ok(anexos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("BuscarDocumentosDespesa")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarDocumentosDespesa(int despesaId, bool documento)
        {
            try
            {
                var retorno = await _anexoService.BuscarPorDespesa(despesaId, documento);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("Deletar")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Detetar(int idAnexo)
        {
            try
            {
                var retorno = await _anexoService.Deleter(idAnexo);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject($"idAnexo: {idAnexo}\n");
                RegistraLog.Log(ex.Message + "\n" + json, TipoResquisicao.Json, "Detetar", "Anexo");
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "Detetar", "Anexo");
                throw ex;
            }
        }

        [Route("RecusarDocumento")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> RecusarDocumento(AnexoRecusar anexo)
        {
            var retorno = await _anexoService.RecusarDocumento(anexo);

            return Ok(retorno);
        }
    }
}