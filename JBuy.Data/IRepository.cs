using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBuy.Data
{
    /// <summary>
    /// 数据仓库抽象
    /// 提供最基本的类似集合操作,所有仓库都应该继承该接口以
    /// 供用户调用CURD
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IRepository<TEntity> where TEntity:class,IEntityObject
    {
        void Initialize();

        TEntity Insert(TEntity entity);

        void Delete(TEntity entity);

        void Delete(params object[] keyValues);

        void DeleteRange(IEnumerable<TEntity> entitiesRange);

        TEntity Update(TEntity entity);

        TEntity Find(params object[] keyValues);
    }
}
