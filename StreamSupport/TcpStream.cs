using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace StreamSupport
{
    public class TcpStream : streamSupport<NetworkStream>
    {
        public byte[] ReadStream(NetworkStream stream)
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
                        break;
                    }
                }
            }
            result = new byte[length];
            Array.Copy(buffer, result, result.Length);
            return result;
    }
       
        public void WriteMessage(byte[] message, NetworkStream stream)
        {
            if (stream.CanWrite)
            {
                stream.Write(message, 0, message.Length);
                var EndMessage = Encoding.UTF8.GetBytes("<EOF>");
               // stream.Write(EndMessage,0,EndMessage.Length);
                stream.Flush();
            }
        }
    }
}
