using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallGraphAnalyzer.Engine.DTOs
{
   public class FieldDefinitionExtension
    {
       public FieldDefinition fieldDefinition { set; get; }

       public string GetfieldDefinitionName()
       {

           return fieldDefinition.Name;
       }
    }
}
