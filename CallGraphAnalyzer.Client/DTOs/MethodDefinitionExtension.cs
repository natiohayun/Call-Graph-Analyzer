using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallGraphAnalyzer.Engine.DTOs
{
   public class MethodDefinitionExtension
    {
        public MethodDefinition methodDefinition { set; get; }

        public List<ParameterDefinitionExtension> GetFuncationParameters()
        {
            List<ParameterDefinitionExtension> parameterDefinitionList = new List<ParameterDefinitionExtension>();
            foreach(var parameter in methodDefinition.Parameters)
            {
                if (!parameter.ParameterType.ToString().StartsWith("System"))
                    parameterDefinitionList.Add(new ParameterDefinitionExtension() { parameterDefinition = parameter });
            }

            return parameterDefinitionList;
        }

        public TypeReferenceExtension GetReturnType()
        {
            if (!methodDefinition.ReturnType.FullName.StartsWith("System"))
                return new TypeReferenceExtension() { typeReference = methodDefinition.ReturnType };
            else return null;
        }
        public List<ParameterDefinitionExtension> GetFuncationVariables()
        {
            List<ParameterDefinitionExtension> parameterDefinitionList = new List<ParameterDefinitionExtension>();

            if (methodDefinition.HasBody && methodDefinition.Body.HasVariables)
            {
                foreach (var parameter in methodDefinition.Parameters)
                {
                  if (!parameter.ParameterType.ToString().StartsWith("System"))
                    parameterDefinitionList.Add(new ParameterDefinitionExtension() { parameterDefinition = parameter });
                }
            }
            return parameterDefinitionList;
        }
    }
}
