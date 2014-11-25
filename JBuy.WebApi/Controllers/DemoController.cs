using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JBuy.WebApi.ActionResult;
using JBuy.WebApi.Models;
using JBuy.WebApi.RequstPacket;

namespace JBuy.WebApi.Controllers
{
    /// <summary>
    /// 用 asp.net & webapi2 创建RESTful风格api 的例子
    /// </summary>
    /// <remarks>
    /// 该框架需要满足 提交数据 , 检索数据 ,操作数据
    /// </remarks>
    [RoutePrefix("api/demos/v1")]
    public class DemoController : ApiController
    {
        /// <summary>
        /// 账户创建(该接口支持RequestString方式提交参数)
        /// </summary>
        /// <param name="accountPacket"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("account")]
        public IHttpActionResult PostAccountCreateUri([FromUri] AccountPacket accountPacket)
        {
            return PostAccountCreate(accountPacket);
        }

        /// <summary>
        /// 账户创建(该接口支持ContentBody方式提交参数) Support V2
        /// </summary>
        /// <param name="accountPacket"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/api/demos/v2/account")]
        public IHttpActionResult PostAccountCreateBody([FromBody] AccountPacket accountPacket)
        {
            return PostAccountCreate(accountPacket);
        }

        [NonAction]
        internal IHttpActionResult PostAccountCreate(AccountPacket accountPacket)
        {
            if (accountPacket == null ||
                String.IsNullOrEmpty(accountPacket.Name) ||
                String.IsNullOrEmpty(accountPacket.Mobile) ||
                accountPacket.Sex == 0)
            {
                return new ErrorResult(Request, 0x1000, "设置的参数不正确或不完整");
            }
            else
            {
                if (AccountDemoModel.CreateAccount(accountPacket))
                {
                    return new ErrorResult(Request, 0x0000, "操作执行成功");
                }
                else
                {
                    return new ErrorResult(Request, 0x1001, "服务器发生内部错误,稍后再试");
                }
            }
        }

        [HttpGet]
        [Route("account/{mobile}")]
        public IHttpActionResult GetAccount(string mobile)
        {
            return AccountDemoModel.GetAccount(Request,mobile);
        }
    }

    /**********************************************************************
     * Api格式按照RESTful框架格式来定制
     * 
     * "/模块名/版本号/{自定义格式/.../}"
     * 
     * 关于参数的提交:
     * 该框架支持2种方式的参数提交,ContentBody方式和QuestString方式
     **********************************************************************/
}
