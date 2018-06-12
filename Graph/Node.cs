using System;
using System.Drawing;
using System.Xml.Serialization;

namespace Graph {
    [Serializable]

    public class Node
    {
        public Point Point { get; set; }


        public double? MinCostToStart { get; set; }
        public Node NearestToStart { get; set; }
        public bool Visited { get; set; }
        public double StraightLineDistanceToEnd { get; set; }
        public Node Start { get; set; }
        public Node End { get; set; }
        public int NodeVisits { get;  set; }

        override
        public string ToString()
        {
            return String.Format("{0}, {1}", Point.X, Point.Y);
        }
    }
}
