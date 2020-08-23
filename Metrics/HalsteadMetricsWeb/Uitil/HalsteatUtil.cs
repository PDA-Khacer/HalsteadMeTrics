using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalsteadMetricsWeb.Uitil
{
    public class HalsteatUtil
    {
        public static int ContainWord(string word, string keyWord)
        {
            int count = 0;
            if (word.Contains(keyWord))
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == keyWord[0])
                    {
                        bool check = true;
                        if (i + keyWord.Length > word.Length)
                        {
                            check = false;
                            break;
                        }
                        else
                        {
                            for (int j = 0; j < keyWord.Length; j++)
                            {
                                if (word[i + j] != keyWord[j])
                                {
                                    check = false;
                                    break;
                                }
                            }

                        }
                        if (check)
                        {
                            count++;
                        }

                    }
                }
            }
            return count;
        }

        public static string RemoveOparator(string text, List<string> lsOperators)
        {
            lsOperators.Add("}");
            lsOperators.Add(")");
            lsOperators.Add("]");
            string res = text;
            foreach (string item in lsOperators)
            {
                res = res.Replace(item, " ");
            }
            lsOperators.Remove("}");
            lsOperators.Remove(")");
            lsOperators.Remove("]");
            return res;
        }

        public static List<string> TrimAndSplit(string text)
        {
            string res = "";
            text = text.Trim();
            int i = 0;
            bool flag = true;
            bool flagBefor = true;
            while (i < text.Length)
            {
                if (text[i] != ' ')
                {
                    flagBefor = flag;
                    flag = true;
                }
                else
                {
                    if (flag)
                    {
                        res += text[i];
                        flag = false;
                    }
                }
                if (flag)
                {
                    res += text[i];
                }
                i++;
            }
            List<string> lsRes = new List<string>();
            string[] ls = res.Split(' ');
            for (int j = 0; j < ls.Length; j++)
            {
                if(ls[j] != string.Empty)
                    lsRes.Add(ls[j]);
            }
            return lsRes;
        }
    }
}