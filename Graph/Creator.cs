using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Graph
{
    
   public class Creator 
    {
        List<Node> Nodelist1 = new List<Node>();
        Node[] Nodes; // tablica na wierzcholki
        Random Rand = new Random();
        int NodesCount = 5; // liczba wierzcholkow
        List<int> ConnectedNodesID = new List<int>(); // lista na wierzcholki, ktore dostaly juz krawedz
        public List<Edge> Edges = new List<Edge>(); // lista krawedzi

        public void Load()
        {
            Nodes = new Node[NodesCount];
            // tworzenie wierzcholkow o losowym polozeniu
            for (int i = 0; i < NodesCount; i++)
            {

                Nodes[i] = new Node();
                Nodes[i].Point = new Point(Rand.Next(100, 1000), Rand.Next(100, 500));
                Nodes[i].ID = i;
                Nodelist1.Add(Nodes[i]);
            }

            ConnectedNodesID.Add(Rand.Next(0, Nodes.Length));

            do
            {
                int randConnectedIndex = Rand.Next(0, ConnectedNodesID.Count); // losowy indeks z wierzcholkow, ktore maja przyporzadkowana krawedz
                int randConnectedID = ConnectedNodesID[randConnectedIndex]; // numer konkretnego wierzcholka spod wylosowanego wyzej indeksu
                                                                            // (bedzie to poczatek krawedzi)
                int randID = Rand.Next(0, Nodes.Length); // losowy numer wierzcholka ze wszystkich wierzcholkow (bedzie to koniec krawedzi)

                if (randConnectedID == randID) // skip Edge if StartNode == EndNode
                {
                    continue;
                }

                Edge newEdge = new Edge(Nodes[randConnectedID], Nodes[randID]); // krawedz utworzona z dwoch wylosowanych wierzcholkow

                // sprawdzenie, czy czasem nie mamy juz takiej krawedzi. Jesli nie, to ja dodajemy do listy i rysujemy
                int foundIt = 0;
                for (int i = 0; i < Edges.Count; i++)
                {
                    if (Edges[i].compareEdge(newEdge)) foundIt++;
                }
                if (foundIt == 0)
                {
                    newEdge.ID = Edges.Count; // assign ID to new edge
                    Edges.Add(newEdge);
                }

                // sprawdzamy, czy wylosowany koniec krawedzi jest juz dodany do connectedNodesID. Jesli nie, to go dodajemy
                foundIt = 0;
                for (int i = 0; i < ConnectedNodesID.Count; i++)
                {
                    if (ConnectedNodesID[i] == randID) foundIt++;
                }
                if (foundIt == 0) ConnectedNodesID.Add(randID);
            } while (ConnectedNodesID.Count < Nodes.Length); // dopoki chociaz 1 wygenerowany wezel nie nalezy do grafu
                                                             // (nie ma przyporzadkowanej krawedzi), losujemy kolejne

            //adjacency matrix
            double[,] matrix = new double[NodesCount, NodesCount];

            for (int i=0; i < Nodes.Length; i++)
            {
                for (int j = 0; j < Nodes.Length; j++)
                {
                    if (i == j)
                    {
                        matrix[i, j] = 0;
                    }
                    else
                    {
                        for (int k = 0; k < Edges.Count; k++)
                        {
                            if((Edges[k].StartNode.ID == i && Edges[k].EndNode.ID == j)
                                || (Edges[k].StartNode.ID == j && Edges[k].EndNode.ID == i))
                            {
                                matrix[i, j] = Edges[k].computeLength();
                            }
                        }
                    }
                }
            }
            //end adjacency matrix
        }

        public void Paint(PaintEventArgs e)
        {
            Brush aBrush = (Brush)Brushes.Black;
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 1);


            // rysowanie wezlow
            for (int i = 0; i < NodesCount; i++)
            {
                g.FillRectangle(aBrush, Nodes[i].Point.X, Nodes[i].Point.Y, 5, 5);
            }
            

            for (int i = 0; i < Edges.Count; i++)
            {
                g.DrawLine(pen, Edges[i].StartNode.Point.X, Edges[i].StartNode.Point.Y, Edges[i].EndNode.Point.X, Edges[i].EndNode.Point.Y);
            }
            
        }

        public void Save()
        {
            TextWriter textWriter = new StreamWriter("C:\\Users\\dawid\\Documents\\graph (3)\\Graph\\EdgeList.txt");
            foreach (Edge s in Edges)
            textWriter.WriteLine(s.ToString());
            textWriter.WriteLine("\n");
            foreach (Node n in Nodelist1)
            textWriter.WriteLine(n.ToString());
            textWriter.WriteLine("\n");
            textWriter.Close();
        }
            public void Ser()
        {
            SerializeClass.Serialize2Bytes(Nodelist1,Edges);
        }


        public void findShortestPath()
        {
            Dijkstra dijkstra = new Dijkstra();
            String result = dijkstra.shortestPath(Nodelist1, Edges);
        }
    }


}
