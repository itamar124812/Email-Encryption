using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace EmailEncryptionHost
{
    class CaClient
    {
        string email = "";
        TlsClient tls = new TlsClient(serverName);
        byte[] pb = null;
        private X509Certificate caCertificate = null;
        static string serverName="IID";
        public X509Certificate CaCertificate 
        {
            get
            {
                if (caCertificate != null) return caCertificate;
                else return ReadCaCertificate();
            }
        }
        public byte [] getMyPeb
        {
            get
            {
                if(pb!=null) return pb;
                else
                {
                    throw new AccessViolationException("You can't call get getMyPeb before setting the public Key.");
                }
            }
        }
        public CaClient(string _email)
        {
            email = _email;           
        }
        public void SetMyPublicKey(byte[] pb)
        {
            pb = Tools.ArrayUnion(BitConverter.GetBytes(0), pb);
            pb= tls.sendMessage(pb);        
        }
        private X509Certificate2 ReadCaCertificate()
        {
            X509Store store = new X509Store(StoreName.My,StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);
            var result=store.Certificates.Find(X509FindType.FindBySubjectName, serverName, true);
            if (result.Count == 1) return result[0];
            else return null;
        }

    }
}
