using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Graph;
namespace ClientTEst2
{
    class Program
    {
        List<Edge> edges = new List<Edge>();
        public class SynchronousSocketListener
        {
            // Incoming data from the client.  
            public static string data = null;
            public static void StartListening()
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
                // Create a TCP/IP socket.  
                Socket listener2 = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);
                // Bind the socket to the local endpoint and   
                // listen for incoming connections.  
                try
                {
                    listener2.Bind(localEndPoint);
                    listener2.Listen(10);

                    // Start listening for connections.  
                    while (true)
                    {
                        Console.WriteLine("Waiting for a connection...");
                        // Program is suspended while waiting for an incoming connection.  
                        data = null;
                        // An incoming connection needs to be processed.  
                        while (true)
                        {

                            Socket handler = listener2.Accept();

                            Stream stream = new NetworkStream(handler);
                            var bin = new BinaryFormatter();
                            var list = (List<Graph.Node>)bin.Deserialize(stream);
                            var list2 = (List<Graph.Edge>)bin.Deserialize(stream);

                            foreach (Graph.Node e in list)
                            {
                                Console.WriteLine(e);
                            }
                            Console.WriteLine("--------------------------------------------------------------------------------------------");

                            foreach (Graph.Edge e in list2)
                            {
                                Console.WriteLine(e);
                            }


                        }

                    }
                    

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                Console.WriteLine("\nPress ENTER to continue...");
                Console.Read();

            }
        }
        static void Main(string[] args)
        {

            SynchronousSocketListener.StartListening();

        }
    }
}
