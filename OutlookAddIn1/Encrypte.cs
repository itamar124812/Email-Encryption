using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutlookAddIn
{
    class Encrypte
    {
        private string m_email;
        private string m_contect;
        private string m_signeture = "";

        public static readonly string p_separator_character = "119988227733664455abcdefh"; //just something random

        public Encrypte(string i_email, string i_contect)
        {
            m_email = i_email;
            m_contect = i_contect;
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

        public string EncrypteMsg(string body)
        {
            //TODO: conecte to TEE

            /*
             * return Tee.EncrypteMsg(body);
             */

            return body;
        }

        public string SignMsg(string body,string senderEmail)
        {
            //TODO: conecte to TEE

            // m_signeture = Tee.sign(body,senderEmail);
            return m_signeture;
        }

        public string Decrypte(string body,string senderEmail)
        {
            //TODO: conecte to TEE

            /*
             * return Tee.decrypte(body, senderEmail);
             */

            return body + "dd-Decrypte";
        }

        public bool VerifySigneture(string body, string signeture, string senderEmail)
        {
            //TODO: conecte to TEE

            /*
             * if(Tee.sign(body,senderEmail)
             * return true;
             * 
             */

            return true;

        }
    }
}
