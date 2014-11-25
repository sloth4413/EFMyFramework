using System.Collections.Generic;
using System.Data.Entity;
using JBuy.Data.DAL.JBuy.Entities;

namespace JBuy.Data.DAL.JBuy.Repositorys
{
    public class CardRepository : JBuyRepository<Card>
    {
        public CardRepository(JBuyContext context) : base(context)
        {
        }
    }
}
