using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAServer
{
    class CATable
    {
        private static CATable instance;

        public static CATable Instance
        {
            get
            {
                if (instance != null) return instance;
                instance = new CATable();
                return instance;
            }          
        }
        Dictionary<string, byte[]> dic;
        private CATable()
        {
            dic = new Dictionary<string, byte[]>();
        }
        public byte [] getPeb(string email)
        {
            byte[] result;
            dic.TryGetValue(email, out result);
            return result;
        }
        public int addEmail(string email,byte [] pb)
        {
            if (dic.ContainsKey(email)) return -1; 
            dic.Add(email,pb);
            return 0;
        }
        public void removeEmail(string email )
        {
            dic.Remove(email);
        }
        public void changePubKey(string email,byte [] NewPb)
        {
            dic[email] = NewPb;
        }
    }
}
