﻿/*==========================================
 *创建人：刘凡平
 *邮  箱：liufanping@iveely.com
 *世界上最快乐的事情，莫过于为理想而奋斗！
 *版  本：0.1.0
 *Iveely=I void everything,except love you!
 *========================================*/

using System;
using System.Collections.Generic;

namespace Iveely.CloudComputing.CacheCommon
{
    /// <summary>
    /// 缓存传输消息
    /// </summary>
    [Serializable]
    public class Message
    {
        /// <summary>
        /// 缓存消息执行类型
        /// </summary>
        [Serializable]
        public enum CommandType
        {
            /// <summary>
            /// 设置缓存
            /// </summary>
            Set,

            /// <summary>
            /// 获取缓存
            /// </summary>
            Get,

            /// <summary>
            /// 设置缓存集
            /// </summary>
            SetList,

            /// <summary>
            /// 获取缓存集
            /// </summary>
            GetList
        }

        /// <summary>
        /// 消息执行类型
        /// </summary>
        public CommandType Command { get; set; }

        /// <summary>
        /// The endpoint of the cache
        /// </summary>
        public string Endpoint { get; set; }

        /// <summary>
        /// 消息的Key
        /// </summary>
        public object Key { get; set; }

        /// <summary>
        /// 消息的Value
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 获取前N项缓存
        /// （在获取list的时候有效）
        /// </summary>
        public int TopN { get; set; }

        /// <summary>
        /// 消息的Value集合
        /// </summary>
        public List<object> Values { get; set; }

        /// <summary>
        /// 如果遇到相同的Key是否覆盖Value
        /// </summary>
        public bool Overrrides { get; set; }

        /// <summary>
        /// 构建缓存消息
        /// </summary>
        public Message(string endpoint, CommandType command, object key, object value, int topN = 200, IEnumerable<object> values = null, bool overrides = true)
        {
            Endpoint = endpoint;
            Command = command;
            Key = key;
            Value = value;
            if (values != null)
            {
                Values = new List<object>(values);
            }
            TopN = topN;
            Overrrides = overrides;
        }

        public Message()
        {
            
        }
    }
}
