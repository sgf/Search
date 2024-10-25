/////////////////////////////////////////////////
//�ļ���:Analyse
//��  ��:
//������:����ƽ(Iveely Liu)
//��  ��:liufanping@iveely.com
//��  ֯:Iveely
//��  ��:2012/3/27 21:52:15
///////////////////////////////////////////////


using System.Linq;
using System.Collections;

namespace Iveely.Framework.Algorithm.AI
{
    /// <summary>
    /// ��ֵ���������������ַ���ģʽ�ַ�����ƥ�䣩
    /// </summary>
    public class Analyse
    {
        /// <summary>
        /// �ַ���ƥ��
        /// </summary>
        /// <param name="pattern">ģʽֵ</param>
        /// <param name="input">�����ַ���</param>
        /// <returns>TrueΪƥ��ɹ���FalseΪƥ��ʧ��</returns>
        public static bool Match(string pattern, string input)
        {
            //���������ַ�����С�ڱȽϵ�
            return input.Length >= pattern.Length && Compare(pattern.ToArray(), input.ToArray());

        }

        /// <summary>
        /// ģʽ�Ƚ�
        /// </summary>
        /// <param name="pattern">ģʽ�ַ�����</param>
        /// <param name="input">�����ַ�����</param>
        /// <returns></returns>
        public static bool Compare(char[] pattern, char[] input)
        {
            //��ʼλ��
            int start = pattern.Length;
            //�����ά����
            var lcs = new int[input.Length, pattern.Length];
            //��
            for (int i = 0; i < pattern.Length; i++)
            {
                //��
                for (int j = 0; j < input.Length; j++)
                {
                    //��ʼ��
                    lcs[j, i] = new int();
                    //������
                    if (pattern[i]==input[j])
                    {
                        //������ֵ��Ϊ1
                        lcs[j, i] = 1;
                        //��ʼλ
                        if (start > i)
                        {
                            start = i;
                        }
                    }

                    //��������
                    else
                    {
                        //������ֵ��Ϊ0
                        lcs[j, i] = 0;
                    }
                }
            }
            //��ջ���洢
            var stack = new Stack();
            //�Ƿ��ҵ���ʼλ
            var find = false;
            //��
            for (int i = 0; i < input.Length; i++)
            {
                //��
                for (int j = 0; j < pattern.Length; j++)
                {
                    //���Ϊ1
                    if (lcs[i, j] == 1)
                    {
                        //�����ʼλС�ڵ�ǰλ
                        if (start < j && find)
                        {
                            //�ѵ�ǰ��ջ
                            stack.Push(pattern.ToArray()[j]);
                            //����ʼλ�Ƶ���ǰ
                            start = j;
                        }
                        //�����ʼλ���ڵ�ǰλ
                        else
                        {
                            if (start == j)
                            {
                                //�Ѿ��ҵ���ʼλ
                                find = true;
                                //�ѵ�ǰ��ջ
                                stack.Push(pattern.ToArray()[j]);
                                //����ʼλ�Ƶ���ǰ
                                start = j;
                            }
                            else if (start > j)
                            {
                                //��ջ
                                if (stack.Count != 0)
                                {
                                    stack.Pop();
                                }
                                //����ʼλ�ƶ�����ǰ
                                start = j;
                            }
                        }
                    }
                    ////�����2��*�����ݲ��֣�
                    //else if (LCS[i, j] == 2)
                    //{
                    //    //���������һ��star�ˣ�Ҳ���ǻ���һ��star
                    //    if (lastj != j && lastj!=-1)
                    //    {
                    //        //����ǰ�ļӽ�ȥ
                    //        Star.list.Add(star);
                    //    }
                    //    //�����ȣ�˵����һ��������star
                    //    else
                    //    {
                    //        star += input.ToArray()[i];
                    //    }
                    //}
                }
            }
            //�������ַ���
            string result = "";
            //ջ������
            int num = stack.Count;
            //������
            for (int m = 0; m < num; m++)
            {
                //�ۼ�����
                result = stack.Pop() + result;
            }
            //����ģʽ
            string pat = pattern.Aggregate("", (current, s) => current + s);
            //ģʽ
            string finnaPattern = pat.Replace("*", "");
            //��������
            string inp = input.Aggregate("", (current, s) => current + s);
            //�����滻Ϊ�ո�
            inp = result.ToArray().Aggregate(inp, (current, s) => current.Replace(s, ' '));
            //�������滻Ϊһ��
            inp = inp.Replace("  ", " ").Trim();
            //�з��ַ�
            string[] starInput = inp.Split(' ');
            //��¼����
            Star.List = starInput;
            //ģʽ�����ıȽ�
            return finnaPattern==result;

        }
    }
}
