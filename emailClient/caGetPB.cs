using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using StreamSupport;

namespace emailClient
{
    class caGetPB
    {
        private static caGetPB instance = null;
        public static caGetPB Instance
        {
            get
            {
                if (instance != null) return instance;
                else
                {
                    instance = new caGetPB();
                    return instance;
                }
            }
        }
        IPAddress iP = IPAddress.Parse("127.0.0.1");
        int port = 1000;
        TlsClient client; 
        private caGetPB()
        {
            client =new TlsClient("IID", new IPEndPoint(iP,port));
        }
        public byte [] getBP(string email)
        {
           return client.sendMessage(BitConverter.GetBytes(25).Concat( Encoding.UTF8.GetBytes(email)).ToArray());
       }


    }
}
