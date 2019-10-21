using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ka_task_2
{
    class Program
    {
        private static readonly string inputFileName = @"in.txt";
        private static readonly string outputFileName = @"out.txt";

        static void Main()
        {
            //a ti lubish kotikov?
            File.Delete(outputFileName);
            var input = File.ReadAllLines(inputFileName);
            var graph = new List<int>[int.Parse(input[0])];
            for (var node = 0; node < graph.Length; node++)
            {
                var line = input[node + 1].Trim().Split(' ');
                var incidentNodesCount = line.Length - 1;
                var incidentNodes = new List<int>(incidentNodesCount);
                for (var i = 0; i < incidentNodesCount; i++)
                    incidentNodes.Add(int.Parse(line[i]) - 1);
                graph[node] = incidentNodes;
            }

            if (IsBipartite(graph, out var bipartiteNodes))
            {
                var output = new StringBuilder();
                output.AppendLine("Y");

                for (var node = 0; node < graph.Length; node++)
                    if ((bool)bipartiteNodes[node])
                        output.Append($"{node + 1} ");
                output.AppendLine("0");

                for (var node = 1; node < graph.Length; node++)
                    if ((bool)!bipartiteNodes[node])
                        output.Append($"{node + 1} ");
                output.Append(0);
                File.WriteAllText(outputFileName, output.ToString());
            }
            else
                File.WriteAllText(outputFileName, "N");
        }

        private static bool IsBipartite(List<int>[] graph, out bool?[] parts)
        {
            parts = new bool?[graph.Length];
            parts[0] = true;
            var stack = new Stack<int>();
            stack.Push(0);
            while (stack.Any())
            {
                var node = stack.Pop();
                foreach (var incidentNode in graph[node])
                {
                    if (parts[incidentNode] == parts[node]) return false;
                    if (parts[incidentNode] == null)
                    {
                        parts[incidentNode] = !parts[node];
                        stack.Push(incidentNode);
                    }
                }
            }
            return !parts.Contains(null);
        }
        //ya ochen' lublu kotikov
    }
    
}
