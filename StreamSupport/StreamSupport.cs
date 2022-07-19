using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace StreamSupport
{
    internal interface streamSupport<T> where T:Stream
    {
        void WriteMessage(byte[] message,T stream);
        byte[] ReadStream(T stream);
        
    }
}
