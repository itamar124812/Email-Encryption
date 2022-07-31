using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StreamSupport
{
    internal static class Tools
    {
        internal static int findElementInBytes(byte[] array, byte[] element)
        {
            int res = -1;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == element[0])
                {
                    for (int j = 1; j < element.Length; j++)
                    {
                        if (array[i + j] != element[j])
                            break;
                        if (j == element.Length - 1)
                            return i;
                    }
                }
            }
            return res;
        }
    }
}
