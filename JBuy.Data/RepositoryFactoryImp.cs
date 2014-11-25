using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;

namespace JBuy.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class RepositoryFactoryImp:IRepositoryFactory
    {
        public static RepositoryFactoryImp Current
        {
            get { return _instance.Value; }
        }

        private static Lazy<RepositoryFactoryImp> _instance = new Lazy<RepositoryFactoryImp>(() => new RepositoryFactoryImp(), LazyThreadSafetyMode.ExecutionAndPublication);

        internal IContainer Container { get; set; }

        public virtual void InitializeRepository(ContainerBuilder containerBuilder)
        {
        }

        public virtual void InitializeEntityContext(ContainerBuilder containerBuilder)
        {
            
        }

        static RepositoryFactoryImp()
        {
            Current.Initialize();
        }

        internal void Initialize()
        {
            var    containerBuilder = new ContainerBuilder();
            var implementers = FindRefsImplementInterface();
            foreach (var implementer in implementers)
            {
                if(implementer == this.GetType())continue;
                var repositoryFactory = Activator.CreateInstance(implementer) as IRepositoryFactory;
                if (null != repositoryFactory)
                {
                    repositoryFactory.InitializeRepository(containerBuilder);
                    repositoryFactory.InitializeEntityContext(containerBuilder);
                }
                else
                {
                    throw new ReflectionTypeLoadException(new []{implementer},null,"反射类型初始化失败");
                }
            }
            Container = containerBuilder.Build();
        }

        private static Type[] FindRefsImplementInterface()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IRepositoryFactory))))
            .ToArray();
            return types;
        }
    }
}
