using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web.Script.Serialization;

namespace Graph
{
      static class SerializeClass
    {
        
      
        public static void Serialize2Bytes(object myObjToSerialize,object myObjToSerialize2)
        {
            int a = 0;
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP socket.  
            Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            List<Socket> sockets = new List<Socket>();
            

                try
                {
                    sender.Connect(localEndPoint);
                     Stream stream = new NetworkStream(sender);
                    var bin = new BinaryFormatter();
                    var bin2 = new BinaryFormatter();

                    bin.Serialize(stream, myObjToSerialize);
                    bin2.Serialize(stream, myObjToSerialize2);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

        
            
        }
       }
    }

    //IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
    //        IPAddress ipAddress = ipHostInfo.AddressList[0];
    //        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP socket.  
            //Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            //sender.Connect(localEndPoint);
            
//            try
//            {

//                Stream stream = new NetworkStream(sender);

//                var bin = new BinaryFormatter();
//                var bin2 = new BinaryFormatter();

//                bin.Serialize(stream, myObjToSerialize);
//                bin2.Serialize(stream, myObjToSerialize2);

//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.ToString());
//            }


//        }
//    }
//}













