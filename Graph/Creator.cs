using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Graph
{
    
   class Creator 
    {
        //edge
        Node[] nodes;
        List<Node> Nodelist = new List<Node>();
        //Node[] nodes; // tablica na wierzcholki
        Random rand = new Random();
        int nodesCount = 50; // liczba wierzcholkow
        List<int> connectedNodesID = new List<int>(); // lista na wierzcholki, ktore dostaly juz krawedz
        List<Edge> edges = new List<Edge>(); // lista krawedzi
        private object data;
        private byte[] bytes;
       
        
        public Node[] Nodes { get => nodes; set => nodes = value; }
        public List<Node> Nodelist1 { get => Nodelist; set => Nodelist = value; }
        public Random Rand { get => rand; set => rand = value; }
        public int NodesCount { get => nodesCount; set => nodesCount = value; }
        public List<int> ConnectedNodesID { get => connectedNodesID; set => connectedNodesID = value; }
        public List<Edge> Edges { get => edges; set => edges = value; }
        public object Data { get => data; set => data = value; }
        public byte[] Bytes { get => bytes; set => bytes = value; }

        public void Load()
        {
            // tworzenie wierzcholkow o losowym polozeniu

            Nodes = new Node[NodesCount];

            for (int i = 0; i < NodesCount; i++)
            {

                Nodes[i] = new Node();
                Nodes[i].Point = new Point(Rand.Next(100, 1000), Rand.Next(100, 500));
                Nodelist1.Add(Nodes[i]);
            }
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

            // losowy punkt startowy
            ConnectedNodesID.Add(Rand.Next(0, Nodes.Length));

            do
            {
                int randConnectedIndex = Rand.Next(0, ConnectedNodesID.Count); // losowy indeks z wierzcholkow, ktore maja przyporzadkowana krawedz
                int randConnectedID = ConnectedNodesID[randConnectedIndex]; // numer konkretnego wierzcholka spod wylosowanego wyzej indeksu
                                                                            // (bedzie to poczatek krawedzi)
                int randID = Rand.Next(0, Nodes.Length); // losowy numer wierzcholka ze wszystkich wierzcholkow (bedzie to koniec krawedzi)
                Edge newEdge = new Edge(Nodes[randConnectedID], Nodes[randID]); // krawedz utworzona z dwoch wylosowanych wierzcholkow

                // sprawdzenie, czy czasem nie mamy juz takiej krawedzi. Jesli nie, to ja dodajemy do listy i rysujemy
                int foundIt = 0;
                for (int i = 0; i < Edges.Count; i++)
                {
                    if (Edges[i] == newEdge) foundIt++;
                }
                if (foundIt == 0)
                {
                    Edges.Add(newEdge);
                    g.DrawLine(pen, newEdge.StartNode.Point.X, newEdge.StartNode.Point.Y, newEdge.EndNode.Point.X, newEdge.EndNode.Point.Y);
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
