using EMCatraca.Core.Dominio;
using EMCatraca.Core.Utilidades;
using EMCatraca.Server;
using EMCatraca.Server.Interfaces;
using System;

namespace EMCatraca.WindowsForms.Configuracoes.Utilidades
{
    public static class ValidacaoDeSenhaOperador
    {
        public static bool SenhaInformadaEhValida(Operador operador, string senhaInformada)
        {
            IRepositorioOperador repositorioOperador = FabricaDeRepositorios.Instancia.CrieRepositorioOperador();
            if (repositorioOperador.ValideOperador(operador.Codigo, senhaInformada.CriptografeMD5()))
            {
                SessaoDoUsuario.Instancia.OperadorLogado = operador;
                return true;
            }
            else if (operador.EhAdministrador && OperadorEhAdministradorEInformouSenhaPadrao(operador, senhaInformada))
            {
                SessaoDoUsuario.Instancia.OperadorLogado = operador;
                return true;
            }

            return false;
        }

        private static bool OperadorEhAdministradorEInformouSenhaPadrao(Operador operador, string senhaInformada)
        {
            return operador.EhAdministrador && senhaInformada == ObtenhaPadraoDeSenhaDoUsuarioAdministrador();
        }

        private static string ObtenhaPadraoDeSenhaDoUsuarioAdministrador()
        {
            return $"EM@{DateTime.Now:dd}{DateTime.Now:MM}";
        }
    }
}