﻿using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EscolaPro.Repository
{
    public abstract class SpecificMethods<TEntity> where TEntity : class, IIdentityEntity
    {
        #region ProtectedMethods
        protected abstract IQueryable<TEntity> GenerateQuery(Expression<Func<TEntity, bool>> filter = null,
                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                params string[] includeProperties);

        protected abstract IEnumerable<TEntity> GetYieldManipulated(IEnumerable<TEntity> entities, Func<TEntity, TEntity> DoAction);
        #endregion ProtectedMethods
    }
}
