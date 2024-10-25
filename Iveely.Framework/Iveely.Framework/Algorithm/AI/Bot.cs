/////////////////////////////////////////////////
//�ļ���:Bot
//��  ��:
//������:����ƽ(Iveely Liu)
//��  ��:liufanping@iveely.com
//��  ֯:Iveely
//��  ��:2012/3/27 20:51:37
///////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Iveely.Framework.DataStructure;

namespace Iveely.Framework.Algorithm.AI
{
    /// <summary>
    /// Ӧ�������
    /// </summary>
    public class Bot
    {
        /// <summary>
        /// ֪ʶĿ¼
        /// </summary>
        public SortedList<Category> Categorys = new SortedList<Category>();

        /// <summary>
        /// ��������
        /// </summary>
        public bool SetUp;

        /// <summary>
        /// Ψһʵ��
        /// </summary>
        private static Bot _bot;// = new Bot();

        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        public static Bot GetInstance(string resourceFolder = "")
        {
            return _bot ?? (_bot = new Bot(resourceFolder));
        }

        /// <summary>
        /// ���췽��
        /// </summary>
        private Bot(string resourceFolder)
        {
            SetUp = false;
            //��ʼ��
            Init(resourceFolder);
            BuildRealCategory();
            //��������
            SetUp = true;
        }

        /// <summary>
        /// Ӧ������˵ĳ�ʼ������
        /// </summary>
        public void Init(string resourceFolder = "")
        {
            if (!SetUp)
            {
                //xml�ĵ�����
                var xmlDoc = new XmlDocument();
                //���ض�Ӧ�������ļ�
                if (resourceFolder == "")
                {
                    xmlDoc.Load(resourceFolder + "Iveely.AI.aiml");
                }
                else
                {
                    xmlDoc.Load(resourceFolder + "\\Iveely.AI.aiml");
                }
                //�ӽڵ㼯��
                var selectSingleNode = xmlDoc.SelectSingleNode("aiml");
                if (selectSingleNode != null)
                {
                    XmlNodeList nodeList = selectSingleNode.ChildNodes;
                    //��ǰ���
                    //����ÿһ���ӽڵ�
                    for (int i = 0; i < nodeList.Count; i++)
                    {
                        foreach (var categoryNode in nodeList[i].ChildNodes)
                        {
                            //����ǰ��㸳ֵ
                            XmlNode currentNode = (XmlNode)categoryNode;
                            //����������
                            if (currentNode.Name.ToLower() == "category" && currentNode.HasChildNodes)
                            {
                                // �½����
                                var category = new Category();
                                //˵������ģʽ
                                var childList = currentNode.ChildNodes;
                                for (int j = 0; j < childList.Count; j++)
                                {
                                    //�ӽڵ㸳ֵ
                                    XmlNode childNode = childList[j];
                                    //�����ģʽ
                                    if (childNode.Name.ToLower() == "pattern")
                                    {
                                        //�½�ģʽ
                                        var pattern = new Pattern { Value = childNode.InnerXml.Trim() };
                                        //ģʽ����
                                        //��һ��һ����ģ��
                                        j++;
                                        //�½�ģ��
                                        var template = new Template();
                                        //���û���ӽ��
                                        if (!childList[j].HasChildNodes)
                                        {
                                            //ģ�帳ֵ
                                            template.Value = childList[j].InnerXml.Trim();
                                        }
                                        else
                                        {
                                            //��ȡ��ǰ���е��ӽڵ�
                                            XmlNodeList innerList = childList[j].ChildNodes;
                                            //�����ӽڵ�ֵ
                                            string tempValue = "";
                                            //����ѭ��
                                            for (int m = 0; m < innerList.Count; m++)
                                            {
                                                //��������
                                                if (innerList[m].Name.ToLower() == "random")
                                                {
                                                    //˵�����������
                                                    var rand = new Rand();
                                                    //��ô��ȡ�����ֵ
                                                    for (var n = 0; n < innerList[m].ChildNodes.Count; n++)
                                                    {
                                                        //���ӽ�ȥ
                                                        rand.List.Add(innerList[m].ChildNodes[n].InnerXml);
                                                    }
                                                    //ģ�����ֵ
                                                    template.Rand = rand;
                                                }
                                                //�������ֵͨ
                                                else if (innerList[m].Name.ToLower() == "#text")
                                                {
                                                    tempValue += innerList[m].Value.Trim();
                                                }
                                                //�����*�ű��
                                                else if (innerList[m].Name.ToLower() == "star")
                                                {
                                                    //��ȡstar����index����ʶλ��
                                                    int index = int.Parse(innerList[m].Attributes["index"].Value);
                                                    //�趨��ֵ
                                                    template.Star.Add(index);
                                                }
                                                //�����input�ű��
                                                else if (innerList[m].Name.ToLower() == "input")
                                                {
                                                    //��ȡinput����index����ʶλ��
                                                    int index = int.Parse(innerList[m].Attributes["index"].Value);
                                                    //�趨��ֵ
                                                    template.Input = index;
                                                }
                                                //�����Set���
                                                else if (innerList[m].Name.ToLower() == "set")
                                                {
                                                    //��ȡSet����name����ʶλ��
                                                    string setName = innerList[m].Attributes["name"].Value;
                                                    //������
                                                    string index = innerList[m].Attributes["index"].Value;
                                                    //�趨����
                                                    template.SetVariable.Name = setName;
                                                    //�趨ȡֵ���
                                                    template.SetVariable.Value = index;
                                                    //����Star����¼
                                                    template.Star.Add(int.Parse(index));
                                                }
                                                //�����Get���
                                                else if (innerList[m].Name.ToLower() == "get")
                                                {
                                                    //��ȡget�ı���
                                                    string getName = innerList[m].Attributes["name"].Value;
                                                    //������
                                                    template.GetVariable.Name = getName;
                                                }
                                                //����Ǻ������ܱ��
                                                else if (innerList[m].Name.ToLower() == "function")
                                                {
                                                    //��ȡ��������
                                                    template.Function.Name = innerList[m].Attributes["name"].Value;
                                                    //������ڲ���
                                                    if (innerList[m].Attributes["para"] != null)
                                                    {
                                                        //���Ȼ�ȡ�����е�ֵ
                                                        string[] par = innerList[m].Attributes["para"].Value.Split(',');
                                                        //����ÿһ������ֵ
                                                        foreach (string p in par)
                                                        {
                                                            //���ȼ�¼������Щ����
                                                            template.Star.Add(int.Parse(p));
                                                            //�ں�������Ҳ��¼
                                                            template.Function.Parameters.Add(p);
                                                        }
                                                    }
                                                }
                                                //���������
                                                else if (innerList[m].Name.ToLower() == "question")
                                                {
                                                    string[] questionStrings = innerList[m].InnerText.Split(
                                                        new[] { ';', '��' }, StringSplitOptions.RemoveEmptyEntries);
                                                    foreach (var questionString in questionStrings)
                                                    {
                                                        //[*.0]��˭�����?[*.1]
                                                        string[] text = questionString.Split(new[] { '?', '��' },
                                                            StringSplitOptions.RemoveEmptyEntries);
                                                        if (text.Length == 2)
                                                        {
                                                            Template.Question question = new Template.Question
                                                            {
                                                                //Description = text[0],
                                                                //Answer = text[1]
                                                            };
                                                            template.AddQuestion(question);
                                                        }
                                                    }
                                                }
                                            }
                                            //����ʱֵ�Ż�ȥ
                                            template.Value = tempValue.Trim();
                                        }
                                        //��ģ�帳ֵ��ģʽ
                                        pattern.Template = template;
                                        //��ģʽ��ӵ�ģʽ����
                                        category.Patterns.Add(pattern);
                                    }
                                }
                                //�������ӵ���𼯺�
                                Categorys.Add(category);
                            }
                        }
                    }
                }
            }

        }

        //<pattern>*[����:����|�Ϻ�|���|-ns]*��*[��Ϊ:����|������]*</pattern>
        //    <template>
        //        <function name="Normal" para="1,2,3"></function>
        //        <question>[0]ʲô�ط�[1]��[2][��Ϊ][3]��[����]</question>
        //    </template>

        private void BuildRealCategory()
        {
            for (int i = 0; i < Categorys.Count; i++)
            {
                SortedList<Pattern> realPatterns = new SortedList<Pattern>();
                SortedList<Pattern> patterns = Categorys[i].Patterns;
                foreach (Pattern t in patterns)
                {
                    if (t.Value.Contains("["))
                    {
                        string patternValue = t.Value;
                        List<Pattern> myPatterns = new List<Pattern>();
                        int leftIndex = patternValue.IndexOf('[');
                        int rightIndex = patternValue.IndexOf(']');
                        // string middleValue = string.Empty;
                        while (leftIndex > -1)
                        {
                            string patternString = patternValue.Substring(leftIndex + 1, rightIndex - leftIndex - 1);
                            string[] valueTemplate = patternString.Split(new[] { '|', ':' }, StringSplitOptions.RemoveEmptyEntries);
                            if (myPatterns.Count == 0)
                            {
                                Pattern[] pts = new Pattern[valueTemplate.Count() - 1];
                                string header = patternValue.Substring(0, leftIndex);
                                for (int k = 0; k < pts.Count(); k++)
                                {
                                    pts[k] = new Pattern();
                                    pts[k].Value = header + valueTemplate[k + 1];
                                    Template template = new Template();

                                    Template.Question[] questions =
                                        new Template.Question[t.Template.Questions.Count];
                                    for (int n = 0; n < questions.Count(); n++)
                                    {
                                        //questions[n] = new Template.Question();
                                        //questions[n].Answer =
                                        //    t.Template.Questions[n].Answer.Replace(
                                        //        "[" + valueTemplate[0] + "]", valueTemplate[k + 1]);
                                        //questions[n].Description = t.Template.Questions[n].Description.Replace(
                                        //    "[" + valueTemplate[0] + "]", valueTemplate[k + 1]);
                                    }
                                    template.Questions.AddRange(questions);
                                    template.Function = t.Template.Function;
                                    template.Star = t.Template.Star;
                                    pts[k].Template = template;
                                }
                                myPatterns.AddRange(pts);
                            }
                            else
                            {
                                Pattern[,] pts = new Pattern[myPatterns.Count, valueTemplate.Count() - 1];
                                for (int k = 0; k < myPatterns.Count; k++)
                                {

                                    for (int n = 0; n < valueTemplate.Count() - 1; n++)
                                    {
                                        pts[k, n] = new Pattern();
                                        pts[k, n].Value = myPatterns[k].Value + patternValue.Substring(0, leftIndex) + valueTemplate[n + 1];
                                        List<Template.Question> questions = myPatterns[k].Template.Questions;
                                        Template template = new Template();
                                        for (int m = 0; m < questions.Count; m++)
                                        {
                                            //Template.Question myQuestion = new Template.Question();
                                            //myQuestion.Description = questions[m].Description.Replace(
                                            //    "[" + valueTemplate[0] + "]", valueTemplate[n + 1]);
                                            //myQuestion.Answer = questions[m].Answer.Replace(
                                            //    "[" + valueTemplate[0] + "]", valueTemplate[n + 1]);
                                            //template.AddQuestion(myQuestion);

                                        }
                                        template.Function = myPatterns[k].Template.Function;
                                        template.Star = myPatterns[k].Template.Star;
                                        pts[k, n].Template = template;
                                    }
                                }

                                myPatterns.Clear();
                                foreach (var p in pts)
                                {
                                    myPatterns.Add(p);
                                }
                            }


                            patternValue = patternValue.Substring(rightIndex + 1, patternValue.Length - rightIndex - 1);
                            //  int lastRightIndex = rightIndex;
                            leftIndex = patternValue.IndexOf('[');
                            rightIndex = patternValue.IndexOf(']');
                            if (rightIndex == -1)
                            {
                                foreach (Pattern t1 in myPatterns)
                                {
                                    t1.Value += patternValue;
                                }
                            }
                        }
                        realPatterns.AddRange(myPatterns);
                    }
                    else
                    {
                        //Pattern pattern = new Pattern();
                        realPatterns.Add(t);
                    }
                }
                Categorys[i].Patterns = realPatterns;
            }
        }

        public string Answer(string input)
        {
            //��¼�û�������
            Input.List.Add(input);
            //�������
            foreach (Category cate in Categorys)
            {
                foreach (Pattern pat in cate.Patterns)
                {
                    if (Analyse.Match(pat.Value, input))
                    {
                        string replyInfo = pat.Template.Reply();
                        return replyInfo;
                    }
                }
            }
            return string.Empty;
        }

        public Template.Question BuildQuestion(string input, params string[] references)
        {
            Interrogative interrogative = new Interrogative();
            Template.Question questions = new Template.Question();
            if (input.Length < 10 || input.Length > 100)
            {
                return questions;
            }

            //TODO:??
            //Tuple<List<Tuple<string, string>>, List<Tuple<string, string>>> result = interrogative.Understand(input);
            //questions.Description = result.Item1;
            //questions.Entity = result.Item2;
            //questions.FromTitle = references[0];
            //questions.Reference = references[1];
            return questions;
        }
    }
}
