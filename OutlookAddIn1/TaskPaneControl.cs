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
    public partial class TaskPaneControl : UserControl
    {
        string bodyEmail = "";
        string email = "";
        string encryptedEmail = "";
        string signeture = "";
        bool isSignGood = false;
        bool emailChecked = false;
        
        
        public TaskPaneControl()
        {
            InitializeComponent();
            
        }

        private void findEmailDecrypt(string body)
        {
            bodyEmail = Globals.ThisAddIn.Application.ActiveInspector().CurrentItem.Body;
            email = Globals.ThisAddIn.Application.ActiveInspector().CurrentItem.SenderEmailAddress;

            var tempBody = body.Split(new string[] { Encrypte.p_separator_character }, StringSplitOptions.None);
            tempBody = tempBody.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            if (tempBody.Length > 1)
            {
                encryptedEmail = tempBody[0];
                signeture = tempBody[1];
            }
            else
            {
                encryptedEmail = tempBody[0];
                signeture = "";
            }

            var Enc = new Encrypte(email, encryptedEmail);

            var decryptedEmail = Enc.Decrypte(encryptedEmail,email);
            this.isSignGood = Enc.VerifySigneture(encryptedEmail, signeture,email);


        }

        private void checkSignBtn_Click(object sender, EventArgs e)
        {
            if (!emailChecked)
                {
                findEmailDecrypt(Globals.ThisAddIn.Application.ActiveInspector().CurrentItem.Body);
                emailChecked = true;
            }
            if (isSignGood)
            {
                MessageBox.Show("Sign is good");
            }
            else
            {
                MessageBox.Show("Sign is bad");
            }

        }

        private void decrypteBtn_Click(object sender, EventArgs e)
        {
            if (!emailChecked)
            {
                findEmailDecrypt(Globals.ThisAddIn.Application.ActiveInspector().CurrentItem.Body);
                emailChecked = true;
            }
            MessageBox.Show(encryptedEmail,"your email is:");
        }
    }
}
