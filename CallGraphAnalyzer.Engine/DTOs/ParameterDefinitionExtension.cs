using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallGraphAnalyzer.Engine.DTOs
{
   public class ParameterDefinitionExtension
    {

        public ParameterDefinitionExtension ()
        {
            IsList = false;
            ListTypeName = string.Empty;
        }
        public ParameterDefinition parameterDefinition { set; get; }

        public bool IsList { set; get; }

        public string ListTypeName { set; get; }

    }
}
