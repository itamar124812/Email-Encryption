using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutlookAddIn
{
    class Encrypte
    {
        #region valuseee
        private string m_email;
        private string m_contect;
        private string m_signeture = "";

        public static readonly string p_separator_character = "119988227733664455abcdefh"; //just something random

        public Encrypte(string i_email, string i_contect)
        {
            m_email = i_email;
            m_contect = i_contect;
        }
        public Encrypte()
        {
            
        }
        public string decrypt(byte [] message, string emailSender)
        {           
            byte[] senderEmail = Encoding.UTF8.GetBytes(emailSender);
            return Client.StartClient(codeTwoNunbers(6, emailSender.Length),buffer: senderEmail.Concat(message).ToArray());
        }

        public string getEmail()
        {
            return m_email;
        }

        public string getContect()
        {
            return m_contect;
        }
        

        public string getSigneture()
        {
            return m_signeture;
        }

        #endregion





        /*
         * EncrypteMsg - send "0" + " " + msg - need to return the Encrypte Msg
         * SignMsg - send "1" + " " + senderEmail + "\n" + " EncrypteMsg - need to return the signeture
         * Decrypte - send "2" + " " + senderEmail + "\n" + " EncrypteMsg - need to return the decrypte string
         * VerifySigneture - send "3" + " " + senderEmail + "\n" + signeture + "\n" + EncrypteMsg 
         * - return yes if the signture is good. 
         * 
         */

        public string EncrypteMsg(string body)
        {
            string enc = Client.StartClient(0,body);

            return enc;
        }
        public string EncrypteMsgAndSign(string body, string receiverEmail)
        {
            string data = Client.StartClient(codeTwoNunbers(5, receiverEmail.Length),receiverEmail + "\n" + body );
            m_signeture = data;
            return data;
        }
        public string DecryptAndVerify(string body,string senderEmail)
        {
            string data = Client.StartClient(codeTwoNunbers(6, senderEmail.Length),senderEmail + "\n" + body);
            return data;
        }
        public string SignMsg(string body,string senderEmail)
        {
            string sign = Client.StartClient(1,senderEmail + "\n" + body);
            m_signeture = sign;
            return m_signeture;
        }

        public string Decrypte(string body,string senderEmail)
        {

            string dec = Client.StartClient(2,senderEmail + "\n" + body);
            
            return dec;
        }

        public bool VerifySigneture(string body, string signeture, string senderEmail)
        {
            string sign = Client.StartClient(3,senderEmail + "\n" + signeture + "\n" + body);

            if (sign == "yes")
                return true;
            return false;
        }
        private int codeTwoNunbers(int a,int b)
        {
            return(int)(Math.Pow(2, a)) * (2 * b + 1) - 1;
        }

    }
}
