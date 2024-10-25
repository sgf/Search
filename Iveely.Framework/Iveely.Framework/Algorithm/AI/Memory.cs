/////////////////////////////////////////////////
//�ļ���:Memory
//��  ��:
//������:����ƽ(Iveely Liu)
//��  ��:liufanping@iveely.com
//��  ֯:Iveely
//��  ��:2012/3/28 11:16:46
///////////////////////////////////////////////


using System.Collections;

namespace Iveely.Framework.Algorithm.AI
{
    /// <summary>
    /// ����
    /// </summary>
    public class Variable
    {
        /// <summary>
        /// ������
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ����ֵ
        /// </summary>
        public string Value { get; set; }
    }

    /// <summary>
    /// ȫ�ּ���
    /// </summary>
    public class Memory
    {
        /// <summary>
        /// ȫ�ּ����
        /// </summary>
        private static readonly Hashtable Table = new Hashtable();

        /// <summary>
        /// ����֪ʶ
        /// </summary>
        /// <param name="userId">�û����</param>
        /// <param name="variable">������</param>
        /// <param name="value">����ֵ</param>
        public static void Set(string userId, string variable, string value)
        {
            //���ñ���
            var vari = new Variable { Name = variable, Value = value };
            //������
            //����ֵ
            //��ȫ�ּ�����в���
            Table.Add(userId, vari);
        }

        /// <summary>
        /// ��ȡ����֪ʶ
        /// </summary>
        /// <param name="userId">�û���</param>
        /// <param name="variable">������</param>
        /// <returns></returns>
        public static string Get(string userId, string variable)
        {
            //����ÿһ��Ԫ��
            foreach (DictionaryEntry item in Table)
            {
                //����û������
                if (item.Key.ToString()==userId)
                {
                    //ת��Ϊ����
                    var vari = (Variable)item.Value;
                    //�������ֵ���
                    if (vari.Name == variable)
                    {
                        //���ر�����ֵ
                        return vari.Value;
                    }
                }
            }
            //��������
            return "�Բ��������ˣ�����...";
        }
    }
}
