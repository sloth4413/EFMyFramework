using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using JBuy.Data.DAL.JBuy.Repositorys;

namespace JBuy.Data.DAL.JBuy
{
    public class JBuyRepositoryFactoryImp:IRepositoryFactory
    {
        public void InitializeRepository(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<CardRepository>().SingleInstance();
        }

        public void InitializeEntityContext(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<JBuyContext>().SingleInstance();
        }
    }
}
