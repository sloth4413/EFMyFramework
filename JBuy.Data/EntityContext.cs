using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JBuy.Data.DAL.JBuy;

namespace JBuy.Data
{
    /// <summary>
    /// 实体上下文 
    /// 库对应的上下文都应该继承该类
    /// </summary>
    public class EntityContext:DbContext
    {
        public string ConnectionString { protected set; get; }

        protected EntityContext(string connectString):base(connectString)
        {
            ConnectionString = connectString;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //用Mappers配置创建模型
            //modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
            AttachModel(modelBuilder);
        }

        /// <summary>
        /// 挂接模型配置
        /// </summary>
        protected virtual void AttachModel(DbModelBuilder modelBuilder)
        {
            
        }
    }
}
