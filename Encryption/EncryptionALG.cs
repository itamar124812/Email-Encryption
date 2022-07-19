using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace CAServer
{
    public interface EncryptionALG
    {
        byte[] encryptMessage(byte[] message);
        byte[] descryptMessage(byte[] message);
        byte[] sign(byte[] message,HashAlgorithmName HashAlg);
        bool verify(byte[] sig, byte[] message, HashAlgorithmName HashAlg);
    }
}
