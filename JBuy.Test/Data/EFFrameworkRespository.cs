using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using JBuy.Data;
using JBuy.Data.DAL.JBuy;
using JBuy.Data.DAL.JBuy.Entities;
using JBuy.Data.DAL.JBuy.Repositorys;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JBuy.Test.Data
{
    [TestClass]
    public class EFFrameworkRespository
    {
        /// <summary>
        /// 用lambada表达式
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            RepositoryFactoryImp.Current.Resolve<CardRepository>(e => e.Insert(new Card()
            {
               Money     = 100,UserName = "Chenxf",UserId = 4413
            })).Resolve<CardRepository>(e=>e.Delete(1));

            RepositoryFactoryImp.Current.Resolve<CardRepository>(e =>
            {
                var entity = from Card card in e.Entity where card.UserName == "Chenxf" select card;
                foreach (var card in entity)
                {
                    Debug.WriteLine(card.CardId);
                }
            });

            RepositoryFactoryImp.Current.Resolve<CardRepository>(e => e.Insert(new Card()
            {
               Money     = 102,UserName = "Wxf",UserId = 4413
            }), e => e.Insert(new Card()
            {
                Money = 103,
                UserName = "Wxf",
                UserId = 4413
            }), e => e.Insert(new Card()
            {
                Money = 104,
                UserName = "Wxf",
                UserId = 4413
            }));
        }

        /// <summary>
        /// 用一般方式
        /// </summary>
        [TestMethod]
        public void TestMethod2()
        {
            var repository = RepositoryFactoryImp.Current.GetRepository<CardRepository>();
            repository.Insert(new Card()
            {
                UserId = 3389,
                Money = 101,
                UserName = "BigChen"
            });
        }
    }
}
