using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1
{
    class Program
    {
        static void Main(string[] args)
        {
            string firstLine = Console.ReadLine();
            string[] firstLineSpl = firstLine.Split();
            long NodeCount = long.Parse(firstLineSpl[0]);
            long EdgeCount = long.Parse(firstLineSpl[1]);

            long[][] edges = new long[EdgeCount][];
            for (int i = 0; i < EdgeCount; i++)
            {
                string edge = Console.ReadLine();
                string[] edgeSpl = edge.Split();
                edges[i] = new long[2];
                edges[i][0] = long.Parse(edgeSpl[0]);
                edges[i][1] = long.Parse(edgeSpl[1]);
            }

            string lastLine = Console.ReadLine();
            string[] lastLineSpl = lastLine.Split();
            long StartNode = long.Parse(lastLineSpl[0]);
            long EndNode = long.Parse(lastLineSpl[1]);

            LinkedList<long>[] graph = BuildGraph(NodeCount, edges);

            long answer=BFS(graph, StartNode, EndNode);
            Console.WriteLine(answer);
        }

        private static long BFS(LinkedList<long>[] graph, long startNode, long endNode)
        {
            long[] dist = new long[graph.Length];

            for (int i = 0; i < dist.Length; i++)
                dist[i] = long.MaxValue;

            Queue<long> queue = new Queue<long>();
            queue.Enqueue(startNode);
            dist[startNode] = 0;

            while (queue.Count != 0)
            {
                long node = queue.Dequeue();
                foreach (var v in graph[node])
                {
                    if (dist[v] == long.MaxValue)
                    {
                        dist[v] = dist[node] + 1;
                        queue.Enqueue(v);
                        if (v == endNode)
                            return dist[endNode];
                    }
                }
            }
            return -1;
        }

        private static LinkedList<long>[] BuildGraph(long nodeCount, long[][] edges)
        {
            var graph = new LinkedList<long>[nodeCount + 1];

            for (int i = 0; i < graph.Length; i++)
                graph[i] = new LinkedList<long>();

            for (int i = 0; i < edges.Length; i++)
            {
                long idxSource = edges[i][0];
                long idxTarget = edges[i][1];

                graph[idxSource].AddLast(idxTarget);
                graph[idxTarget].AddLast(idxSource);
            }

            return graph;
        }
    }
}
