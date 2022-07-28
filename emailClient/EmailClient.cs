using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emailClient
{
    interface EmailClient
    {
        byte[] genretePublicKey();
        byte[] encreptAndSign(byte[] message, byte[] ReciverPK);
        byte[] descreptAndCheck(byte[] message, byte[] pbSender);
    }
}
