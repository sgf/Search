/////////////////////////////////////////////////
//�ļ���:Fucntion
//��  ��:
//������:����ƽ(Iveely Liu)
//��  ��:liufanping@iveely.com
//��  ֯:Iveely
//��  ��:2012/3/28 16:53:27
/////////////////////////////////////////////

using Iveely.Framework.DataStructure;


namespace Iveely.Framework.Algorithm.AI
{
    /// <summary>
    /// ����ִ����
    /// </summary>
    public class Function
    {
        /// <summary>
        /// ������
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ����ִ�в�������
        /// </summary>
        public SortedList<string> Parameters = new SortedList<string>();
    }
}
