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
        public List<double> shortestPath(List<Node> originalNodes, double[,] matrix)
        {
            List<double> d = new List<double>(); // min length
            List<double> v = new List<double>(); // visited verteces
            double temp;
            double min;
            int minindex;

            //Initialization verteces and length
            for (int i = 0; i < originalNodes.Count; i++)
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
                for (int i = 0; i < originalNodes.Count; i++)
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
                    for (int i = 0; i < originalNodes.Count; i++)
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

            return d;
        }
    }
}
