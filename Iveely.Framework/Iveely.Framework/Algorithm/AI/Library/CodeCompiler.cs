/////////////////////////////////////////////////
//�ļ���:CodeCompiler
//��  ��:
//������:����ƽ(Iveely Liu)
//��  ��:liufanping@iveely.com
//��  ֯:Iveely
//��  ��:2012/3/28 16:05:24
///////////////////////////////////////////////

using System;
using System.Diagnostics;
using System.Reflection;

namespace Iveely.Framework.Algorithm.AI.Library
{
    /// <summary>
    /// �������
    /// </summary>
    public class CodeCompiler
    {
        /// <summary>
        /// ��ִ̬��
        /// </summary>
        /// <param name="funName">��������</param>
        /// <param name="objs">������ </param>
        /// <returns>����ִ�н��</returns>
        public static string Execute(string funName, string[] objs)
        {
            Type magicType = Type.GetType("Iveely.Framework.Algorithm.AI.Library.Sys");
            Debug.Assert(magicType != null, "magicType != null");
            ConstructorInfo magicConstructor = magicType.GetConstructor(Type.EmptyTypes);
            Debug.Assert(magicConstructor != null, "magicConstructor != null");
            object magicClassObject = magicConstructor.Invoke(new object[] { });
            MethodInfo magicMethod = magicType.GetMethod(funName);
            object magicValue = magicMethod.Invoke(magicClassObject, objs);
            //object magicValue = magicMethod.Invoke(magicClassObject,null);
            return magicValue.ToString();
        }
    }
}
