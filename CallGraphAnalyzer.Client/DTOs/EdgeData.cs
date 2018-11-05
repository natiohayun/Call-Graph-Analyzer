using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallGraphAnalyzer.Engine.DTOs
{
    public class EdgeData
    {
        public string  source { set; get; }
        public string target { set; get; }
        public double weight { set; get; }
        public string label { set; get; }

        public EdgeData(Node source, Node target, double weight, string label)
        {
            this.source = source.data.id;
            this.target = target.data.id;
            this.weight = weight;
            this.label = label;
        }
    }
}
