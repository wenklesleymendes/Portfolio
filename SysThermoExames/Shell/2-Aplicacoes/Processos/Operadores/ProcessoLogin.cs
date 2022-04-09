using ModelPrincipal.Entidades;
using Repositorio.Persistencia.Operadores;
using System;

namespace Processos.Operadores
{
    public class ProcessoLogin
    {
        private readonly IRepositorioOperadores Repositorio = new RepositorioOperadores();
        private readonly ProcessoOperadores processoOperdores = new ProcessoOperadores();

        public bool PossuiAdministrador() =>  Repositorio.PossuiAdministrador();

        public Operador RealizeLogin(string login, string senha)
        {
            if(EhAdministradorPrincipal(login, senha))
            {
                return Operador.OperadorAdministrador;
            }

            return Repositorio.ConsulteLogin(login, processoOperdores.CriptografeSenha(senha));            
        }

        public bool EhAdministradorPrincipal(string login, string senha)
        {
            if(login == Operador.OperadorAdministrador.Login && senha == Operador.OperadorAdministrador.Senha)
            {
                return true;
            }

            return false;
        }
    }
}
