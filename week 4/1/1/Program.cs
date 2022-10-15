using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace _1
{
    class SimplePriorityQueue
    {
        public long[] values;
        public SimplePriorityQueue(int size)
        {
            values = new long[size];
            for (int i = 0; i < values.Length; i++)
                values[i] = long.MaxValue;
        }

        public void Enqueue(long n, long v)
        {
            values[n-1] = v;
            Count++;
        }

        public int Count = 0;

        public long Dequeue()
        {
            int minIndex = 0;
            for(int i=1;i<values.Length; i++)
            {
                if (values[i] < values[minIndex])
                {
                    minIndex = i;
                }
            }
            long ret = minIndex;
            values[minIndex] = long.MaxValue;
            Count--;
            return ret+1;
        }

        public void UpdatePriority(int n, long v)
        {
            values[n-1] = v;
        }

    }

    class Program
    {
        static void Main2(string[] args)
        {
            string inputfile = args[0];
            long edgeCount = File.ReadAllLines(inputfile).Length - 2;
            StreamReader reader = new StreamReader(inputfile, Encoding.UTF8);
            Console.SetIn(reader);
            Main2(new string[] { edgeCount.ToString() });
        }

        static void Main(string[] args)
        {
            string firstLine = Console.ReadLine();
            string[] firstLineSpl = firstLine.Split();
            long nodeCount = long.Parse(firstLineSpl[0]);
            long edgeCount;
            if (args.Length > 0)
                edgeCount = long.Parse(args[0]);
            else
                edgeCount = long.Parse(firstLineSpl[1]);

            long[][] edges = new long[edgeCount][];
            for (int i = 0; i < edgeCount; i++)
            {
                string edge = Console.ReadLine();
                string[] edgeSpl = edge.Split();
                edges[i] = new long[3];
                edges[i][0] = long.Parse(edgeSpl[0]);
                edges[i][1] = long.Parse(edgeSpl[1]);
                edges[i][2] = long.Parse(edgeSpl[2]);
            }


            string lastLine = Console.ReadLine();
            string[] lastLineSpl = lastLine.Split();
            long startNode = long.Parse(lastLineSpl[0]);
            long endNode = long.Parse(lastLineSpl[1]);

            List<List<long>> graph = BuildGraphTarget(nodeCount, edges);
            List<List<long>> graphWeight = BuildGraphWeight(nodeCount, edges);
            long minCost = Dijkstra(graph,graphWeight, nodeCount, startNode, endNode);
            Console.WriteLine(minCost);
        }

        private static long Dijkstra(List<List<long>> graph2, List<List<long>> weight, long nodeCount, long startNode, long endNode)
        {
            long[] dist = Enumerable.Repeat(long.MaxValue, (int)nodeCount + 1).ToArray();
            dist[startNode] = 0;

            SimplePriorityQueue distQueue = new SimplePriorityQueue((int)nodeCount);
            for (int i = 0; i < nodeCount; i++)
                if (i + 1 == startNode)
                    distQueue.Enqueue(i + 1, 0);
                else
                    distQueue.Enqueue(i + 1, long.MaxValue);

            bool[] processed = new bool[nodeCount + 1];
            processed[startNode] = true;

            while (distQueue.Count != 0)
            {
                var u = distQueue.Dequeue();
                processed[u] = true;

                for (int i = 0; i < graph2[(int)u].Count; i++)
                {
                    var targetNeighbors = graph2[(int)u];
                    var weightNeighbors = weight[(int)u];

                    if (dist[u] + weightNeighbors[i] < dist[targetNeighbors[i]] && !processed[targetNeighbors[i]])
                    {
                        dist[targetNeighbors[i]] = dist[u] + weightNeighbors[i];
                        distQueue.UpdatePriority((int)targetNeighbors[i], dist[targetNeighbors[i]]);
                    }
                    else if (dist[u] + weightNeighbors[i] < dist[targetNeighbors[i]] && processed[targetNeighbors[i]])
                        return -1;
                }
                if (processed[endNode])
                    return dist[endNode];
            }
            return -1;
        }

        private static List<List<long>> BuildGraphTarget(long nodeCount, long[][] edges)
        {
            var graph = new List<List<long>>();

            for (int i = 0; i < nodeCount + 1; i++)
                graph.Add(new List<long>());

            for (int i = 0; i < edges.Length; i++)
            {
                long idxSource = edges[i][0];
                long idxTarget = edges[i][1];

                graph[(int)idxSource].Add(idxTarget);
            }
            return graph;
        }

        private static List<List<long>> BuildGraphWeight(long nodeCount, long[][] edges)
        {
            var graph = new List<List<long>>();

            for (int i = 0; i < nodeCount + 1; i++)
                graph.Add(new List<long>());

            for (int i = 0; i < edges.Length; i++)
            {
                long idxSource = edges[i][0];
                long weight = edges[i][2];

                graph[(int)idxSource].Add(weight);
            }
            return graph;
        }
    }
}
