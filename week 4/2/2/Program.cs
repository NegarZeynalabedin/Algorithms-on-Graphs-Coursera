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
            string inputSTR = Console.ReadLine();
            string[] inputSPL = inputSTR.Split();
            int nodesCount = int.Parse(inputSPL[0]);
            int edgesCount = int.Parse(inputSPL[1]);

            long[][] edges = new long[edgesCount][];
            for(int i = 0; i < edgesCount; i++)
            {
                string inp = Console.ReadLine();
                string[] inpSPL = inp.Split();
                edges[i] = new long[3];
                edges[i][0] = long.Parse(inpSPL[0]);
                edges[i][1] = long.Parse(inpSPL[1]);
                edges[i][2] = long.Parse(inpSPL[2]);
            }

            List<long> graphSource = BuildGraphSource(edgesCount, edges);
            List<long> graphTarget = BuildGraphTarget(edgesCount, edges);
            List<long> graphWeight = BuildGraphWeight(edgesCount, edges);

            long negativeCycle = Bellmanford(graphSource,graphTarget,graphWeight, nodesCount);

            Console.WriteLine(negativeCycle);
        }

        private static long Bellmanford(List<long> graphSource, List<long> graphTarget, List<long> graphWeight, int nodesCount)
        {
            long[] dist = Enumerable.Repeat((long)100000, (int)nodesCount + 1).ToArray();
            dist[1] = 0;

            for (int i = 0; i < nodesCount - 1; i++)
            {
                for(int j= 0;j<graphSource.Count;j++)
                    Relax( graphSource[j], graphTarget[j], graphWeight[j], dist);
            }

            long sum = dist.Sum();

            for (int j = 0; j < graphSource.Count; j++)
                Relax(graphSource[j], graphTarget[j], graphWeight[j], dist);

            long sumAfterRelax = dist.Sum();

            if (sum != sumAfterRelax)
                return 1;
            else
                return 0;
        }

        private static void Relax(long source, long target, long weight, long[] dist)
        {
            if (dist[source] + weight < dist[target])
                dist[target] = dist[source] + weight;
        }

        private static List<long> BuildGraphWeight(int edgesCount, long[][] edges)
        {
            var graph = new List<long>(edgesCount);

            for (int i = 0; i < edgesCount; i++)
                graph.Add(edges[i][2]);

            return graph;
        }

        private static List<long> BuildGraphTarget(int edgesCount, long[][] edges)
        {
            var graph = new List<long>(edgesCount);

            for (int i = 0; i < edgesCount; i++)
                graph.Add(edges[i][1]);

            return graph;
        }

        private static List<long> BuildGraphSource(int edgesCount, long[][] edges)
        {
            var graph = new List<long>(edgesCount);

            for (int i = 0; i < edgesCount; i++)
                graph.Add(edges[i][0]);

            return graph;
        }
    }
 }
