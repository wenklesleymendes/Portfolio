using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EscolaPro.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControlePontoController : ControllerBase
    {
        IControlePontoService _controlePontoService;

        public ControlePontoController(IControlePontoService controlePontoService)
        {
            _controlePontoService = controlePontoService;
        }

        [Route("BuscarTodosArquivosPonto")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarTodosArquivosPonto()
        {
            try
            {
                var retorno = await _controlePontoService.ListaArquivosPonto();

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("SubirArquivoPonto")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> SubirArquivoPonto(IFormFile file)
        {
            bool sucesso = false;

            try
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        string s = Convert.ToBase64String(fileBytes);

                        var testes = Bin2Hex(fileBytes);

                        string extension = Path.GetExtension(file.FileName);
                    }
                }

                if (file != null && file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(Path.GetTempPath(), @"", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    sucesso = await _controlePontoService.UploadArquivo(filePath, fileName);

                    if (sucesso)
                    {
                        await _controlePontoService.SalvarArquivo(fileName, DateTime.Now);
                    }
                }

                return Ok(sucesso);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message + "\n", TipoResquisicao.Json, "Inserir", "SubirArquivoPonto");
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "Inserir", "SubirArquivoPonto");
                throw ex;
            }
        }

        [Route("ExcluirArquivoPonto")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> ExcluirFerias(int idArquivoPonto)
        {
            var retorno = await _controlePontoService.ExcluirArquivoPonto(idArquivoPonto);

            return Ok(retorno);
        }

        [Route("ExcluirPontoEletronico")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> ExcluirPontoEletronico(int idPontoEletronico)
        {
            var retorno = await _controlePontoService.ExcluirPontoEletronico(idPontoEletronico);

            return Ok(retorno);
        }

        [Route("BuscarSaldoHorasExtras")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarSaldoHorasExtras(string cpf, DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                var retorno = await _controlePontoService.BuscarSaldoHorasExtras(cpf, dataInicio, dataFim);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string Bin2Hex(byte[] binary)
        {
            StringBuilder builder = new StringBuilder();
            foreach (byte num in binary)
            {
                if (num > 15)
                {
                    builder.AppendFormat("{0:X}", num);
                }
                else
                {
                    builder.AppendFormat("0{0:X}", num); 
                }
            }
            return builder.ToString();
        }
    }
}