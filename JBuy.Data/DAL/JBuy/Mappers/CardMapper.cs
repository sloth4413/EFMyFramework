using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using JBuy.Data.DAL.JBuy.Entities;

namespace JBuy.Data.DAL.JBuy.Mappers
{
    /// <summary>
    /// 扩展模型配置类
    /// </summary>
    class CardMapper:EntityTypeConfiguration<Card>
    {
        /// <summary>
        /// 对模型进行配置
        /// </summary>
        public CardMapper()
        {
            ToTable("Card");
            HasKey(e => e.CardId);
            Property(c => c.CardId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.CardId).IsRequired();

            Property(e => e.UserName).HasMaxLength(64);
            Property(e => e.UserName).IsRequired();

            Property(e => e.UserId).IsRequired();

            Property(e => e.Money).IsOptional();

            Property(e => e.CardId).HasColumnName("CardId");
            Property(e => e.Money).HasColumnName("Money");
            Property(e => e.UserId).HasColumnName("UserId");
            Property(e => e.UserName).HasColumnName("UserName");
        }
    }
}
