using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmailEncryptionHost
{
    static class Tools
    {
        static public byte[] ArrayUnionLength(byte[] array1, byte[] array2)
        {
            byte[] messageLength = BitConverter.GetBytes(array1.Length);
            byte[] result = new byte[array1.Length + messageLength.Length + array2.Length];
            Array.Copy(messageLength, 0, result, 0, messageLength.Length);
            Array.Copy(array1, 0, result, messageLength.Length, array1.Length);
            Array.Copy(array2, 0, result, messageLength.Length + array1.Length, array2.Length);
            return result;
        }
        static public T[] ArrayUnion<T>(T[] first,T[] secund) where T : new()
        {
            T[] result = new T[first.Length + secund.Length];
            Array.Copy(first, 0, result, 0, first.Length);
            Array.Copy(secund, 0, result,  first.Length, secund.Length);
            return result;
        }
    }
}
