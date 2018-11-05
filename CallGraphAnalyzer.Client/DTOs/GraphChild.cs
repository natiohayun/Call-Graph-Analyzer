using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallGraphAnalyzer.Engine.DTOs
{
   public class GraphChild
    {
        
        public List<NodeType> NodeType { set; get; }
        public string Name { set; get; }

        
        
    }
}
