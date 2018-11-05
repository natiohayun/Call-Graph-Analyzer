using CallGraphAnalyzer.Engine.DTOs;
using CallGraphAnalyzer.Engine.Interfaces;
using Mono.Cecil;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallGraphAnalyzer.Engine.Workers
{
    public class CBOAction : IActions<TypeDefinitionExtension, ModuleDefinition>
    {
        public List<TypeDefinitionExtension> BuildTypes(ModuleDefinition moduleDefinition)
        {
            try
            {
                List<TypeDefinitionExtension> root = new List<TypeDefinitionExtension>();

                foreach (TypeDefinition type in moduleDefinition.Types)
                {
                    if (type.IsClass && type.Name != "<Module>")
                    {
                        root.Add( new TypeDefinitionExtension() { typeDefinition = type });
                    }
                }
                return root;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void Run(List<TypeDefinitionExtension> source)
        {
            try
            {
                List<GraphNode> graphs = new List<GraphNode>();
                foreach (var node in source)
                {

                    GraphNode cbo = new GraphNode();
                    if (node.typeDefinition.IsInterface)
                        cbo.nodeType = NodeType.Interface;
                    else if (node.typeDefinition.IsAbstract)
                        cbo.nodeType = NodeType.AbstractClass;
                    else cbo.nodeType = NodeType.Class;
                    cbo.Name = node.typeDefinition.Name;
                    cbo.Namespace = node.typeDefinition.Namespace;
                    cbo.dependencyType = new List<DependencyType>();
                    cbo.Childrens = new List<GraphNode>();
                    var fields = node.GetFieldsDefinition(cbo.Namespace);
                    foreach (var field in fields)
                    {
                        if (field.fieldDefinition.DeclaringType.Name != cbo.Name)
                        {
                            var exist = cbo.Childrens.Where(x => x.Name == field.fieldDefinition.DeclaringType.Name).FirstOrDefault();
                            if (exist == null)
                            {
                                GraphNode childNode = new GraphNode();
                                childNode.Name = field.fieldDefinition.DeclaringType.Name;
                                if (field.fieldDefinition.DeclaringType.IsAbstract)
                                    childNode.nodeType = NodeType.AbstractClass;
                                else if (field.fieldDefinition.DeclaringType.IsClass)
                                    childNode.nodeType = NodeType.Class;
                                childNode.dependencyType = new List<DependencyType>();
                                childNode.dependencyType.Add(DependencyType.ClassAttribute);
                                cbo.Childrens.Add(childNode);
                            }
                            else if (!exist.dependencyType.Contains(DependencyType.ClassAttribute))
                                exist.dependencyType.Add(DependencyType.ClassAttribute);
                        }
                    }

                    var methods = node.GetMethods();
                    foreach (var method in methods)
                    {

                        var returnType = method.GetReturnType(cbo.Namespace);
                        if (returnType != null)
                        {
                            if (returnType.typeReference.Name != cbo.Name)
                            { 
                              var exist = cbo.Childrens.Where(x => x.Name == returnType.typeReference.Name).FirstOrDefault();
                            if (exist == null)
                            {
                                GraphNode childNode = new GraphNode();
                                if (returnType.IsList)
                                    childNode.Name = returnType.ListTypeName;
                                else childNode.Name = returnType.typeReference.Name;

                                childNode.nodeType = NodeType.Class;
                                childNode.dependencyType = new List<DependencyType>();
                                childNode.dependencyType.Add(DependencyType.FunctionReturnValue);
                                cbo.Childrens.Add(childNode);
                            }
                            else if (!exist.dependencyType.Contains(DependencyType.FunctionReturnValue))
                                exist.dependencyType.Add(DependencyType.FunctionReturnValue);
                        }
                        }
                        var funcationParameters = method.GetFuncationParameters(cbo.Namespace);
                        foreach (var parameter in funcationParameters)
                        {
                            if (parameter.parameterDefinition.ParameterType.Name != cbo.Name)
                            {
                                var exist = cbo.Childrens.Where(x => x.Name == parameter.parameterDefinition.ParameterType.Name).FirstOrDefault();
                                if (exist == null)
                                {
                                    GraphNode childNode = new GraphNode();
                                    if (parameter.IsList)
                                        childNode.Name = parameter.ListTypeName;
                                    else childNode.Name = parameter.parameterDefinition.ParameterType.Name;
                                    childNode.nodeType = NodeType.Class;
                                    childNode.dependencyType = new List<DependencyType>();
                                    childNode.dependencyType.Add(DependencyType.FunctionParameter);
                                    cbo.Childrens.Add(childNode);

                                }
                                else if (!exist.dependencyType.Contains(DependencyType.FunctionParameter))
                                    exist.dependencyType.Add(DependencyType.FunctionParameter);
                            }
                        }
                        var funcationAttribute = method.GetFuncationAttributes(cbo.Namespace);
                        foreach (var attribute in funcationAttribute)
                        {
                            if (attribute.parameterDefinition.ParameterType.Name != cbo.Name)
                            {
                                var exist = cbo.Childrens.Where(x => x.Name == attribute.parameterDefinition.ParameterType.Name).FirstOrDefault();
                                if (exist == null)
                                {
                                    GraphNode childNode = new GraphNode();
                                    if (attribute.IsList)
                                        childNode.Name = attribute.ListTypeName;
                                    else childNode.Name = attribute.parameterDefinition.ParameterType.Name;
                                    childNode.Name = attribute.parameterDefinition.ParameterType.Name;
                                    childNode.nodeType = NodeType.Class;
                                    childNode.dependencyType = new List<DependencyType>();
                                    childNode.dependencyType.Add(DependencyType.FunctionAttribute);
                                    cbo.Childrens.Add(childNode);
                                }
                                else if (!exist.dependencyType.Contains(DependencyType.FunctionAttribute))
                                    exist.dependencyType.Add(DependencyType.FunctionAttribute);
                            }
                        }
                    }
                    graphs.Add(cbo);
                }
                FileActions a = new FileActions();
                HtmlReport report = new HtmlReport();
                
                var data = BuildBaseGraphs(graphs);
              
                report.BuildHTMLReport(AnalyzerType.CBO, data);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        //public List<BaseGraphType> BuildBaseGraphs2(List<GraphNode> data)
        //{
        //    int id = 0;
        //    List<BaseGraphType> graph = new List<BaseGraphType>();
        //    foreach (var item in data)
        //    {
        //        Node node = new Node(id++, item.Name,item.dependencyType, item.nodeType);
        //        id++;
        //        if (item.Childrens.Count > 0)
        //            id = ExpandChilderns(item, node, id);
        //        Node current = node;
        //        graph.Add(node);
        //        foreach (var child in item.Childrens)
        //        {
                 
        //            Node childNode = new Node(id ,child.Name, child.dependencyType, child.nodeType);
        //            graph.Add(childNode);
        //            id++;
        //            Edge edge = new Edge(current, childNode, "");
        //            graph.Add(edge);
        //        }

        //    }

        //    return graph; 
        //}
        //public int ExpandChilderns(GraphNode source , BaseGraphType root , List<BaseGraphType> graph , int id )
        //{
        //    foreach(var child in source.Childrens)
        //    {
        //        var node = graph.FirstOrDefault(x => x.label == child.Name) as Node;
        //        if(node == null)
        //           node = new Node(id++, child.Name, child.dependencyType, child.nodeType);
        //        Edge edge = new Edge(root as Node, node, "");
         
        //        if (child.Childrens.Count()> 0)
        //            id = ExpandChilderns(child, node, graph, id);
        //        graph.Add(node);
        //        graph.Add(edge);
        //    }
        //    return id;

        //}
        public List<BaseGraphType> BuildBaseGraphs(List<GraphNode> data )
        {
            int id = 1;
            List<BaseGraphType> graph = new List<BaseGraphType>();
            foreach (var item in data)
            {
                Node node = new Node(id, item.Name, item.dependencyType, item.nodeType);
                id++;
           
                Node current = node;

                graph.Add(node);
                foreach (var child in item.Childrens)
                {

                    Node childNode = new Node(id, child.Name, child.dependencyType, child.nodeType);
                    graph.Add(childNode);
                    id++;
                    Edge edge = new Edge(current, childNode, "");
                    graph.Add(edge);
                }

            }

            return graph;
        }
    }
}
