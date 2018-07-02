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
        public List<Node> Nodes = new List<Node>();
        public List<Edge> Edges = new List<Edge>();
        public Edge dijkstraResultNode;
        Random Rand = new Random();
        int NodesCount;
        int branching;

        public List<Node> getNodeList()
        {
            return Nodes;
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
            string dir = "C:\\Graph\\";
            string serializationFileEdge = Path.Combine(dir, "edges.bin");
            using (Stream stream = File.Open(serializationFileEdge, FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bformatter.Serialize(stream, Edges);
            }
           
            string serializationFileNode = Path.Combine(dir, "nodes.bin");
            using (Stream stream = File.Open(serializationFileNode, FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bformatter.Serialize(stream, Nodes);
            }

            MessageBox.Show("Saved to " + dir);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TcpClient tcpServer = new TcpClient();
            tcpServer.StartServer(5000);

            tcpServer.Ser(Nodes, Edges);
            tcpServer.LoopClients();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dijkstra dijkstra = new Dijkstra();
            double resultIndexNode = -1;
            
            string result = dijkstra.shortestPath(Nodes, Edges, ref resultIndexNode);
            MessageBox.Show("Time: " + result);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //czyszczenie list grafu
            using (var g = Graphics.FromImage(pictureBox1.Image))
            {
                g.Clear(Color.White);
                pictureBox1.Refresh();
            }
            Nodes.Clear();
            Edges.Clear();


            if (textBox1.Text == "" || Convert.ToInt32(textBox1.Text) <= 3)
            {
                NodesCount = 3;
            }
            else
            {
                NodesCount = Convert.ToInt32(textBox1.Text);
            }

            if (textBox2.Text == "" || Convert.ToInt32(textBox2.Text) <= 3)
            {
                branching = 3;
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
                Nodes.Add(newNode);
            }

            ConnectedNodesID.Add(0);
            Nodes[0].Branches++;
            do
            {
                do
                {
                    randID = Rand.Next(0, Nodes.Count); // random node having 0 branches
                } while (Nodes[randID].Branches != 0);

                do
                {
                    randConnectedIndex = Rand.Next(0, ConnectedNodesID.Count); // losowy indeks z wierzcholkow, ktore maja przyporzadkowana krawedz
                    randConnectedID = ConnectedNodesID[randConnectedIndex]; // numer konkretnego wierzcholka spod wylosowanego wyzej indeksu
                } while (Nodes[randConnectedID].Branches >= branching);

                Nodes[randID].Branches++;
                ConnectedNodesID.Add(randID);
                if (randConnectedID != 0) Nodes[randConnectedID].Branches++; //Nodes[0].Branches++ occured before loop

                Edge newEdge = new Edge(Nodes[randID], Nodes[randConnectedID]); // krawedz utworzona z dwoch wylosowanych wierzcholkow
                newEdge.ID = Edges.Count;
                Edges.Add(newEdge);
            } while (ConnectedNodesID.Count < Nodes.Count); // dopoki chociaz 1 wygenerowany wezel nie nalezy do grafu
                                                            // (nie ma przyporzadkowanej krawedzi), losujemy kolejne

            using (var g = Graphics.FromImage(pictureBox1.Image))
            {
                Brush aBrush = (Brush)Brushes.Black;
                Pen pen = new Pen(Color.Black, 1);

                // rysowanie wezlow i krawedzi
                for (int i = 0; i < Nodes.Count; i++)
                {
                    g.FillRectangle(aBrush, Nodes[i].Point.X, Nodes[i].Point.Y, 5, 5);
                }

                for (int i = 0; i < Edges.Count; i++)
                {
                    g.DrawLine(pen, Edges[i].StartNode.Point.X, Edges[i].StartNode.Point.Y, Edges[i].EndNode.Point.X, Edges[i].EndNode.Point.Y);
                }
                pictureBox1.Refresh();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //czyszczenie list grafu
            using (var g = Graphics.FromImage(pictureBox1.Image))
            {
                g.Clear(Color.White);
                pictureBox1.Refresh();
            }
            Nodes.Clear();
            Edges.Clear();

            string dir = "C:\\Graph\\";
            string serializationFileEdge = Path.Combine(dir, "edges.bin");
            using (Stream stream = File.Open(serializationFileEdge, FileMode.Open))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                Edges = (List<Edge>)bformatter.Deserialize(stream);

                foreach (Edge ed in Edges)
                {
                    Console.WriteLine(ed);
                }
            }

            string serializationFileNode = Path.Combine(dir, "nodes.bin");
            using (Stream stream = File.Open(serializationFileNode, FileMode.Open))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                Nodes = (List<Node>)bformatter.Deserialize(stream);

                foreach (Node n in Nodes)
                {
                    Console.WriteLine(n);
                }
            }

            using (var g = Graphics.FromImage(pictureBox1.Image))
            {
                Brush aBrush = (Brush)Brushes.Black;
                Pen pen = new Pen(Color.Black, 1);

                // rysowanie wezlow i krawedzi
                for (int i = 0; i < Nodes.Count; i++)
                {
                    g.FillRectangle(aBrush, Nodes[i].Point.X, Nodes[i].Point.Y, 5, 5);
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
