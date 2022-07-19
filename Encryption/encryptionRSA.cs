using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAServer
{
    public class encryptionRSA : EncryptionALG
    {
        int keySize = 2048;
        RSA rSA;
        RSAEncryptionPadding padding = RSAEncryptionPadding.Pkcs1;
        public RSAEncryptionPadding Padding { get => padding; set => padding = value; }
        public encryptionRSA()
        {
           rSA = RSA.Create(keySize);
        }
        public encryptionRSA(int _keySize,RSAEncryptionPadding _padding)
        {
            keySize = _keySize;
            rSA = RSA.Create(keySize);
            padding = _padding;
        }
        public encryptionRSA(RSAParameters parameters)
        {
            rSA.ImportParameters(parameters);

        }
        public encryptionRSA(X509Certificate2 x509)
        {
           rSA= RSACertificateExtensions.GetRSAPrivateKey(x509);
        }

        public byte[] descryptMessage(byte[] message)
        {
            return rSA.Decrypt(message, padding);
        }

        public byte[] encryptMessage(byte[] message)
        {
            return rSA.Encrypt(message, padding);
        }

        public byte[] sign(byte[] message, HashAlgorithmName HashAlg)
        {
            return rSA.SignData(message, HashAlg, RSASignaturePadding.Pkcs1);
        }

        public bool verify(byte[] sig,byte [] message, HashAlgorithmName HashAlg)
        {
            return rSA.VerifyData(message, sig, HashAlg, RSASignaturePadding.Pkcs1);
        }
        public byte[] GetMyPublicKey()
        {
           var parms= rSA.ExportParameters(false);
           var pbexp = parms.Exponent;
           if (parms.Exponent.Length < 4)
            {
                byte [] add = new byte[4- pbexp.Length];
                pbexp = pbexp.Concat(add).ToArray();
            }    
           return parms.Modulus.Concat(parms.Exponent).ToArray() ;
        }
    }
}
