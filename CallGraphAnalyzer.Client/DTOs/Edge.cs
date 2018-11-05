using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallGraphAnalyzer.Engine.DTOs
{
    public  class Edge : BaseGraphType
    {
 
        public EdgeData data { set; get; }

        public Edge(Node source , Node target ,double weight , string label , string classes)
        {
            this.group = "edges";
            this.position = new Position();
            this.removed = true;
            this.selected = false;
            this.selectabl = false;
            this.locked = false;
            this.grabbed = false;
            this.grabbable = false;
            this.classes = classes;
            data = new EdgeData(source, target, weight, label);
        }
    }
}
