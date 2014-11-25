using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JBuy.WebApi
{
    internal class VerifyUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exper"></param>
        /// <returns></returns>
        public static bool VerifyIf(params bool[] exper)
        {
            return exper.All(e=>e);
        }
    }
}