using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace clientNetF4
{
    class ClientDemo
    {
        private TcpClient _client;

        private StreamReader _sReader;
        private StreamWriter _sWriter;
        private Boolean _isConnected;
        public ClientDemo(String ipAddress, int portNum)
        {
            Console.ReadKey();

            _client = new TcpClient();
            _client.Connect(ipAddress, portNum);
           
            HandleCommunication();
        }

        public void HandleCommunication()
        {
            _sReader = new StreamReader(_client.GetStream(), Encoding.ASCII);
            _sWriter = new StreamWriter(_client.GetStream(), Encoding.ASCII);

            _isConnected = true;
            String sData = null;
            
                NetworkStream stream = _client.GetStream();

                var bin = new BinaryFormatter();
                var list = (List<Graph.Node>)bin.Deserialize(stream);

                var list2 = (List<Graph.Edge>)bin.Deserialize(_client.GetStream());

                foreach (Graph.Node e in list)
                {
                    Console.WriteLine(e);
                }

            foreach (Graph.Edge e in list2)
            {
                Console.WriteLine(e);
            }

            Dijkstra dijkstra = new Dijkstra();
            string result = dijkstra.shortestPath(list, list2);
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
