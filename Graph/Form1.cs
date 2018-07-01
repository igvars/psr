using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Graph
{
    public partial class Form1 : Form
    {
        Form myForm = new Form();
        public List<Node> Nodelist1 = new List<Node>();
        public List<Edge> Edges = new List<Edge>();
        Random Rand = new Random();
        int NodesCount;
        int branching;

        public List<Node> getNodeList()
        {
            return Nodelist1;
        }
        public List<Edge> getEdges()
        {
            return Edges;
        }

        public Form1()
        {
            InitializeComponent();

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }
        public void Form1_Load(object sender, EventArgs e)
        {
            
        }

        public void Form1_Paint(object sender, PaintEventArgs e)
        {
            
        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            string path = "C:\\Graf\\EdgeList.txt";
            TextWriter textWriter = new StreamWriter(path);
            foreach (Edge s in Edges)
                textWriter.WriteLine(s.ToString());
            textWriter.WriteLine("\n");
            foreach (Node n in Nodelist1)
                textWriter.WriteLine(n.ToString());
            textWriter.WriteLine("\n");
            textWriter.Close();

            MessageBox.Show("Zapisano graf na " + path);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TcpClient tcpServer = new TcpClient();
            tcpServer.StartServer(5000);

            tcpServer.Ser(Nodelist1, Edges);
            tcpServer.LoopClients();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dijkstra dijkstra = new Dijkstra();
            string result = dijkstra.shortestPath(Nodelist1, Edges);
            MessageBox.Show("Time: " + result);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (var g = Graphics.FromImage(pictureBox1.Image))
            {
                g.Clear(Color.White);
                pictureBox1.Refresh();
            }
            Nodelist1.Clear();
            Edges.Clear();
            

            if(textBox1.Text == "" || Convert.ToInt32(textBox1.Text) <= 2)
            {
                NodesCount = 2;
            }
            else
            {
                NodesCount = Convert.ToInt32(textBox1.Text);
            }

            if (textBox2.Text == "" || Convert.ToInt32(textBox2.Text) <= 2)
            {
                branching = 2;
            }
            else
            {
                branching = Convert.ToInt32(textBox2.Text);
            }

            List<int> ConnectedNodesID = new List<int>(); // lista na wierzcholki, ktore dostaly juz krawedz
            ConnectedNodesID.Clear();
            int randConnectedIndex;
            int randConnectedID;
            int randID;

            // tworzenie wierzcholkow o losowym polozeniu
            for (int i = 0; i < NodesCount; i++)
            {
                Node newNode = new Node();
                newNode.Point = new Point(Rand.Next(12, 1800), Rand.Next(12, 720));
                newNode.ID = i;
                Nodelist1.Add(newNode);
            }

            //asdfasdfasdfasdfasdfasdf
              //  asdfasdfsadf

            ConnectedNodesID.Add(Rand.Next(0, Nodelist1.Count));
            do
            {
                
                    randConnectedIndex = Rand.Next(0, ConnectedNodesID.Count); // losowy indeks z wierzcholkow, ktore maja przyporzadkowana krawedz
                    randConnectedID = ConnectedNodesID[randConnectedIndex]; // numer konkretnego wierzcholka spod wylosowanego wyzej indeksu
                                                                            // (bedzie to poczatek krawedzi)
                

               

                
                    randID = Rand.Next(0, Nodelist1.Count); // losowy numer wierzcholka ze wszystkich wierzcholkow (bedzie to koniec krawedzi)
                

                

                Edge newEdge = new Edge(Nodelist1[randConnectedID], Nodelist1[randID]); // krawedz utworzona z dwoch wylosowanych wierzcholkow

                // sprawdzenie, czy czasem nie mamy juz takiej krawedzi. Jesli nie, to ja dodajemy do listy i rysujemy
                int foundIt = 0;
                for (int i = 0; i < Edges.Count; i++)
                {
                    if (Edges[i].compareEdge(newEdge)) foundIt++;
                }
                if (foundIt <= 0)
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
            } while (ConnectedNodesID.Count < Nodelist1.Count); // dopoki chociaz 1 wygenerowany wezel nie nalezy do grafu
                                                                // (nie ma przyporzadkowanej krawedzi), losujemy kolejne

            using (var g = Graphics.FromImage(pictureBox1.Image))
            {
                Brush aBrush = (Brush)Brushes.Black;
                Pen pen = new Pen(Color.Black, 1);

                // rysowanie wezlow i krawedzi
                for (int i = 0; i < Nodelist1.Count; i++)
                {
                    g.FillRectangle(aBrush, Nodelist1[i].Point.X, Nodelist1[i].Point.Y, 5, 5);
                }

                for (int i = 0; i < Edges.Count; i++)
                {
                    g.DrawLine(pen, Edges[i].StartNode.Point.X, Edges[i].StartNode.Point.Y, Edges[i].EndNode.Point.X, Edges[i].EndNode.Point.Y);
                }
                pictureBox1.Refresh();
            }

        }
    }
}
