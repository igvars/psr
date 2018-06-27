using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;

namespace clientNetF4
{
    class ClientDemo
    {
        private TcpClient _client;

        private StreamReader _sReader;
        private StreamWriter _sWriter;
        private Boolean _isConnected;
        String ipAddress;
        int port;
        int aWartosc;
        int bWartosc;
        public ClientDemo()
        {
            Console.WriteLine("Podaj addres ip:  ");
            ipAddress = Console.ReadLine();
            Console.WriteLine("Podaj port:  ");
            port = int.Parse(Console.ReadLine());
            Console.WriteLine("Podaj przedział list:");
            aWartosc = int.Parse(Console.ReadLine());
            bWartosc = int.Parse(Console.ReadLine());
            Console.ReadKey();

            _client = new TcpClient();
            _client.Connect(ipAddress, port);

            HandleCommunication();
        }

        public void HandleCommunication()
        {
            _sReader = new StreamReader(_client.GetStream(), Encoding.ASCII);
            _sWriter = new StreamWriter(_client.GetStream(), Encoding.ASCII);

            _isConnected = true;

            NetworkStream stream = _client.GetStream();

            var bin = new BinaryFormatter();
            var list3 = bin.Deserialize(stream);
            Console.WriteLine(list3);
            Console.ReadLine();
            var list = (List<Graph.Node>)bin.Deserialize(stream);
            var list2 = (List<Graph.Edge>)bin.Deserialize(stream);

            List<Graph.Node> nodeList = new List<Graph.Node>();
            nodeList = list;
            List<Graph.Edge> edgeList = new List<Graph.Edge>();
            edgeList = list2;
            List<Graph.Node> rangeNode = nodeList.GetRange(aWartosc, bWartosc);


            foreach (Graph.Node e in rangeNode)
            {
                Console.WriteLine(e);
            }

            foreach (Graph.Edge e in edgeList)
            {
                Console.WriteLine(e);
            }


            Dijkstra dijkstra = new Dijkstra();
            string result = dijkstra.shortestPath(rangeNode, edgeList);

            Console.WriteLine(result);
            Console.ReadLine();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, result);
            Console.ReadLine();
        }

    }
}
