using ModelPrincipal.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Repositorio.Persistencia
{
    public interface IRepositorioAbstrato<T> where T : class, IDocument
    {
        void Crie(T obj);

        void Remova(T obj);

        void Atualize(T obj);

        T GetPorId(Guid id);

        IEnumerable<T> GetPorFiltro(Expression<Func<T, bool>> filtro);

        IEnumerable<T> GetAll();
    }
}
