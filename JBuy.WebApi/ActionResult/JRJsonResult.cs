using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Runtime.Serialization;
using System.Web;

namespace JBuy.WebApi.ActionResult
{
    /// <summary>
    /// 定义实体到返回格式为JSON的基础类
    /// </summary>
    [DataContract]
    public class JRJsonResult:ResultBase
    {
        public JRJsonResult(HttpRequestMessage request) : base(request)
        {
        }

        protected override HttpContent BuildContent()
        {
            return SerializationUtils.SerializeToStreamContent(new JsonMediaTypeFormatter(), this);
        }

        #region 错误处理
        /// <summary>
        /// 错误描述信息
        /// </summary>
        [DataMember(Name = "errmsg",IsRequired = true)]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 错误描述代码
        /// </summary>
        [DataMember(Name = "errcode",IsRequired = true)]
        public long ErrorCode { get; set; }
        #endregion
    }

    internal static class SerializationUtils
    {
        public static string Serialize<T>(MediaTypeFormatter formatter, T value)
        {
            var content = SerializeToStreamContent(formatter, value);
            return content.ReadAsStringAsync().Result;
        }

        internal static StreamContent SerializeToStreamContent<T>(MediaTypeFormatter formatter, T value)
        {
            Stream stream = new MemoryStream();
            var content = new StreamContent(stream);
            formatter.WriteToStreamAsync(typeof(T), value, stream, content, null).Wait();
            stream.Position = 0;
            return content;
        }

        public static T Deserialize<T>(MediaTypeFormatter formatter, string str) where T : class
        {
            Stream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(str);
            writer.Flush();
            stream.Position = 0;
            return formatter.ReadFromStreamAsync(typeof(T), stream, null, null).Result as T;
        }
    }
}