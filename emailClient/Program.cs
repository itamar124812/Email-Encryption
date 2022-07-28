using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using StreamSupport;
using EmailEncryptionHost;

namespace emailClient
{
    class Program
    {
        static int PORT = 11000;
        static TEEImp tee =  new TEEImp();
        static caGetPB CaHelper = caGetPB.Instance;
        private static int r(int x)
        {
            int y = x / (int)Math.Pow(2,l(x)) ;
            return (y - 1) / 2;
        }
        private static int l(int x)
        {
            x = x + 1;
            int y = 0;
            while(x %2==0 )
            {
                x /= 2;
                y++;
            }
            return y;
        }

        static void Main(string[] args)
        {

            tee.genretePublicKey();
            TcpListener server = new TcpListener(new IPEndPoint(IPAddress.Parse("127.0.0.1"), PORT));
            server.Start();
            while (true)
            {
                var client = server.AcceptTcpClient();
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[2000];
                byte[] result=new byte[2];
                int length = 0;            
                while ((length = stream.Read(buffer, 0, buffer.Length)) != 0)
                {

                    string data = UTF8Encoding.UTF8.GetString(buffer,4,length);

                    int option = BitConverter.ToInt32(buffer, 0);
                    int lengthEmail = r(option);
                    option = l(option);
                    switch (option) {
                        case 5:
                        var pb = CaHelper.getBP(data.Substring(0,lengthEmail+1));
                            byte[] message = new byte[length - (lengthEmail + 4)];
                            Array.Copy(buffer, (lengthEmail + 4), message, 0, length - (lengthEmail + 4));
                            result =tee.encreptAndSign(message, pb);                           
                            break;
                        case 6:
                            break;
                    }
                    stream.Write(result, 0, result.Length);                  
                }
                client.Close();
            }
        }


    }
}
