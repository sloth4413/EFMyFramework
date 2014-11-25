using System.Data.Entity;
using System.Diagnostics;
using JBuy.Data.DAL.JBuy.Entities;
using JBuy.Data.DAL.JBuy.Mappers;

namespace JBuy.Data.DAL.JBuy
{
    /// <summary>
    /// JBuy库的上下文对象
    /// </summary>
    public class JBuyContext:EntityContext
    {
        public JBuyContext() : base(Constant.JBUY_PROVIDER_NAME)
        {
            Debug.WriteLine("Again Call");
            //Database.SetInitializer<JBuyContext>(null);
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<JBuyContext, EntityContextMigrationConfiguration>());
        }

        //static JBuyContext()
        //{
        //    System.Data.Entity.Database.SetInitializer<JBuyContext>(null);
        //}

        protected override void AttachModel(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CardMapper());
        }

        //public DbSet<Card> Cards { get; set; }
    }
}
