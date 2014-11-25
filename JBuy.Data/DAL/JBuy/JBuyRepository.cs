using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBuy.Data.DAL.JBuy
{
    public class JBuyRepository<TEntity> : RepositoryBase<TEntity, JBuyContext>
        where TEntity : class,IEntityObject
    {
        public JBuyRepository(JBuyContext context) : base(context)
        {
        }
    }
}
