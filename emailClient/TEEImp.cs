using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailEncryptionHost;

namespace emailClient
{
    
    class TEEImp : EmailClient
    {

        public EmailEncryptionHost.EmailTee tee=new EmailTee(@"itamar124812@gmail.com");
        public byte[] descreptAndCheck(byte[] message, byte[] pbSender)
        {
            return  tee.descreptAndCheck(message, pbSender);
        }

        public byte[] encreptAndSign(byte[] message, byte[] ReciverPK)
        {
            return tee.encreptAndSign(message, ReciverPK);
        }

        public byte[] genretePublicKey()
        {
            return tee.genretePublicKey();
        }
        public int closeConnection()
        {
            tee.closeConnection();
            return 0;
        }
    }
}
