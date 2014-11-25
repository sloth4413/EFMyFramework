using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace JBuy.Data
{
    /// <summary>
    /// 仓库工厂,一个库必须实现该接口以初始化
    /// 所有仓库和上下文
    /// </summary>
    public interface IRepositoryFactory
    {
        void InitializeRepository(ContainerBuilder containerBuilder);

        void InitializeEntityContext(ContainerBuilder containerBuilder);
    }
}
