using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using StreamSupport;
using System.IO;


namespace OutlookAddIn
{

    internal class Client
    {
        const int PORT = 11000;
        const int MAX_LENGTH = 10000;
        private static TcpClient connectClient()
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, PORT);
            TcpClient client = new TcpClient();
            client.Connect(remoteEP);
            return client;
        }

        public static string StartClient(int actionNumber, byte[] buffer)
        {
            var client=connectClient();
            TcpStream streamSup = new TcpStream();
            byte[] bytes = new byte[MAX_LENGTH];
            byte[] NumInByte=BitConverter.GetBytes(actionNumber);
            streamSup.WriteMessage(NumInByte.Concat(buffer).ToArray(), client.GetStream());
            int bytesRec = -1;
            // Receive the response from the remote device.
            bytes = streamSup.ReadStream(client.GetStream());
            client.Close();
            return Encoding.UTF8.GetString(bytes);
        }
        public static string StartClient(int actionNumber,string msg)
        {
            byte[] bytes = new byte[MAX_LENGTH];

            try
            {
                var client = connectClient();
                // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    TcpStream streamSup = new TcpStream();             
                    // Encode the data string into a byte array.
                    byte[] msgByets = BitConverter.GetBytes(actionNumber).Concat(Encoding.UTF8.GetBytes(msg)).ToArray();
                    // Send the data through the socket.
                    streamSup.WriteMessage(msgByets, client.GetStream());
                    int bytesRec = -1;
                    // Receive the response from the remote device.
                    bytes=streamSup.ReadStream(client.GetStream());
                    File.WriteAllBytes(@"..\file.bin", bytes);
                    // Release the socket.
                    client.Close();
                    return Encoding.UTF8.GetString(bytes);
                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return "";
        }
    }
}
