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
    public partial class ReceiveForm : Form
    {
        private string senderName;
        private string content;
        private string subject;

        public ReceiveForm()
        {
            InitializeComponent();
        }

        public ReceiveForm(string senderName, string subject, string content)
        {
            this.senderName = senderName;
            this.subject = subject;
            this.content = content;
            InitializeComponent();
            this.label4.Text = senderName;
            this.label2.Text = subject;
            this.BodyContent.Text = content;
            // this.BodyContent.AutoSize = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ReceiveForm_Load(object sender, EventArgs e)
        {

        }

       
    }
}
