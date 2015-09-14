using System;
using System.Linq;
using System.Linq.Expressions;
using System.Data;
using Microsoft.Data.Entity;
using OctoFX.Core.Model;

namespace OctoFX.Core.Repository
{
    public abstract class OctoFXRepository<TEntity>
	   : IOctoFXRepository<TEntity> where TEntity : class, IId
	{
        OctoFXContext context;
        
		public OctoFXContext Context
		{
			get{return context;}
			set{context = value;}
		}

        public IQueryable<TEntity> GetAll()
        {
            var query = context.Set<TEntity>();
            return query;
        }

        public TEntity GetById(int id)
        {
            var entity = context.Set<TEntity>().FirstOrDefault(e => e.Id == id);
            return entity;
        }
        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            var query = context.Set<TEntity>().Where(predicate);
            return query;
        }

        public void Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}