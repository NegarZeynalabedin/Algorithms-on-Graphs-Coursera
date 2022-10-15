using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mazeexit
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

            string lastLine = Console.ReadLine();
            string[] lastLineSpl = lastLine.Split();
            long StartNode = long.Parse(lastLineSpl[0]);
            long EndNode = long.Parse(lastLineSpl[1]);

            bool[] graphVisited = new bool[nodeCount + 1];
            LinkedList<long> [] graphNeighbors = BuildGraphNeighbors(nodeCount, edges);

            Explore(graphVisited, graphNeighbors, StartNode);

            if (graphVisited[EndNode])
                Console.WriteLine("1");
            else
                Console.WriteLine("0");
        }

        static void Explore(long node, bool[] graphVisited, LinkedList<long>[] graphNeighbors)
        {
            graphVisited[node] = true;
            foreach (var a in graphNeighbors[node])
                if (!graphVisited[a])
                    Explore(a, graphVisited, graphNeighbors);
        }

        public static void Explore(bool [] graphVisited,LinkedList<long>[] graphNeighbors,
            long startNode)
        {
            Explore(startNode, graphVisited, graphNeighbors);
        }

        private static LinkedList<long>[] BuildGraphNeighbors(long nodeCount, long[][] edges)
        {
            var graph = new LinkedList<long> [nodeCount + 1];

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
    }
}
