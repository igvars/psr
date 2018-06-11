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
        public String shortestPath(List<Node> originalNodes, List<Edge> originalEdges)
        {
            List<List<double>> matrix = new List<List<double>>();
            List<double> row = new List<double>();
        
            for (int i = 0; i < originalNodes.Count; i++)
            {
                row = new List<double>();
                for (int j = 0; j < originalNodes.Count; j++)
                {
                    row.Add(0);
                }
                matrix.Add(row);
            }
            //convert edges to matrix
            foreach (var edge in originalEdges)
            {
                int resultStartIndex = originalNodes.FindIndex(node => node.ToString().Equals(edge.StartNode.ToString()));
                int resultEndIndex = originalNodes.FindIndex(node => node.ToString().Equals(edge.EndNode.ToString()));
                matrix[resultStartIndex][resultEndIndex] = edge.Length;
            }
            
            string start = originalNodes[0].ToString();
            string finish = originalNodes[originalNodes.Count - 1].ToString();

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
                        if (matrix[minindex][i] > 0)
                        {
                            temp = min + matrix[minindex][i];
                            if (temp < d[i])
                            {
                                d[i] = temp;
                            }
                        }
                    }
                    v[minindex] = 0;
                }
            } while (minindex < 10000);

            return d.ToString();
        }
    }
}
