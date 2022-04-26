using ModelPrincipal.Entidades;
using System;
using System.Collections.Generic;

namespace Repositorio.Persistencia.Operadores
{
    public interface IRepositorioOperadores 
    {
        bool PossuiAdministrador();

        void CrieOuAtualize(Operador operador);

        void Atualize(Operador operador);

        void Remova(int id);

        IEnumerable<Operador> GetAll();

        IEnumerable<Operador> GetPorFiltro(Func<Operador, bool> filtro);

        Operador ConsulteOperador(int id);

        Operador ConsulteLogin(string login, string senha);
    }
}
