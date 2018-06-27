using Newtonsoft.Json;
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
using System.Web.Script.Serialization;
using System.Xml;
using System.Web;
using System.Runtime.Serialization.Json;

namespace Graph
{
   public  class TcpClient
{
        System.Net.Sockets.TcpClient newClient;
        Creator creator = new Creator();
        List<Node> newNodes;
        List<Edge> newEdges;
        private TcpListener _server;
        private Boolean _isRunning;
        //double[,] matrix = new double[Node,Edge];
        // List<System.Net.Sockets.TcpClient> clients = new List<System.Net.Sockets.TcpClient>();
        int clients = 0;    
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
                clients++;
                Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
                t.Start(newClient);

            }
        }

        public void HandleClient(object obj)
        {

            System.Net.Sockets.TcpClient client = (System.Net.Sockets.TcpClient)obj;
            StreamWriter sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
            StreamReader sReader = new StreamReader(client.GetStream(), Encoding.ASCII);
            BinaryFormatter formatter = new BinaryFormatter();
            NetworkStream stream = newClient.GetStream();
            formatter.Serialize(stream, clients);
            formatter.Serialize(stream, newNodes);
            formatter.Serialize(stream, newEdges);

            sWriter.Flush();
            var bin = new BinaryFormatter();

            var result = bin.Deserialize(stream);
            TextWriter textWriter = new StreamWriter("C:\\Users\\dawid\\Documents\\graph (3)\\Graph\\result.txt");
            textWriter.WriteLine(result);
            textWriter.Close();
            stream.Close();
        }
    }
}

