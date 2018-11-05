using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallGraphAnalyzer.Engine.DTOs
{
  
    
         
       public  enum DependencyType {CBO =1 };
         [Serializable]
         [JsonConverter(typeof(StringEnumConverter))]
       public enum NodeType { Class = 0,AbstractClass =1 ,Interface = 2, FunctionParameter = 3, ClassAttribute =4, FunctionAttribute =5,FunctionReturnValue = 6 }
    
}
