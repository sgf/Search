/////////////////////////////////////////////////
//�ļ���:System
//��  ��:
//������:����ƽ(Iveely Liu)
//��  ��:liufanping@iveely.com
//��  ֯:Iveely
//��  ��:2012/3/28 15:50:14
///////////////////////////////////////////////

using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;


namespace Iveely.Framework.Algorithm.AI.Library
{
    /// <summary>
    /// ϵͳ������
    /// </summary>
    public class Sys
    {
        /// <summary>
        /// ��ȡ��ǰʱ��
        /// </summary>
        /// <returns></returns>
        public string GetTime()
        {
            //���ص�ǰʱ��
            Debug.Assert(CultureInfo.InvariantCulture != null, "CultureInfo.InvariantCulture != null");
            return DateTime.Now.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// �ӷ�����
        /// </summary>
        /// <param name="a">����</param>
        /// <param name="b">������</param>
        /// <returns>���ؽ��</returns>
        public string Plus(string a, string b)
        {
            return (int.Parse(a) + int.Parse(b)).ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="a">������</param>
        /// <param name="b">����</param>
        /// <returns>���Ľ��</returns>
        public string Sub(string a, string b)
        {
            return (int.Parse(a) - int.Parse(b)).ToString(CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// �˷�
        /// </summary>
        /// <param name="a">����</param>
        /// <param name="b">������</param>
        /// <returns>��</returns>
        public string Mul(string a, string b)
        {
            return (int.Parse(a) * int.Parse(b)).ToString(CultureInfo.InvariantCulture);
        }

        public string Normal(params string[] infors)
        {
            return infors[0];
        }


        /// <summary>
        /// ִ��ת������
        /// </summary>
        public static string ConvertToBig(string number)
        {

            //��ȡ����ֵ
            string num = number;
            //�����ĩ��0
            num = Clean(num);
            //�ж��Ƿ�Ϊ��
            if (String.IsNullOrWhiteSpace(num))
            {
                Console.WriteLine("���ֲ���Ϊ�ջ���Ϊ�ո�");
                return null;
            }
            //�������
            if (CheckStandard(num))
            {
                //�Ӹ�С����
                num += ".";
                //С�������ֵ
                string left = num.Split('.')[0];
                //С�����ұ�ֵ
                string right = num.Split('.')[1];
                //��λ��������
                string[] symbol = { "", "ʮ", "��", "ǧ", "��", "ʮ", "��", "ǧ", "��", "ʮ", "��", "ǧ" };
                //��Сд����
                string[] uppercase = { "��", "Ҽ", "��", "��", "��", "��", "½", "��", "��", "��" };
                //������
                string result = "";
                //��ʶ�Ƿ����м�0
                int middlezero = 0;
                //���ת��Ϊ�ַ�����
                char[] cLeft = left.ToCharArray();
                //�����������ֻ��0
                if (cLeft[0] == '0')
                {
                    //ֱ���㿪ͷ
                    result += "��";
                }
                //������������ߣ�����
                for (int i = 0; i < cLeft.Length; i++)
                {
                    //������м�λ��0
                    if (cLeft[i] != '0')
                    {
                        //ת��Ϊ��д
                        result += uppercase[int.Parse(cLeft[i].ToString(CultureInfo.InvariantCulture))];
                        //��ӵ�λ
                        result += symbol[cLeft.Length - 1 - i];
                        //�м�0����
                        middlezero = 0;
                    }
                    else
                    {
                        //���������������ĩλ��0
                        if (i != cLeft.Length - 1)
                        {
                            //�����һλ�Ƿ�0
                            if (cLeft[i + 1] != '0' && (cLeft.Length - 1 - i != 4 && cLeft.Length - 1 - i != 8))
                            {
                                //ת��Ϊ��д
                                result += uppercase[int.Parse(cLeft[i].ToString(CultureInfo.InvariantCulture))];
                            }
                            //�����һλ��Ȼ��0
                            else
                            {
                                //�������λ�ú�������0
                                if (cLeft.Length - 1 - i == 4 && middlezero < 3)
                                {
                                    //�����λ��
                                    result += "��";
                                    //�м�0������
                                    middlezero = 0;
                                    //�����һλ��λΪ0
                                    if (cLeft[i + 1] == '0')
                                    {
                                        result += "��";

                                        i++;
                                    }

                                }
                                //������ں����0
                                if (cLeft.Length - 1 - i == 8 && middlezero < 3)
                                {
                                    //�����λ��
                                    result += "��";
                                    //�м�0����
                                    middlezero = 0;
                                    //�����һλ��λΪ0
                                    if (cLeft[i + 1] == '0')
                                    {
                                        result += "��";

                                        i++;
                                    }
                                }
                            }
                        }
                        middlezero++;
                    }

                }
                //С�����������Ϊ��
                if (right.Trim() != "")
                {
                    //���С����
                    result += "��";
                    //���ת��Ϊ�ַ�
                    char[] cRight = right.ToCharArray();
                    //�����ұ�С������
                    result = cRight.Aggregate(result, (current, t) => current + uppercase[int.Parse(t.ToString(CultureInfo.InvariantCulture))]);
                }
                //���ת����Ľ��
                return result;
            }
            return null;

        }
        /// <summary>
        /// ��������ַ��Ƿ�����淶
        /// </summary>
        private static bool CheckStandard(string input)
        {
            //��ʱ����
            //��һ�����ж�С����С�ڵ���1��
            int point = 0;
            //�ڶ�����ȫ����������
            int otherChar = 0;
            int num = 0;
            //��������С�������������޶����ֵΪ12λ
            int left = 0;
            //����ȫ��ת��Ϊ�ַ�����
            char[] myInput = input.ToCharArray();
            //����
            foreach (char t in myInput)
            {
//������ǵ㣬���Ǿ͵��������ַ�
                if (t != '.')
                {
                    //�ܳɹ�ת��Ϊ����
                    int temp;
                    if (int.TryParse(t.ToString(CultureInfo.InvariantCulture), out temp))
                    {
                        //����+1
                        num++;
                        //�����С�������
                        if (point < 1)
                        {
                            //��ʾ��ǰ���������
                            left++;
                        }
                    }
                        //�����ַ�
                    else
                    {
                        //�����ַ�+1
                        otherChar++;
                    }
                }
                    //��С����
                else
                {
                    //С����+1
                    point++;
                }
            }
            //���С�������1��
            if (point > 1)
            {
                Console.WriteLine("�����������ж����С���㣡");
                return false;
            }
            //���������
            //�����ߵ��������ֳ���12λ
            if (left > 12)
            {
                Console.WriteLine("��������������������ֲ��ó���12λ��");
                return false;
            }
            //���������
            //�Ƿ���������ַ�
            if (otherChar > 0)
            {
                Console.WriteLine("����������ڷ������ַ�������ϸ��飡");
                return false;
            }
            //һ������
            return true;
        }

        /// <summary>
        /// ����ײ���0��ĩβ��0
        /// </summary>
        /// <param name="input">������ַ���</param>
        /// <returns>�����������ַ���</returns>
        private static string Clean(string input)
        {
            //��������ַ���ĩ�Ŀո��
            input = input.Trim();
            //�����0��ͷ
            while (input.StartsWith("0"))
            {
                //��0���
                input = input.Substring(1, input.Length - 1);
            }
            //�����С�����0��β
            while (input.EndsWith("0") && input.Contains("."))
            {
                //���ĩβ��0
                input = input.Substring(0, input.Length - 1);
            }
            //�����ʼ��С����
            if (input.StartsWith("."))
            {
                //ǰ���0
                input = "0" + input;
            }

            //����Input
            return input;
        }


    }
}
