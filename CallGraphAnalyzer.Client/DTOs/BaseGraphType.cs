using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallGraphAnalyzer.Engine.DTOs
{
    public  class BaseGraphType
    {
        public string group { set; get; }
        public Position position { set; get; }
        public bool removed { set; get; }
        public bool selected { set; get; }
        public bool selectabl { set; get; }
        public bool locked { set; get; }
        public bool grabbed { set; get; }
        public bool grabbable { set; get; }
        public string classes { set; get; }
    }
}
