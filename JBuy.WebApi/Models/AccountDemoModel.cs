 using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Web;
using JBuy.Data;
using JBuy.WebApi.ActionResult;
using JBuy.WebApi.RequstPacket;

namespace JBuy.WebApi.Models
{
    public class AccountDemoModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nickName"></param>
        /// <param name="sex"></param>
        /// <param name="qq"></param>
        /// <param name="mobile"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static bool CreateAccount(string nickName,byte sex,string qq,
            string mobile,string email = "",string phone="")
        {
            return CreateAccount(new AccountPacket()
            {
                Name = nickName,
                Email = email,
                Mobile = mobile,
                Phone = phone,
                QQ = qq,
                Sex = sex,
            });
        }

        public static bool CreateAccount(AccountPacket accountPacket)
        {
            try
            {
                //MySqlProviderFactory.USER.ExecuteNonQuery(
                //SQLBuilder.INSERT.TABLE("Account")
                //.VALUE("Name", accountPacket.Name)
                //.VALUE("Sex", accountPacket.Sex.ToString(CultureInfo.InvariantCulture))
                //.VALUE("QQ",accountPacket.QQ)
                //.VALUE("Email",accountPacket.Email)
                //.VALUE("Mobile",accountPacket.Mobile)
                //.VALUE("Phone",accountPacket.Phone)
                //.GetSQL()
                //);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static ResultBase GetAccount(HttpRequestMessage requestMessage,string mobile)
        {
            try
            {
                var   accountResult = new AccountResult(requestMessage);
                if(String.IsNullOrEmpty(mobile))
                    return new ErrorResult(requestMessage, 0x1000, "设置的参数不正确或不完整");
                //MySqlProviderFactory.USER.ExecuteReader(r =>
                //{
                //    if (r.Read())
                //    {
                //        accountResult.Name = r["Name"] as string;
                //        accountResult.Email = r["Email"] as string;
                //        accountResult.Mobile = mobile;
                //        accountResult.Phone = r["Phone"] as string;
                //        accountResult.QQ = r["QQ"] as string;
                //        accountResult.Sex = r.GetByte("Sex");
                //    }
                //},
                //    SQLBuilder.SELECT.IsOnly().TABLE("Account").WHERE(String.Format("{0}='{1}'","Mobile",mobile)).GetSQL());
                return accountResult;
            }
            catch (Exception ex)
            {
                return new ErrorResult(requestMessage, 0x1001, "服务器发生内部错误,稍后再试" + ex.Message);
            }
        }
    }
}