using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2
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

            LinkedList<long>[] graph = BuildGraph(NodeCount, edges);
            long answer=BFS(graph);
            Console.WriteLine(answer);
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

        private static long BFS(LinkedList<long>[] graph)
        {
            long[] color = new long[graph.Length];

            Queue<long> queue = new Queue<long>();
            queue.Enqueue(1);
            color[1] = 2;

            while (queue.Count != 0)
            {
                long node = queue.Dequeue();
                if (color[node] == 2)
                    foreach (var v in graph[node])
                    {
                        if (color[v] == 0)
                        {
                            color[v] = 1;
                            queue.Enqueue(v);
                        }
                        else if (color[v] == 2)
                            return 0;

                    }

                if (color[node] == 1)
                    foreach (var v in graph[node])
                    {
                        if (color[v] == 0)
                        {
                            color[v] = 2;
                            queue.Enqueue(v);
                        }
                        else if (color[v] == 1)
                            return 0;

                    }
            }
            return 1;

        }
    }
}
