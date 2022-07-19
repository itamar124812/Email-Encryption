using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace StreamSupport
{
    public class TlsStreamSup : streamSupport<SslStream>
    {    
        public byte[] ReadStream(SslStream stream)
        {
            byte[] buffer = new byte[2048];
            byte[] result;
            int length = -1;
            if(stream.CanRead)
            {
               length= stream.Read(buffer, 0, buffer.Length);
               length += stream.Read(buffer, 1, buffer.Length - 1);
            }
            result = new byte[length];
            Array.Copy(buffer, result, result.Length);
            return result;
        }

        public void WriteMessage(byte[] message,SslStream stream)
        {
            if (stream.CanWrite)
            {
                stream.Write(message);
                stream.Flush();
            }
        }
    }
}
