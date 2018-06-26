using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace Graph
{
    class TcpServer
{
        TcpClient newClient;
        Creator creator = new Creator();
        List<Node> newNodes;
        List<Edge> newEdges;
        private TcpListener _server;
        private Boolean _isRunning;
        //double[,] matrix = new double[Node,Edge];
        public void StartServer(int port )
        {

            _server = new TcpListener(IPAddress.Any, port);
            _server.Start();

            _isRunning = true;
        }
       
        public void Ser(List<Node> o1, List<Edge>o2)
        {
            newNodes = o1;
            newEdges = o2;
        }

        public void LoopClients()
        {
            while (_isRunning)
            {
                
                 newClient = _server.AcceptTcpClient();

          
                Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
                t.Start(newClient);
            }
        }

        public void HandleClient(object obj)
        {

            // newEdges = creator.getEdges();
            TcpClient client = (TcpClient)obj;

            StreamWriter sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
            StreamReader sReader = new StreamReader(client.GetStream(), Encoding.ASCII);
         
            Boolean bClientConnected = true;
            //String sData = null;
            
            
                BinaryFormatter formatter = new BinaryFormatter();
                //byte[] buffer = fs.ToArray();
                NetworkStream stream = newClient.GetStream();
                int b = 100;
                MemoryStream fs = new MemoryStream();
                BinaryWriter writer = new BinaryWriter(newClient.GetStream());
                formatter.Serialize(stream, newNodes);
                formatter.Serialize(stream, newEdges);


            //var bin = new BinaryFormatter();
            //var bin2 = new BinaryFormatter();
            // bin2.Serialize(stream, newNodes);

            sWriter.Flush();
            stream.Close();
                // bin.Serialize(newClient.GetStream(), newEdges);
                //sWriter.WriteLine("Witam cie ");
            
        }
    }
}

