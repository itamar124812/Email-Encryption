using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StreamSupport
{
    public class TlsStreamSup : streamSupport<SslStream>
    {
        public byte[] ReadStream(SslStream stream)
        {
            byte[] buffer = new byte[2048];
            byte[] result;
            string message = null;
            int length = -1;
            if (stream.CanRead)
            {
                length = stream.Read(buffer, 0, buffer.Length);
                message = Encoding.UTF8.GetString(buffer);
                while (length > 0)
                {
                    if (message.Contains("<EOF>"))
                    {
                        byte[] eof = Encoding.UTF8.GetBytes("<EOF>");
                        int index = Tools.findElementInBytes(buffer, eof);
                        length = index;
                        break;
                    }
                    else
                    {
                        length += stream.Read(buffer, length, buffer.Length - length);
                        message = Encoding.UTF8.GetString(buffer);
                    }
                }
            }
            else
            {
                return null;
                
            }
            result = new byte[length];
            Array.Copy(buffer, result, result.Length);
            return result;
        }

        public void WriteMessage(byte[] message, SslStream stream)
        {
            if (stream.CanWrite)
            {
                stream.Write(message);
                stream.Write(Encoding.UTF8.GetBytes("<EOF>"));
                stream.Flush();
            }
        }
       
    }
}
