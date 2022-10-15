using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace strong
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
            bool[] graphVisited = new bool[nodeCount + 1];

            LinkedList<long>[] graphReverse = BuildGraphReverse(nodeCount, edges);
            bool[] graphVisitedReverse = new bool[nodeCount + 1];

            Stack<long> reverse = StackReverse(graphReverse,graphVisitedReverse);
            long count = 0;

            while (reverse.Count != 0)
            {
                if (Explore(graph, reverse.Pop(), graphVisited))
                    count++;
            }
            Console.WriteLine(count);
        }

        private static Stack<long> StackReverse(LinkedList<long> [] graphReverse,bool [] graphVisitedReverse)
        {
            Stack<long> postVisit = new Stack<long>(graphReverse.Length);

            for (int i = 1; i < graphReverse.Length; i++)
                if (!graphVisitedReverse[i])
                    Explore2(i,graphReverse,graphVisitedReverse, postVisit);

            return postVisit;
        }

        static void Explore2(long i, LinkedList<long>[] graphReverse, bool[] graphVisitedReverse,Stack<long> postVisit)
        {
            graphVisitedReverse[i] = true;

            foreach (var j in graphReverse[i])
            {
                if (!graphVisitedReverse[j])
                    Explore2(j,graphReverse,graphVisitedReverse,postVisit);
            }
            postVisit.Push(i);
        }

        private static bool Explore(LinkedList<long>[] graph, long v,bool[] graphVisited)
        {
            if (graphVisited[v])
                return false;
            else
            {
                Explore1(v,graph,graphVisited);
                return true;
            }
        }

        static void Explore1(long node, LinkedList<long>[] graph, bool[] graphVisited)
        {
            graphVisited[node] = true;
            foreach (var a in graph[node])
                if (!graphVisited[a])
                    Explore1(a,graph,graphVisited);
        }

        private static LinkedList<long>[] BuildGraphReverse(long nodeCount, long[][] edges)
        {
            var graph = new LinkedList<long> [nodeCount + 1];

            for (int i = 0; i < graph.Length; i++)
                graph[i] = new LinkedList<long>();

            for (int i = 0; i < edges.Length; i++)

            {
                long idxSrc = edges[i][1];
                long idxTgt = edges[i][0];

                graph[idxSrc].AddLast(idxTgt);
            }

            return graph;
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
