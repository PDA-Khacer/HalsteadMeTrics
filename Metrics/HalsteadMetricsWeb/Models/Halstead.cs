using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalsteadMetricsWeb.Models
{
    public class Halstead
    {
        public int N1 { get; set; }
        public int n1 { get; set; }
        public int N2 { get; set; }
        public int n2 { get; set; }
        public int N { get; set; }
        public int n { get; set; }
        public double V { get; set; }
        public double VStats { get; set; }
        public double L { get; set; }
        public double D { get; set; }
        public double E { get; set; }
        public double I { get; set; }
        public double T { get; set; }
        public Dictionary<string, int> map1 { get; set; }
        public Dictionary<string, int> map2 { get; set; }

        public Halstead()
        {
            n = 0;
            n1 = 0;
            n2 = 0;
            N = 0;
            N1 = 0;
            N2 = 0;
            V = 0;
            VStats = 0;
            L = 0;
            D = 0;
            E = 0;
            I = 0;
            T = 0;
            map1 = new Dictionary<string, int>();
            map2 = new Dictionary<string, int>();
        }

    }
}