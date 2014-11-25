using System.ComponentModel.DataAnnotations;

namespace JBuy.Data.DAL.JBuy.Entities
{
    /// <summary>
    /// 模型实体
    /// </summary>
    public partial class Card:IEntityObject
    {
        public int CardId { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public double Money { get; set; }
    }
}
