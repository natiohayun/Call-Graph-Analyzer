using CallGraphAnalyzer.Engine.DTOs;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CallGraphAnalyzer.Engine.Interfaces
{
    public interface IActions<T,K>
    {
        List<T> BuildTypes(K moduleDefinition);

        void Run(List<T> source);

        List<BaseGraphType> ConvertData(List<GraphNode> nodes); 
    }
}
