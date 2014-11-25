using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBuy.Data
{
    /// <summary>
    /// 实现了基本的仓库基类,<see cref="RepositoryBase"/>继承者为
    /// 一个表的对应的仓库
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public abstract class RepositoryBase<TEntity,TContext>:IRepository<TEntity>
        where TEntity: class,IEntityObject
        where TContext:EntityContext, new()
    {
        protected TContext CurrentContext;

        public DbSet<TEntity> Entity
        {
            get { return CurrentContext.Set<TEntity>(); }
        }

        protected RepositoryBase(TContext context)
        {
            CurrentContext = context;
        }

        public virtual void Initialize()
        {
        }

        protected void DoCheck()
        {
            if(CurrentContext == null)throw new ArgumentException("当前上下文件已经被回收");
        }

        protected void DoDispose()
        {
            CurrentContext.Dispose();
            CurrentContext = null;
        }

        public virtual TEntity Insert(TEntity entity)
        {
            DoCheck();
            var result = CurrentContext.Set<TEntity>().Add(entity);
            CurrentContext.SaveChanges();
            return result;
        }

        public void Delete(TEntity entity)
        {
            DoCheck();
            CurrentContext.Set<TEntity>().Remove(entity);
            CurrentContext.SaveChanges();
        }

        public void Delete(params object[] keyValues)
        {
            var entity = Find(keyValues);
            if(null != entity)Delete(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entitiesRange)
        {
            DoCheck();
            CurrentContext.Set<TEntity>().RemoveRange(entitiesRange);
            CurrentContext.SaveChanges();
        }

        public TEntity Update(TEntity entity)
        {
            throw    new NotImplementedException();
        }

        public TEntity Find(params object[] keyValues)
        {
            DoCheck();
            var result = CurrentContext.Set<TEntity>().Find(keyValues);
            return result;
        }
    }
}
