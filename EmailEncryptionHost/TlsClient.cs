using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using StreamSupport;
namespace EmailEncryptionHost
{
    class TlsClient
    {
        TcpClient client = new TcpClient();
        static IPAddress iP = IPAddress.Parse("127.0.0.1");
        static int port = 1000;
        SslStream _stream = null;
        string ServerName;
        TlsStreamSup TlsStreamSup = new TlsStreamSup();
        public TlsClient(string serverName)
        {
            ServerName = serverName;
        }
        SslStream stream
        {
            get
            {
                if (_stream != null) return _stream;
                else
                {
                    connectClient();
                    _stream=new SslStream(client.GetStream(), false, new RemoteCertificateValidationCallback(ValidateServerCertificate), null);
                    try
                    {
                        _stream.AuthenticateAsClient(ServerName);

                    }
                    catch (AuthenticationException e)
                    {
                        Console.WriteLine("Exception: {0}", e.Message);
                        if (e.InnerException != null)
                        {
                            Console.WriteLine("Inner exception: {0}", e.InnerException.Message);
                        }
                        Console.WriteLine("Authentication failed - closing the connection.");
                        client.Close();
                        return null;
                    }
                    return _stream;
                }
            }
        }
        private void connectClient()
        {
            if (client.Connected) return;
            client.Connect(new IPEndPoint(iP, port));
        }
        private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;
            Console.WriteLine("Certificate error: {0}", sslPolicyErrors);
            return false;
        }
        public byte[] sendMessage(byte [] message)
        {
            TlsStreamSup.WriteMessage(message, stream);
            byte[] serverResponse = TlsStreamSup.ReadStream(stream);
            return serverResponse;        
        }
    
    }
}
