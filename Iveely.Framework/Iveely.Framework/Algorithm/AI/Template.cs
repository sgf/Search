/////////////////////////////////////////////////
//�ļ���:Template
//��  ��:
//������:����ƽ(Iveely Liu)
//��  ��:liufanping@iveely.com
//��  ֯:Iveely
//��  ��:2012/3/27 20:45:04
///////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using Iveely.Framework.DataStructure;
using Iveely.Framework.Text;
using CodeCompiler = Iveely.Framework.Algorithm.AI.Library.CodeCompiler;


namespace Iveely.Framework.Algorithm.AI
{
    /// <summary>
    /// ģ��
    /// </summary>
    public class Template
    {
        /// <summary>
        /// ���ɵ�����
        /// </summary>
        [Serializable]
        public class Question
        {
            /// <summary>
            /// ����ҳ����
            /// </summary>
            public string FromTitle { get; internal set; }

            /// <summary>
            /// ԭʼ����
            /// </summary>
            public string Content { get; set; }

            /// <summary>
            /// ���⼯��
            /// </summary>
            public List<Tuple<string, string>> Description { get; set; }

            /// <summary>
            /// ����ʵ��
            /// </summary>
            public List<Tuple<string, string>> Entity { get; set; }

            /// <summary>
            /// �ο���Ϣ
            /// </summary>
            public string Reference { get; internal set; }

            /// <summary>
            /// ��ʶ
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// ��ȡ�������
            /// </summary>
            /// <param name="input"></param>
            /// <returns></returns>
            public Tuple<string, decimal> GetBestQuestion(string input)
            {
                string bestQuestion = string.Empty;
                string bestAnswer = string.Empty;
                decimal val = 0;
                foreach (var desc in Description)
                {
                    decimal similarVal = LevenshteinDistance.Instance.LevenshteinDistancePercent(input, desc.Item1);
                    if (similarVal > val)
                    {
                        val = similarVal;
                        bestQuestion = desc.Item1;
                        bestAnswer = desc.Item2;
                    }
                }
                if (val > 0)
                {
                    string question = string.Format("{2}[|]�������ʣ�{0}��<br/>�𰸿����ǣ�{1}[|]-[|]{3}", bestQuestion, bestAnswer, "�ʴ�����ϵͳ��" + FromTitle, Reference);
                    Tuple<string, decimal> tuple = new Tuple<string, decimal>(question, val);
                    return tuple;
                }
                return null;
            }

            /// <summary>
            /// ��ȡ��������
            /// </summary>
            /// <returns></returns>
            public List<string> GetAllQuestions()
            {
                List<string> allQuestions = new List<string>();
                foreach (Tuple<string, string> tuple in Description)
                {
                    allQuestions.Add(tuple.Item1 + "\t" + tuple.Item2);
                }
                return allQuestions;
            }

            public override string ToString()
            {
                return string.Format("{0}[|]{1}[|]{2}[|]{3}", FromTitle, Content, Entity.Count > 0 ? Entity[0].Item1 : "-", Reference);
            }
        }

        /// <summary>
        /// ģ��ֵ
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// ģ�����ֵ
        /// </summary>
        public Rand Rand { get; set; }

        /// <summary>
        /// ƥ��*���λ��
        /// </summary>
        public SortedList<int> Star { get; set; }

        /// <summary>
        /// �û��ĵڼ������
        /// </summary>
        public int Input { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public Variable SetVariable;

        /// <summary>
        /// ��ȡ�ı���
        /// </summary>
        public Variable GetVariable;

        /// <summary>
        /// ������
        /// </summary>
        public Function Function { get; set; }

        public List<Question> Questions { get; set; }

        /// <summary>
        /// ���췽��
        /// </summary>
        public Template()
        {
            //���ѡ���ʼ��
            Rand = new Rand();
            //���ñ�����ʼ��
            SetVariable = new Variable();
            //��ȡ������ʼ��
            GetVariable = new Variable();
            //�������ϳ�ʼ��
            Function = new Function();
            //*��ƥ�����ʼ��
            Star = new SortedList<int>();
            //��ȡ�û������ʼ��
            Input = -1;
            //��ʼ������
            Questions = new List<Question>();
        }

        /// <summary>
        /// �ظ���Ϣ
        /// </summary>
        /// <returns></returns>
        public string Reply()
        {
            //�ظ���Ϣ
            var result = Value;
            //����Ǵ��м����*�ű�ʶ
            if (Star != null)
            {
                //������ֲ�Ϊ��
                if (SetVariable.Name != "" || SetVariable.Name != null)
                {
                    //����ÿһ��*����
                    for (int i = 0; i < Star.Count; i++)
                    {
                        //�洢������Ŀ
                        User.StoreNum++;
                        //�趨�������
                        Memory.Set(User.UserId + "" + User.StoreNum, SetVariable.Name, AI.Star.List[i]);
                        //break;
                    }
                    //����ֵ
                    //return this.Value;
                }
                //�����Ϣ
                //return this.Value + Smart.Star.List[Star-1];
                //����ÿһ��*����
                //for (int i = 0; i < this.Star.Count; i++)
                //{
                //    result += Smart.Star.List[i];
                //}
            }

            //����ж�ִ̬�к�������
            if (Function != null)
            {
                //��Ҫ���ݵĲ�������
                var parm = new SortedList<string>();
                //�����������ֵȡ����
                var star = Star;
                if (star != null)
                {
                    int[] va = star.ToArray();
                    //����ʵֵȡ����
                    parm.AddRange(va.Select(a => AI.Star.List[a - 1]));
                }
                //ִ�У���ȻҪ�Ѳ������ݹ�ȥ
                return CodeCompiler.Execute(Function.Name, parm.ToArray());
            }
            //��ȡ�洢�ı���ֵ
            if (GetVariable.Name != null)
            {

                //��ȡ����
                result
                    += Memory.Get(User.UserId + "" + User.StoreNum, GetVariable.Name);
            }
            //�������û����������
            if (Input != -1)
            {
                //�����Ϣ
                result += AI.Input.List[Input - 1];
            }
            if (String.IsNullOrEmpty(result))
            {
                //������ķ�ʽ����
                var rnd = new Random();
                //����
                if (Rand != null) return Rand.List[rnd.Next(0, Rand.List.Count)];
            }
            return result;
        }


        public void AddQuestion(Question question)
        {
            Questions.Add(question);
        }

        public List<Question> BuildQuestion(params string[] references)
        {
            //List<Question> formalQuestions = new List<Question>();
            //string[] values = AI.Star.List;
            //foreach (var question in Questions)
            //{
            //    string doubt = question.Description;
            //    string answer = question.Answer;
            //    for (int i = 0; i < values.Count(); i++)
            //    {
            //        doubt = doubt.Replace("[" + i + "]", values[i]);
            //        answer = answer.Replace("[" + i + "]", values[i]);
            //    }

            //    //doubt = ReplaceStar(doubt, values);
            //    answer = ReplaceStar(answer, values);
            //    Question formalQuestion = new Question
            //    {
            //        Answer = answer,
            //        Description = doubt,
            //        FromTitle = references[1],
            //        Reference = references[0]
            //    };
            //    formalQuestions.Add(formalQuestion);
            //}
            //return formalQuestions;
            return null;
        }

        private string ReplaceStar(string text, string[] values)
        {
            int indexStar = text.IndexOf("*", StringComparison.Ordinal);
            while (indexStar > -1)
            {
                int numberLeftIndex = text.IndexOf("{", StringComparison.Ordinal);
                int numberRightIndex = text.IndexOf("}", StringComparison.Ordinal);
                string[] indexs = text.Substring(numberLeftIndex + 1, numberRightIndex - numberLeftIndex - 1).Split(new[] { ',', '��' });
                int start = int.Parse(indexs[0]);
                int end = start;
                if (indexs.Length > 1)
                {
                    end = int.Parse(indexs[1]);
                }
                text = text.Remove(numberLeftIndex, numberRightIndex - numberLeftIndex + 1);
                while (end >= start)
                {
                    text = text.Remove(indexStar, 1).Insert(indexStar, values[start]);
                    indexStar = text.IndexOf("*", StringComparison.Ordinal);
                    start++;
                }

            }
            return text;
        }
    }
}
