using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;

namespace Graph
{
    public class Dijkstra
    {
        public string shortestPath(List<Node> Nodes, List<Edge> Edges)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            double[,] matrix = new double[Nodes.Count, Nodes.Count];

            for (int i = 0; i < Nodes.Count; i++)
            {
                for (int j = 0; j < Nodes.Count; j++)
                {
                    if (i == j)
                    {
                        matrix[i, j] = 0;
                    }
                    else
                    {
                        for (int k = 0; k < Edges.Count; k++)
                        {
                            if ((Edges[k].StartNode.ID == i && Edges[k].EndNode.ID == j)
                                || (Edges[k].StartNode.ID == j && Edges[k].EndNode.ID == i))
                            {
                                matrix[i, j] = Edges[k].computeLength();
                            }
                        }
                    }
                }
            }

            List<double> d = new List<double>(); // min length
            List<double> v = new List<double>(); // visited verteces
            double temp;
            double min;
            int minindex;

            //Initialization verteces and length
            for (int i = 0; i < Nodes.Count; i++)
            {
                d.Add(10000);
                v.Add(1);
            }
            //TODO: startFrom
            d[0] = 0;
            // Algorithm step
            do
            {
                minindex = 10000;
                min = 10000;
                for (int i = 0; i < Nodes.Count; i++)
                { // If vertex isn't visited and weght less than min
                    if ((v[i] == 1) && (d[i] < min))
                    { // Reassign values
                        min = d[i];
                        minindex = i;
                    }
                }
                // Add found min weigth to current vertex's weight
                // and compare with current min weight of vertex
                if (minindex != 10000)
                {
                    for (int i = 0; i < Nodes.Count; i++)
                    {
                        if (matrix[minindex, i] > 0)
                        {
                            temp = min + matrix[minindex, i];
                            if (temp < d[i])
                            {
                                d[i] = temp;
                            }
                        }
                    }
                    v[minindex] = 0;
                }
            } while (minindex < 10000);

            stopWatch.Stop();

            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:0000}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds);
            return elapsedTime;
        }
    }
}
