using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Net.Sockets;
using System.Net;
using System.Numerics;
using System.Globalization;
using System.IO;
using System.Net.Security;
using System.Security.Authentication;
using StreamSupport;
using System.Threading;

namespace CAServer
{
    class Program
    {
        public static CATable table = CATable.Instance;
        static IPEndPoint iPEnd = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1000);
        static TcpListener server = new TcpListener(iPEnd);
        static EncryptionALG RSA = new encryptionRSA();
        static int keyLength = 260;
        static TlsStreamSup tlsStreamSup=new TlsStreamSup();
        static void Main(string[] args)
        {
            server.Start();
            while (true)
            {
                
                var client = server.AcceptTcpClient();
                ProcessClient(client);
            }
        }
        public static byte[] SignPublicKey(string email,byte [] publicKey)
        {
            if (table.getPeb(email) == publicKey)
            {
                byte[] sig = RSA.sign(publicKey, HashAlgorithmName.SHA256);
                return BitConverter.GetBytes(keyLength).Concat(publicKey.Concat(sig)).ToArray();
            }
            else return null;
        }
        private static X509Certificate2 MYPrivateKey;
        static Program()
        {
            X509Certificate2 x509 = new X509Certificate2();
            X509Certificate2 x5091 = new X509Certificate2();
            X509Store store = new X509Store(StoreName.Root,StoreLocation.CurrentUser);
            X509Store store1 = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadWrite);
            store1.Open(OpenFlags.ReadWrite);
            var result = store.Certificates.Find(X509FindType.FindBySubjectKeyIdentifier, "", false) ;
            if(result.Count==1)
            {
                x509 = result[0];
                MYPrivateKey = x509;
                RSA = new encryptionRSA(x509);
                x5091.Import(genrateMyPb);
                store1.Add(x5091);
            }
            else
            {

            }
            store1.Close();
            store.Close();
        }
        static void ProcessClient(TcpClient client)
        {
            byte[] response = null;
            X509Certificate2 x509 = new X509Certificate2();
            x509.Import(genrateMyPb);
            SslStream sslStream = new SslStream(client.GetStream(), false);
            
            try
            {
                sslStream.AuthenticateAsServer(MYPrivateKey, clientCertificateRequired: false, checkCertificateRevocation: true);
                sslStream.ReadTimeout = 50000;
                sslStream.WriteTimeout = 50000;
                byte[] buffer = tlsStreamSup.ReadStream(sslStream);
                response = BodyServer(buffer);
                if (response!=null) tlsStreamSup.WriteMessage(response,sslStream);
            }
            catch (AuthenticationException e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
                if (e.InnerException != null)
                {
                    Console.WriteLine("Inner exception: {0}", e.InnerException.Message);
                }
                Console.WriteLine("Authentication failed - closing the connection.");
                sslStream.Close();
                client.Close();
                return;
            }
            finally
            {
                sslStream.Close();
                client.Close();
            }
        }
        static byte [] MyPb = null;
        static byte[] genrateMyPb
        {
            get
            {
                if (MyPb != null) return MyPb;
                else
                {
                    X509Certificate2 x509 = new X509Certificate2();
                    var file=Tools.ReadFile(@"pathOfMyPK.crt");
                    x509.Import(file, "", X509KeyStorageFlags.Exportable);
                    MyPb = x509.RawData;
                    return MyPb;
                }
            }
        }

        private static byte [] BodyServer(byte[] bytes)
        {
            byte[] response = null;
            int option = BitConverter.ToInt32(bytes, 0);          
            switch (option)
            {
                case 0:
                    {
                        byte[] pb = new byte[keyLength];
                        byte[] email = new byte[bytes.Length - (keyLength + 4)];
                        Array.Copy(bytes, 4, pb, 0, keyLength);
                        Array.Copy(bytes, (keyLength + 4), email, 0, bytes.Length - (keyLength + 4));
                        string Email = Encoding.ASCII.GetString(email);
                        int flag = table.addEmail(Email, pb);
                        if (flag == 0)
                        {
                            response = SignPublicKey(Encoding.ASCII.GetString(email), pb);
                        }
                        break;
                    }
                case 25:
                    {
                        string email = Encoding.UTF8.GetString(bytes, 4, bytes.Length - 4);
                        response=table.getPeb(email);
                        break;
                    }
                default:
                    break;
            }
            return response;
            
        }

     
        static string ReadMessage(SslStream sslStream)
        {
            // Read the  message sent by the client.
            // The client signals the end of the message using the
            // "<EOF>" marker.
            byte[] buffer = new byte[2048];
            StringBuilder messageData = new StringBuilder();
            int bytes = -1;
            do
            {
                // Read the client's test message.
                bytes = sslStream.Read(buffer, 0, buffer.Length);

                // Use Decoder class to convert from bytes to UTF8
                // in case a character spans two buffers.
                Decoder decoder = Encoding.UTF8.GetDecoder();
                char[] chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
                decoder.GetChars(buffer, 0, bytes, chars, 0);
                messageData.Append(chars);
                // Check for EOF or an empty message.
                if (messageData.ToString().IndexOf("<EOF>") != -1)
                {
                    break;
                }
            } while (bytes != 0);

            return messageData.ToString();
        }
    }
}
