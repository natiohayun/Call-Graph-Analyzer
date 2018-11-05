
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Mono;
using ICSharpCode;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;
using ICSharpCode.Decompiler.TypeSystem;
using Mono.Cecil;
using CallGraphAnalyzer.Engine.Interfaces;
using CallGraphAnalyzer.Engine.DTOs;
using CallGraphAnalyzer.Engine.Workers;

namespace CallGraphAnalyzer.Console.Client
{
    class Program
    {
       
        static void Main(string[] args)
        {
           
            var file = @"C:\temp\Chatroom.exe";


            var fileAction = new FileActions();
            System.Console.WriteLine("Welcome to Call Graph Analyzer");
            var path = FileMenu();
            ModuleDefinition module = fileAction.LoadFile(path);
            if (module != null)
            {
                var option = DisplayManu();
                switch (option)
                {
                    case 1:
                        {
                            var action = new CBOAction();
                            var root = action.BuildTypes(module);
                            action.Run(root);
                            break;
                        }
                    default:
                        Environment.Exit(0);
                        break;
                } 
              
            }
            else System.Console.WriteLine("Error while trying to load file");
        }
       private static string FileMenu ()
        {
            return  @"C:\temp\Chatroom.exe";
            System.Console.WriteLine("Please select a file to analyze :");
            System.Console.WriteLine("Type EXIT to return:");
            while (true)
            {
                var file = System.Console.ReadLine();
                if (file.ToLower() == "exit")
                    Environment.Exit(0);
                else
                {
                    if (System.IO.File.Exists(file))
                        return file;
                    else System.Console.WriteLine("Cant locate file please try again:");
                }

            }
        }

         private static int DisplayManu()
        {
            return 1;
            int option = 0;
            List<int> validOption = new List<int> { 1, 5 };
            System.Console.WriteLine("Please select analyze type:");
            System.Console.WriteLine("1.CBO");
            System.Console.WriteLine("5.Exit");
            while (true)
            {
                var type = System.Console.ReadLine();
                if (int.TryParse(type, out option))
                {
                   if(validOption.Contains(option))
                        return option;
                }
                else System.Console.WriteLine("Wrone input");
            }
        }
      

    }
}
