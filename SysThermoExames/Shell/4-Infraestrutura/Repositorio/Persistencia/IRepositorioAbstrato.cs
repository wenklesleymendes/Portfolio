using ModelPrincipal.Utilitarios;
using MongoDB.Driver;
using Repositorio.Conexao;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Repositorio.Persistencia
{
    public class RepositorioAbstrato<T> : IRepositorioAbstrato<T> where T : class, IDocument
    {
        protected readonly IConexaoMongoDB<T> Conexao = new ConexaoMongoDB<T>();
        protected IMongoCollection<T> Colecao { get; set; }

        public RepositorioAbstrato()
        {
            Colecao = Conexao.GetCollection(typeof(T).Name);
        }
        public virtual void Atualize(T obj)
        {
            Colecao.FindOneAndReplace(doc => doc.Id.Equals(obj.Id), obj);
        }

        public virtual void Crie(T obj)
        {
            Colecao.InsertOne(obj);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Colecao.AsQueryable();
        }

        public virtual IEnumerable<T> GetPorFiltro(Expression<Func<T, bool>> filtro)
        {
            return Colecao.Find(filtro).ToEnumerable();
        }

        public virtual T GetPorId(Guid id)
        {
            return Colecao.Find(doc => doc.Id.Equals(id)).FirstOrDefault();
        }

        public virtual void Remova(T obj)
        {
            Colecao.FindOneAndDelete(o => o.Id == obj.Id);
        }
    }
}
