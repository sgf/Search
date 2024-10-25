﻿using System;
using Iveely.Framework.Network;

namespace Iveely.CloudComputing.MergerCommon
{
    [Serializable]
    public class MergePacket : Packet
    {
        public enum MergeType
        {
            Sum,
            Average,
            Distinct,
            CombineTable,
            CombineList,
            CombineSort,
            TopN
        }

        /// <summary>
        /// 应用的名称
        /// </summary>
        public string AppName { get; private set; }

        /// <summary>
        /// 应用的时间戳
        /// </summary>
        public string TimeStamp { get; private set; }

        public MergeType Type { get; private set; }

        public MergePacket(byte[] data, MergeType type, string timeStamp, string appName)
        {
            this.TimeStamp = timeStamp;
            this.AppName = appName;
            this.Data = data;
            this.Type = type;
        }

        public MergePacket()
        {}
    }
}
