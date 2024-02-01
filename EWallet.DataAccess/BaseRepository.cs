using EWallet.Common;
using System.Linq;
using EWallet.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace EWallet.DataAccess
{
    public class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly EWalletContext Context;

        public BaseRepository(EWalletContext context)
        {
            this.Context = context;
        }

        public IQueryable<TEntity> Get()
        {
            return  Context.Set<TEntity>().AsQueryable();
        }
        public IQueryable<TEntity> GetDetached()
        {
            return Context.Set<TEntity>().AsQueryable().AsNoTracking();
        }

        public TEntity Insert(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            return entity;
        }

        public TEntity Update(TEntity entitty)
        {
            Context.Set<TEntity>().Update(entitty);

            return entitty;
        }

        public void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

    }
}
