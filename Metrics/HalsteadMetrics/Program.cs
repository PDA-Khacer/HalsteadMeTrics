using System;
using System.Collections.Generic;
using System.IO;

namespace HalsteadMetrics
{
    class Program
    {
        //string[] operators = {"int","float","double","{","[",
        //",",";","if","for","do","while","not","return","void",
        //"+","-","*","/","%","=","==","<","<=",">",">=","!=","!",
        //"--","++","&&","||","+=","-=","*=","/=","%=","char"};
        static void Main(string[] args)
        {
            List<string> lsOperators = new List<string>() {"int","float","double","{","[","(",
            ",",";","if","for","do","while","not","return","void",
            "+","-","*","/","%","=","==","<","<=",">",">=","!=","!",
            "--","++","&&","||","+=","-=","*=","/=","%=","char"};

            int N1 = 0, n1 = 0, N2 = 0, n2 = 0;
            // readfile
            var map1 = new Dictionary<string,int>();
            var map2 = new Dictionary<string, int>();
            //
            string textFile = @"D:\Programing\Project\C#\Metrics\test.cpp";

            if (File.Exists(textFile))
            {
                // Read a text file line by line.  
                string[] lines = File.ReadAllLines(textFile);
                foreach (string line in lines)
                {
                    foreach (string oper in lsOperators)
                    {
                        if (line.Contains(oper))
                        {
                            if (!map1.ContainsKey(oper))
                                map1.Add(oper, ContainWord(line, oper));
                            else
                                map1[oper] = map1[oper] + ContainWord(line, oper);
                        }
                    }
                    // map2
                    foreach(string item in TrimAndSplit(RemoveOparator(line, lsOperators)))
                    {
                        if (map2.ContainsKey(item))
                        {
                            map2[item] = map2[item] + 1;
                        }
                        else
                        {
                            map2.Add(item, 1);
                        }
                        
                    }
                }

                // do == sẽ được tính 2 dấu bằng nữa. sử lý những ngoại lệ đó
                var token = new Dictionary<string, int>();
                foreach (var item in map1)
                {
                    token.Add(item.Key, item.Value);
                }
                foreach (var item in token)
                {
                    switch (item.Key)
                    {
                        case "==":
                            map1["="] = map1["="] - 2 * item.Value;
                            if(map1["="] == 0)
                            {
                                map1.Remove("=");
                            }
                            break;
                        case "/=":
                            map1["="] = map1["="] - 1 * item.Value;
                            map1["/"] = map1["/"] - 1 * item.Value;
                            if (map1["/"] == 0)
                            {
                                map1.Remove("/");
                            }
                            if (map1["="] == 0)
                            {
                                map1.Remove("=");
                            }
                            break;
                        case "<=":
                            map1["="] = map1["="] - 1 * item.Value;
                            map1["<"] = map1["<"] - 1 * item.Value;
                            if (map1["="] == 0)
                            {
                                map1.Remove("=");
                            }
                            if (map1["<"] == 0)
                            {
                                map1.Remove("<");
                            }
                            break;
                        case ">=":
                            map1["="] = map1["="] - 1 * item.Value;
                            map1[">"] = map1[">"] - 1 * item.Value;
                            if (map1["="] == 0)
                            {
                                map1.Remove("=");
                            }
                            if (map1[">"] == 0)
                            {
                                map1.Remove(">");
                            }
                            break;
                        case "!=":
                            map1["="] = map1["="] - 1 * item.Value;
                            map1["!"] = map1["!"] - 1 * item.Value;
                            if (map1["="] == 0)
                            {
                                map1.Remove("=");
                            }
                            if (map1["!"] == 0)
                            {
                                map1.Remove("!");
                            }
                            break;
                        case "--":
                            map1["-"] = map1["-"] - 2 * item.Value;
                            if (map1["-"] == 0)
                            {
                                map1.Remove("-");
                            }
                            break;
                        case "++":
                            map1["+"] = map1["+"] - 2 * item.Value;
                            if (map1["+"] == 0)
                            {
                                map1.Remove("+");
                            }
                            break;
                        case "+=":
                            map1["="] = map1["="] - 1 * item.Value;
                            map1["+"] = map1["+"] - 1 * item.Value;
                            if (map1["="] == 0)
                            {
                                map1.Remove("=");
                            }
                            if (map1["+"] == 0)
                            {
                                map1.Remove("+");
                            }
                            break;
                        case "-=":
                            map1["="] = map1["="] - 1 * item.Value;
                            map1["-"] = map1["-"] - 1 * item.Value;
                            if (map1["="] == 0)
                            {
                                map1.Remove("-");
                            }
                            if (map1["="] == 0)
                            {
                                map1.Remove("-");
                            }
                            break;
                        case "%=":
                            map1["="] = map1["="] - 1 * item.Value;
                            map1["%"] = map1["%"] - 1 * item.Value;
                            if (map1["="] == 0)
                            {
                                map1.Remove("=");
                            }
                            if (map1["%"] == 0)
                            {
                                map1.Remove("%");
                            }
                            break;
                        case "*=":
                            map1["="] = map1["="] - 1 * item.Value;
                            map1["*"] = map1["!"] - 1 * item.Value;
                            if (map1["="] == 0)
                            {
                                map1.Remove("=");
                            }
                            if (map1["*"] == 0)
                            {
                                map1.Remove("*");
                            }
                            break;
                    }
                }
                
                


                // tính n1 ,N1
                n1 = map1.Count;
                foreach (var item in map1)
                {
                    N1 += item.Value;
                }


                Console.WriteLine(n1+"|"+ N1);
                foreach (var item in map1)
                {
                    Console.WriteLine(item.Key + "|" + item.Value);
                }
                Console.WriteLine("Map2");
                map2.Remove(string.Empty);
                n2 = map2.Count;
                foreach (var item in map2)
                {
                    N2 += item.Value;
                }
                Console.WriteLine(n2 + "|" + N2);
                foreach (var item in map2)
                {
                    Console.WriteLine(item.Key + "|" + item.Value);
                }
            }
        }
    
        public static int ContainWord(string word,string keyWord)
        {
            int count = 0;
            if(word.Contains(keyWord))
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if(word[i] == keyWord[0])
                    {
                        bool check = true;
                        if(i + keyWord.Length > word.Length)
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
                        if(check)
                        {
                            count++;
                        }
                        
                    }
                }
            }
            return count;
        }

        public static string RemoveOparator(string text,List<string> lsOperators)
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
            text= text.Trim();
            int i = 0;
            bool flag = true;
            bool flagBefor = true;
            while (i < text.Length)
            {
                if(text[i] != ' ')
                {
                    flagBefor = flag;
                    flag = true;
                }
                else
                {
                    if(flag)
                    {
                        res += text[i];
                        flag = false;
                    }
                }
                if(flag)
                {
                    res += text[i];
                }
                i++;
            }
            List<string> lsRes = new List<string>();
            string[] ls = res.Split(' ');
            for(int j=0;j<ls.Length;j++)
            {
                lsRes.Add(ls[j]);
            }
            return lsRes;
        }
    }
}
