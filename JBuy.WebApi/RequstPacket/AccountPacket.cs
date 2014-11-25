using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace JBuy.WebApi.RequstPacket
{
    public class AccountPacket
    {
        /// <summary>
        /// 用户名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户性别
        /// </summary>
        public byte Sex { get; set; }

        /// <summary>
        /// 用户QQ
        /// </summary>
        public string QQ { get; set; }

        /// <summary>
        /// 用户邮件
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 用户手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 用户电话
        /// </summary>
        public string Phone { get; set; }
    }
}