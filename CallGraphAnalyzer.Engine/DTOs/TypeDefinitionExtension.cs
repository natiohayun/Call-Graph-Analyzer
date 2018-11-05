using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallGraphAnalyzer.Engine.DTOs
{
    public class TypeDefinitionExtension
    {
        public TypeDefinition typeDefinition { set; get; }
        public List<MethodDefinitionExtension> GetMethods()
        {
            List<MethodDefinitionExtension> methodDefinitionExtensionList = new List<MethodDefinitionExtension>();
            foreach (var method in typeDefinition.Methods)
                methodDefinitionExtensionList.Add(new MethodDefinitionExtension() { methodDefinition = method });
            return methodDefinitionExtensionList;
        }

        public List<FieldDefinitionExtension> GetFieldsDefinition(string projectNamespace)
        {
            List<FieldDefinitionExtension> list = new List<FieldDefinitionExtension>();
            foreach (var field in typeDefinition.Fields)
            {
                if (field.FieldType.Namespace == projectNamespace)
                    list.Add(new FieldDefinitionExtension() { fieldDefinition = field });
            }
            return list;
        }
       
    
    }
}
