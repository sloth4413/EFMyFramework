using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web;

namespace JBuy.WebApi.ActionResult
{
    /// <summary>
    /// 用户账户返回给客户端的包
    /// </summary>
    [DataContract]
    public class AccountResult:JRJsonResult
    {
        public AccountResult(HttpRequestMessage request) : base(request)
        {
        }

        /// <summary>
        /// 用户名字
        /// </summary>
        [DataMember(Name = "nickname")]
        public string Name { get; set; }

        /// <summary>
        /// 用户性别
        /// </summary>
        [DataMember(Name = "sex")]
        public byte Sex { get; set; }

        /// <summary>
        /// 用户QQ
        /// </summary>
        [DataMember(Name = "qq")]
        public string QQ { get; set; }

        /// <summary>
        /// 用户邮件
        /// </summary>
        [DataMember(Name = "email")]
        public string Email { get; set; }

        /// <summary>
        /// 用户手机号码
        /// </summary>
        [DataMember(Name = "mobile")]
        public string Mobile { get; set; }

        /// <summary>
        /// 用户电话
        /// </summary>
        [DataMember(Name = "phone")]
        public string Phone { get; set; }
    }
}