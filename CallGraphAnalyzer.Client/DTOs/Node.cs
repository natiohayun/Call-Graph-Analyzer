using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallGraphAnalyzer.Engine.DTOs
{
    public class Node : BaseGraphType
    {
        public NodeData data { set; get; }

        public Node(string name, string classes)
        {
            this.group = "nodes";
            this.removed = false;
            this.selected = false;
            this.selectabl = true;
            this.locked = false;
            this.grabbed = false;
            this.grabbable = true;
            this.position = new Position();
            this.classes = classes;
            this.data = new NodeData(name);
        }
    }
}
