/*  ----------------------------------------------------------------------------
 * 项目名： JinRen
 * 制作开发：宁波市金人网络技术有限公司IT部
 * 联系电话：0574-88229917
 * 网址：http://www.eastresoure.com
 * 制作人: 陈晓峰
 * Copyright (c) 宁波市金人网络技术有限公司
 *  ----------------------------------------------------------------------------
 */

using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace JBuy.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonBuilder:IDisposable
    {
        private StringWriter _sw = null;
        private JsonTextWriter _writer = null;

        public  JsonBuilder()
        {
            _sw = new StringWriter();
            _writer = new JsonTextWriter(_sw);
        }

        /// <summary>
        /// 获得Json字符串
        /// </summary>
        protected string JsonString
        {
            get
            {
                if(_sw != null)
                {
                    return _sw.ToString();
                }
                return null;
            }
        }

        public override string ToString()
        {
            return this.JsonString;
        }

        #region Flud API
        public JsonBuilder BeginObject()
        {
            _writer.WriteStartObject();
            return this;
        }

        public JsonBuilder EndObject()
        {
            _writer.WriteEndObject();
            return this;
        }

        public JsonBuilder BeginArray()
        {
            _writer.WriteStartArray();
            return this;
        }

        public JsonBuilder EndArray()
        {
            _writer.WriteEndArray();
            return this;
        }

        /// <summary>
        /// 设置K-V Pair
        /// </summary>
        /// <param name="key">名称</param>
        /// <param name="value">
        /// 支持JsonBuilder嵌套
        /// </param>
        /// <param name="defaultValue">提供默认值</param>
        /// <returns></returns>
        public JsonBuilder SetKvPair(string key,object value,params object[] defaultValue)
        {
            Type type = null;
            if (value == null)
            {
                var firstOrDefault = defaultValue.FirstOrDefault();
                value = firstOrDefault;
            }
            
            type = value.GetType();

            if(!string.IsNullOrEmpty(key))
                _writer.WritePropertyName(key);
            if(type == typeof(string))
            {
                _writer.WriteValue((string)value);
            }else if(type == typeof(int))
            {
                _writer.WriteValue((int)value);
            }else if(type == typeof(bool))
            {
                _writer.WriteValue((bool)value);
            }
            else if(type == typeof(byte))
            {
                _writer.WriteValue((byte)value);
            }else if(type == typeof(byte[]))
            {
                _writer.WriteValue((byte[])value);
            }else if(type == typeof(char))
            {
                _writer.WriteValue((char)value);
            }else if(type == typeof(DateTime))
            {
                _writer.WriteValue((DateTime)value);
            }else if(type == typeof(decimal))
            {
                _writer.WriteValue((decimal)value);
            }else if(type == typeof(double))
            {
                _writer.WriteValue((double)value);
            }else if(type == typeof(float))
            {
                _writer.WriteValue((float)value);
            }else if(type == typeof(Guid))
            {
                _writer.WriteValue((Guid)value);
            }else if(type == typeof(long))
            {
                _writer.WriteValue((long)value);
            }else if(type == typeof(object))
            {
                _writer.WriteValue((object)value);
            }else if(type == typeof(short))
            {
                _writer.WriteValue((short)value);
            }else if(type == typeof(sbyte))
            {
                _writer.WriteValue((sbyte)value);
            }else if(type == typeof(uint))//无符号支持
            {
                _writer.WriteValue((uint)value);
            }else if(type == typeof(ushort))
            {
                _writer.WriteValue((ushort)value);
            }else if(type == typeof(ulong))
            {
                _writer.WriteValue((ulong)value);
            }else if(type == typeof(JsonBuilder))//这个类型是否安全?
            {
                _writer.WriteRawValue(value.ToString());
            }else if(type == typeof(JsonRaw))
            {
                var jsonRaw = value as JsonRaw;
                if (jsonRaw != null) _writer.WriteRawValue(jsonRaw.Value);
            }
            return this;
        }

        #endregion

        #region IClonable接口
        public void Dispose()
        {
            if(_writer!=null)
            {
                _writer.Close();
                _writer = null;
            }
            if(_sw!=null)
            {
                _sw.Close();
                _sw.Dispose();
                _sw = null;
            }
        }

        #endregion

        public string  End()
        {
            string  result = this.ToString();
            this.Dispose();
            return result;
        }
    }

    public class JsonRaw
    {
        public string Value
        {
            get;
            set;
        }

        public JsonRaw(string value)
        {
            this.Value = value;
        }
    }
}