using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace JBuy.WebApi.ActionResult
{
    /// <summary>
    /// 定义抽象实体对象 该实现类都为WebApi返回
    /// 实体
    /// </summary>
    public abstract class ResultBase:IHttpActionResult
    {
        private HttpRequestMessage _request;

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage()
            {
                Content = BuildContent(),
                RequestMessage = _request
            };
            return Task.FromResult(response);
        }

        protected abstract HttpContent BuildContent();

        protected ResultBase(HttpRequestMessage request)
        {
            this._request = request;
        }
    }
}