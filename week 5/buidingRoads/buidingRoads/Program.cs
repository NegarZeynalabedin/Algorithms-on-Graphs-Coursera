using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace buidingRoads
{
    class SimplePriorityQueue
    {
        const double NotHere = int.MaxValue;
        public double[] values;
        public SimplePriorityQueue(int size)
        {
            values = new double[size];
            for (int i = 0; i < values.Length; i++)
                values[i] = NotHere;
        }

        public void Enqueue(long n, double v)
        {
            values[n] = v;
            Count++;
        }

        public int Count = 0;

        public long Dequeue()
        {
            int minIndex = 0;
            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] < values[minIndex] && values[i] != NotHere)
                {
                    minIndex = i;
                }
            }
            long ret = minIndex;
            values[minIndex] = NotHere;
            Count--;
            return ret;
        }

        public void UpdatePriority(int n, double v)
        {
            values[n] = v;
        }

        public bool Contains(long idx)
        {
            return values[idx] != NotHere;
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

            List<List<long>> graphX = BuildGraphX(pointCount, points);
            List<List<double>> graphY = BuildGraphY(pointCount, points);

            double roads = Prim(graphX,graphY, (int)pointCount);

            Console.WriteLine(roads.ToString("0.0000000"));
        }

        private static double Prim(List<List<long>> graphX, List<List<double>> graphY, int count)
        {
            double[] distRoads = Enumerable.Repeat(double.MaxValue, count).ToArray();
            distRoads[0] = 0;

            SimplePriorityQueue  prioQ = new SimplePriorityQueue (count);
            for (int i = 0; i < count; i++)
                if (i == 0)
                    prioQ.Enqueue(i, 0);
                else
                    prioQ.Enqueue(i, double.MaxValue);

            while (prioQ.Count != 0)
            {
                var node = prioQ.Dequeue();

                for(int i=0;i< graphX[(int)node].Count;i++)
                {
                    //prioQ.Contains(graphX[(int)node][i]) &&
                    if (prioQ.Contains(graphX[(int)node][i]) && distRoads[graphX[(int)node][i]] > graphY[(int)node][i])
                    {
                        distRoads[graphX[(int)node][i]] = graphY[(int)node][i];
                        prioQ.UpdatePriority((int)graphX[(int)node][i], (double)distRoads[graphX[(int)node][i]]);
                    }
                }
            }

            return Math.Round(distRoads.Sum(), 9);
        }

        private static List<List<long>> BuildGraphX(long pointCount, long[][] points)
        {
            var graph = new List<List<long>>();

            for (int i = 0; i < pointCount; i++)
            {
                graph.Add(new List<long>());

                for (int j = 0; j < pointCount; j++)
                {
                    if (i != j)
                    {
                        double distance = ComputeDist(points[i], points[j]);
                        graph[i].Add((long)j);
                    }
                }
            }

            return graph;
        }

        private static List<List<double>> BuildGraphY(long pointCount, long[][] points)
        {
            var graph = new List<List<double>>();

            for (int i = 0; i < pointCount; i++)
            {
                graph.Add(new List<double>());

                for (int j = 0; j < pointCount; j++)
                {
                    if (i != j)
                    {
                        double distance = ComputeDist(points[i], points[j]);
                        graph[i].Add(distance);
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
