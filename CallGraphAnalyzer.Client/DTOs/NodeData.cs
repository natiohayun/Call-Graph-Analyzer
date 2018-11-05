using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallGraphAnalyzer.Engine.DTOs
{
    public class NodeData
    {
        public string name { set; get; }
        public string id { set; get; }
        public string rightAngel { set; get; }
        public string leftAngel { set; get; }

        public NodeData(string name)
        {
            this.name = name;
            id = Guid.NewGuid().ToString();
            rightAngel = "Not Define";
            leftAngel = "Not Define";
        }
    }
}
