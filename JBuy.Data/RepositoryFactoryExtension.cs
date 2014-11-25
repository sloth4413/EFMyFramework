using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace JBuy.Data
{
    public static class RepositoryFactoryExtension
    {
        public static RepositoryFactoryImp Resolve<T>(this RepositoryFactoryImp factory, params Action<T>[] function)
        {
            using (var scope = factory.Container.BeginLifetimeScope())
            {
                var entity = scope.Resolve<T>();
                foreach (var action in function)
                {
                    action.Invoke(entity);
                }
                return factory;
            }
        }

        public static T GetRepository<T>(this RepositoryFactoryImp factory)
        {
            return factory.Container.Resolve<T>();
        }
    }
}
