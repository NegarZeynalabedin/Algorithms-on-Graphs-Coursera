using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace addExitToMaze
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
            long result = 0;
            Thread t = new Thread(
                () => result = countComponent(graph,visited),
                16 * 1024 * 1024
                );
            t.Start();
            t.Join();

            Console.WriteLine(result);
        }

        public static LinkedList<long>[] BuildGraph(long nodeCount, long[][] edges)
        {
            var graph = new LinkedList<long>[nodeCount + 1];

            for (int i = 0; i < graph.Length; i++)
                graph[i] = new LinkedList<long>();

            for (int i = 0; i < edges.Length; i++)
            {
                long idxSrc = edges[i][1];
                long idxTgt = edges[i][0];

                graph[idxSrc].AddLast(idxTgt);
                graph[idxTgt].AddLast(idxSrc);
            }

            return graph;
        }

        private static long countComponent(LinkedList<long>[] graph,bool []visited)
        {
            long count = 0;
            for (int i = 1; i < graph.Length; i++)
            {
                if (!visited[i])
                {
                    Explore(graph, i,visited);
                    count++;
                }
            }
            return count;
        }

        public static void Explore(LinkedList<long>[] graph, long startNode, bool[] visited)
        {
            Explore(startNode,graph,visited);
        }

        static void Explore(long node,LinkedList<long>[] graph, bool[] visited)
        {
            visited[node] = true;
            foreach (var a in graph[node])
                if (!visited[a])
                    Explore(a,graph,visited);
        }
    }
}
