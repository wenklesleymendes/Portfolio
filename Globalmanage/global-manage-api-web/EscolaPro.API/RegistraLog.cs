using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace EscolaPro.API
{
    public class RegistraLog
    {        
        public static bool Log(string strMensagem, TipoResquisicao tipoResquisicao, string nomeApi, string metodo)
        {
           
            try
            {
                switch (tipoResquisicao)
                {
                    case TipoResquisicao.Error:
                        Serilog.Log.Error(strMensagem);
                        break;
                    case TipoResquisicao.Json:
                    case TipoResquisicao.Sucesso:
                        Serilog.Log.Information(strMensagem);
                        break;
                    default:
                        break;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}


public enum TipoResquisicao
{
    Error,
    Json,
    Sucesso
}
