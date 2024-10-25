﻿/*==========================================
 *创建人：刘凡平
 *邮  箱：liufanping@iveely.com
 *世界上最快乐的事情，莫过于为理想而奋斗！
 *版  本：0.1.0
 *Iveely=I void everything,except love you!
 *========================================*/

using System;
using System.Collections;

namespace Iveely.CloudComputing.Merger
{
    public class CombineTable : Operate
    {
        private const string OperateType = "combine_table";

        private readonly string _flag;

        public CombineTable(string appTimeStamp, string appName)
            : base(appTimeStamp, appName)
        {
            this.AppName = appName;
            this.AppTimeStamp = appTimeStamp;
            _flag = OperateType + "_" + appTimeStamp + "_" + appName;
        }

        public override T Compute<T>(T val)
        {
            lock (Table)
            {
                if (Table[_flag] == null)
                {
                    Table.Add(_flag, val);
                    CountTable.Add(_flag, 1);
                }
                else
                {
                    Hashtable oldTable = (Hashtable)Table[_flag];
                    Hashtable currentTable = (Hashtable)(object)val;
                    foreach (DictionaryEntry dictionaryEntry in currentTable)
                    {
                        if (oldTable.ContainsKey(dictionaryEntry.Key))
                        {
                            double oldValue = double.Parse(oldTable[dictionaryEntry.Key].ToString());
                            oldTable[dictionaryEntry.Key] = oldValue + double.Parse(dictionaryEntry.Value.ToString());
                        }
                        else
                        {
                            oldTable.Add(dictionaryEntry.Key, dictionaryEntry.Value);
                        }
                    }

                    Table[_flag] = oldTable;
                    int count = int.Parse(CountTable[_flag].ToString());
                    CountTable[_flag] = count + 1;
                }
            }
            if (Waite(_flag))
            {
                T t = (T)Convert.ChangeType(Table[_flag], typeof(T));
                return t;
            }
            throw new TimeoutException();
        }
    }
}
