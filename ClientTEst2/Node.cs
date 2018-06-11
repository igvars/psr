using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTEst2
{
   public class Node
    {
        public Point Point { get; set; }

        public Node()
        {

        }


        override
        public string ToString()
        {
            return String.Format("{0},{1}", Point.X, Point.Y);
        }
    }
}
