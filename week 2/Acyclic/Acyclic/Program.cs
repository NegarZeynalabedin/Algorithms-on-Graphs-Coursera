using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acyclic
{
    class Program
    {
        static void Main(string[] args)
        {
            string firstLine = Console.ReadLine();
            string[] firstLineSpl = firstLine.Split();
            long nodeCount = long.Parse(firstLineSpl[0]);
            long edgeCount = long.Parse(firstLineSpl[1]);

            long[][] edges = new long[edgeCount][];
            for (int i = 0; i < edgeCount; i++)
            {
                string edge = Console.ReadLine();
                string[] edgeSpl = edge.Split();
                edges[i] = new long[2];
                edges[i][0] = long.Parse(edgeSpl[0]);
                edges[i][1] = long.Parse(edgeSpl[1]);
            }

            LinkedList<long>[] graph = BuildGraph(nodeCount, edges);
            bool[] visited = new bool[nodeCount + 1];
            bool [] inside =new bool [nodeCount+1];

            for (int i = 1; i < graph.Length; i++)
            {
                if (1 == Explore(graph, i, visited, inside))
                {
                    Console.WriteLine("1");
                    return;
                }
                    
            }

            Console.WriteLine("0");
        }

        private static long Explore(LinkedList<long>[] graph, long i,bool [] visited,bool []inside)
        {
            visited[i] = true;

            inside[i] = true;

            foreach (var j in graph[i])
            {
                if (!visited[j])
                {
                    Explore(graph, j,visited,inside);
                }
                else
                if (inside[j])
                    return 1;
            }
            inside[i] = false;
            return 0;
        }

        private static LinkedList<long>[] BuildGraph(long nodeCount, long[][] edges)
        {
            var graph = new LinkedList<long>[nodeCount + 1];

            for (int i = 0; i < graph.Length; i++)
                graph[i] = new LinkedList<long>();

            for (int i = 0; i < edges.Length; i++)
            {
                long idxSrc = edges[i][0];
                long idxTgt = edges[i][1];

                graph[idxSrc].AddLast(idxTgt);
            }

            return graph;
        }
    }
}
