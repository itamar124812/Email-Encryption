using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OutlookAddIn
{
    public partial class SendForm : Form
    {

        private static Boolean s_send = false;
        public SendForm(string i_email, string i_subject, string i_content, string i_encrypted, string i_signed)
        {
            InitializeComponent();

            s_send = false;

            this.EmailContentLable.Text = i_content;
            this.EmailSendLable.Text = i_email;
            this.subgectLable.Text = i_subject;
            this.BodyEncrypteLable.Text = i_encrypted;
            this.SignLable.Text = i_signed;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            s_send = true;
            this.Close();
        }

        public static Boolean Send()
        {
            return s_send;
        }
    }
}
