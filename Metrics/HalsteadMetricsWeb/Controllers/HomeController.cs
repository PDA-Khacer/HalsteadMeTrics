using HalsteadMetricsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HalsteadMetricsWeb.Uitil;

namespace HalsteadMetricsWeb.Controllers
{
    public class HomeController : Controller
    {
        List<string> lsOperators = new List<string>() {"int","float","double","{","[","(",
            ",",";","if","for","do","while","not","return","void",
            "+","-","*","/","%","=","==","<","<=",">",">=","!=","!",
            "--","++","&&","||","+=","-=","*=","/=","%=","char"};
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Ananalyst()
        {
            var file = Request["file"];
            string codeText = Request["content"];
            Halstead model = new Halstead();
            List<string> lines = new List<string>();
            if (file == null)
            {

            }
            else
            {
                string[] tokenAS = codeText.Split('\r','\n');
                
                foreach(string i in tokenAS)
                {
                    if(i != string.Empty)
                    {
                        lines.Add(i.Trim());
                    }
                }
            }
            ////
            foreach (string line in lines)
            {
                foreach (string oper in lsOperators)
                {
                    if (line.Contains(oper))
                    {
                        if (!model.map1.ContainsKey(oper))
                            model.map1.Add(oper, HalsteatUtil.ContainWord(line, oper));
                        else
                            model.map1[oper] = model.map1[oper] + HalsteatUtil.ContainWord(line, oper);
                    }
                }
                // model.map2
                foreach (string item in HalsteatUtil.TrimAndSplit(HalsteatUtil.RemoveOparator(line, lsOperators)))
                {
                    if (model.map2.ContainsKey(item))
                    {
                        model.map2[item] = model.map2[item] + 1;
                    }
                    else
                    {
                        model.map2.Add(item, 1);
                    }
                }

            }
            // do == sẽ được tính 2 dấu bằng nữa. sử lý những ngoại lệ đó
            var token = new Dictionary<string, int>();
            foreach (var item in model.map1)
            {
                token.Add(item.Key, item.Value);
            }
            foreach (var item in token)
            {
                switch (item.Key)
                {
                    case "==":
                        if (model.map1.ContainsKey("="))
                        {
                            model.map1["="] = model.map1["="] - 2 * item.Value;
                            if (model.map1["="] == 0)
                            {
                                model.map1.Remove("=");
                            }
                        }
                        break;
                    case "/=":
                        if (model.map1.ContainsKey("=") && model.map1.ContainsKey("/"))
                        {
                            model.map1["="] = model.map1["="] - 1 * item.Value;
                            model.map1["/"] = model.map1["/"] - 1 * item.Value;
                            if (model.map1["/"] == 0)
                            {
                                model.map1.Remove("/");
                            }
                            if (model.map1["="] == 0)
                            {
                                model.map1.Remove("=");
                            }
                        }
                        break;
                    case "<=":
                        if (model.map1.ContainsKey("=") && model.map1.ContainsKey("<"))
                        {
                            model.map1["="] = model.map1["="] - 1 * item.Value;
                            model.map1["<"] = model.map1["<"] - 1 * item.Value;
                            if (model.map1["="] == 0)
                            {
                                model.map1.Remove("=");
                            }
                            if (model.map1["<"] == 0)
                            {
                                model.map1.Remove("<");
                            }
                        }
                        break;
                    case ">=":
                        if (model.map1.ContainsKey("=") && model.map1.ContainsKey(">"))
                        {
                            model.map1["="] = model.map1["="] - 1 * item.Value;
                            model.map1[">"] = model.map1[">"] - 1 * item.Value;
                            if (model.map1["="] == 0)
                            {
                                model.map1.Remove("=");
                            }
                            if (model.map1[">"] == 0)
                            {
                                model.map1.Remove(">");
                            }
                        }
                        break;
                    case "!=":
                        if (model.map1.ContainsKey("=") && model.map1.ContainsKey("!"))
                        {
                            model.map1["="] = model.map1["="] - 1 * item.Value;
                            model.map1["!"] = model.map1["!"] - 1 * item.Value;
                            if (model.map1["="] == 0)
                            {
                                model.map1.Remove("=");
                            }
                            if (model.map1["!"] == 0)
                            {
                                model.map1.Remove("!");
                            }
                        }
                        break;
                    case "--":
                        if (model.map1.ContainsKey("-"))
                        {
                            model.map1["-"] = model.map1["-"] - 2 * item.Value;
                            if (model.map1["-"] == 0)
                            {
                                model.map1.Remove("-");
                            }
                        }
                        break;
                    case "++":
                        if (model.map1.ContainsKey("+"))
                        {
                            model.map1["+"] = model.map1["+"] - 2 * item.Value;
                            if (model.map1["+"] == 0)
                            {
                                model.map1.Remove("+");
                            }
                        }
                        break;
                    case "+=":
                        if (model.map1.ContainsKey("=") && model.map1.ContainsKey("+"))
                        {
                            model.map1["="] = model.map1["="] - 1 * item.Value;
                            model.map1["+"] = model.map1["+"] - 1 * item.Value;
                            if (model.map1["="] == 0)
                            {
                                model.map1.Remove("=");
                            }
                            if (model.map1["+"] == 0)
                            {
                                model.map1.Remove("+");
                            }
                        }
                        break;
                    case "-=":
                        if (model.map1.ContainsKey("=") && model.map1.ContainsKey("-"))
                        {
                            model.map1["="] = model.map1["="] - 1 * item.Value;
                            model.map1["-"] = model.map1["-"] - 1 * item.Value;
                            if (model.map1["="] == 0)
                            {
                                model.map1.Remove("-");
                            }
                            if (model.map1["="] == 0)
                            {
                                model.map1.Remove("-");
                            }
                        }
                        break;
                    case "%=":
                        if (model.map1.ContainsKey("=") && model.map1.ContainsKey("%"))
                        {
                            model.map1["="] = model.map1["="] - 1 * item.Value;
                            model.map1["%"] = model.map1["%"] - 1 * item.Value;
                            if (model.map1["="] == 0)
                            {
                                model.map1.Remove("=");
                            }
                            if (model.map1["%"] == 0)
                            {
                                model.map1.Remove("%");
                            }
                        }
                        break;
                    case "*=":
                        if (model.map1.ContainsKey("=") && model.map1.ContainsKey("*"))
                        {
                            model.map1["="] = model.map1["="] - 1 * item.Value;
                            model.map1["*"] = model.map1["!"] - 1 * item.Value;
                            if (model.map1["="] == 0)
                            {
                                model.map1.Remove("=");
                            }
                            if (model.map1["*"] == 0)
                            {
                                model.map1.Remove("*");
                            }
                        }
                        break;
                }
            }
            // tính các thành phần của model

            model.n1 = model.map1.Count;
            model.n2 = model.map2.Count;
            foreach (var i in model.map1)
            {
                model.N1 +=  i.Value;
            }
            foreach (var i in model.map2)
            {
                model.N2 += i.Value;
            }
                
            return View("Index", model) ;
        }
    }
}