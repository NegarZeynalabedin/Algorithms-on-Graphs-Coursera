using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clustering
{
    class DisjointSets
    {
        long[] rank, parent;
        int n;

        public DisjointSets(int n)
        {
            rank = new long[n];
            parent = new long[n];
            this.n = n;
            MakeSet();
        }

        private void MakeSet()
        {
            for (int i = 0; i < n; i++)
                parent[i] = i;
        }

        public long Find(long x)
        {
            if (parent[x] != x)
                parent[x] = Find(parent[x]);

            return parent[x];
        }

        public void Union(long x, long y)
        {
            long RootA = Find(x), RootB = Find(y);

            if (RootA == RootB)
                return;

            if (rank[RootA] < rank[RootB])
                parent[RootA] = RootB;

            else if (rank[RootB] < rank[RootA])
                parent[RootB] = RootA;

            else
            {
                parent[RootB] = RootA;
                rank[RootA] = rank[RootA] + 1;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            long pointCount = long.Parse(Console.ReadLine());
            long[][] points = new long[pointCount][];

            for (int i = 0; i < pointCount; i++)
            {
                string input = Console.ReadLine();
                string[] inputSpl = input.Split();
                points[i] = new long[2];
                points[i][0] = long.Parse(inputSpl[0]);
                points[i][1] = long.Parse(inputSpl[1]);
            }

            long clusterCount = long.Parse(Console.ReadLine());

            List<long> graphX = BuildGraphX(pointCount, points);
            List<long> graphY = BuildGraphY(pointCount, points);
            List<double> graphZ = BuildGraphZ(pointCount, points);

            double answer = Kruskal(graphX,graphY,graphZ, (int)pointCount, clusterCount);

            Console.WriteLine(Math.Round(answer, 6));
        }

        private static double Kruskal(List<long> graphX, List<long> graphY, List<double> graphZ, int count, long cluster)
        {
            DisjointSets ds = new DisjointSets(count);

            var graph = graphZ.OrderBy(d => d).ToList();

            List<long> x = new List<long>();
            List<long> y = new List<long>();
            List<double> z = new List<double>();

            int countCopy = count;

            int i;
            for (i = 0; i < graph.Count; i++)
            {
                if (ds.Find(graphX[i]) != ds.Find(graphY[i]))
                {
                    x.Add();
                    y.Add();
                    z.Add(graph[i]);

                    ds.Union(graphX[i], graphY[i]);
                    countCopy--;
                }

                if (countCopy == cluster)
                    break;
            }

            for (int j = i + 1; j < graph.Count; j++)
            {
                if (ds.Find(graphX[j]) != ds.Find(graphY[j]))
                    return graphZ[j];
            }

            return 0;
        }

        private static List<double> BuildGraphZ(long pointCount, long[][] points)
        {
            var graph = new List<double>();

            for (int i = 0; i < pointCount; i++)
            {
                for (int j = 0; j < pointCount; j++)
                {
                    if (j != i)
                    {
                        double distance = ComputeDist(points[i], points[j]);
                        graph.Add(distance);
                    }
                }
            }

            return graph;
        }

        private static List<long> BuildGraphY(long pointCount, long[][] points)
        {
            var graph = new List<long>();

            for (int i = 0; i < pointCount; i++)
            {
                for (int j = 0; j < pointCount; j++)
                {
                    if (j != i)
                    {
                        graph.Add((long)j);
                    }
                }
            }

            return graph;
        }

        private static List<long> BuildGraphX(long pointCount, long[][] points)
        {
            var graph = new List<long>();

            for (int i = 0; i < pointCount; i++)
            {
                for (int j = 0; j < pointCount; j++)
                {
                    if (j != i)
                    {
                        graph.Add((long)i);
                    }
                }
            }

            return graph;
        }

        private static double ComputeDist(long[] v1, long[] v2)
        {
            long sourceX = v1[0];
            long sourceY = v1[1];

            long targetX = v2[0];
            long targetY = v2[1];

            return Math.Sqrt(Math.Pow(sourceX - targetX, 2) + Math.Pow(sourceY - targetY, 2));
        }
    }
}
