using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderOfCourse
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

            LinkedList<long>[] graph =BuildGraph(nodeCount, edges);
            bool[] visited = new bool[nodeCount + 1];

            long[] answer = Sort(graph, visited);
            Console.WriteLine(string.Join(" ", answer.Select(x => x.ToString()).ToArray()));
        }

        private static long[] Sort(LinkedList<long> [] graph,bool [] visited)
        {
            Stack<long> postVisit = new Stack<long>(graph.Length);

            for (int i = 1; i < graph.Length; i++)
                if (!visited[i])
                    Explore(i,visited,postVisit,graph);

            long[] answer = new long[postVisit.Count];
            int idx = 0;
            while (postVisit.Count != 0)
            {
                answer[idx] = postVisit.Pop();
                idx++;
            }

            return answer;
        }

        static void Explore(long i,bool[]visited,Stack<long> postVisit, LinkedList<long>[]graph)
        {
            visited[i] = true;

            foreach (var j in graph[i])
            {
                if (!visited[j])
                    Explore(j,visited, postVisit, graph);
            }
            postVisit.Push(i);
        }

        private static LinkedList<long> [] BuildGraph(long nodeCount, long[][] edges)
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
