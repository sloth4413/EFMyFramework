using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace JBuy.WebApi.ActionResult
{
    public class ErrorResult:JRJsonResult
    {
        public ErrorResult(HttpRequestMessage request) : base(request)
        {
        }

        public ErrorResult(HttpRequestMessage request,long errcode,string errmsg):this(request)
        {
            this.ErrorCode = errcode;
            this.ErrorMessage = errmsg;
        }
    }
}