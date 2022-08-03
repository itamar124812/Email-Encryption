using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Outlook = Microsoft.Office.Interop.Outlook;

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
            var message=(Globals.ThisAddIn.Application.ActiveInspector().CurrentItem as Microsoft.Office.Interop.Outlook.MailItem).Attachments;
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

            var decryptedEmail = Enc.DecryptAndVerify(body, email);

            
        }

        private string readFile(Outlook.Attachments file)
        {
            foreach (var item in file)
            {
                string Filename= (item as Outlook.Attachment).FileName;
                byte[] buffer;
                if (Filename.EndsWith(".bin"))
                {
                    string senderEmail = Globals.ThisAddIn.Application.ActiveInspector().CurrentItem.SenderEmailAddress;
                    string filepath = @"..\" + (item as Outlook.Attachment).FileName;
                    (item as Outlook.Attachment).SaveAsFile(filepath);
                    buffer= File.ReadAllBytes(filepath);
                    Encrypte encrypte = new Encrypte();
                    return encrypte.decrypt(buffer, senderEmail);
                }        
            }
            return null;
        }
        private void checkSignBtn_Click(object sender, EventArgs e)
        {
            string content="";
            string senderEmail = "";
            if (!emailChecked)
                {
                content=readFile(Globals.ThisAddIn.Application.ActiveInspector().CurrentItem.Attachments);
                senderEmail = Globals.ThisAddIn.Application.ActiveInspector().CurrentItem.SenderEmailAddress;

                // findEmailDecrypt(Globals.ThisAddIn.Application.ActiveInspector().CurrentItem.Body);
                emailChecked = true;
            }
            if (content != null || content != "")
            {
                isSignGood = true;
                if(content.StartsWith("m\n"))
                {
                   content= content.Substring(content.IndexOf("m\n") + 2);
                }
                string[] result= content.Split('☀');
                //MessageBox.Show(result[1], result[0]);
                ReceiveForm receive = new ReceiveForm(senderEmail ,result[0],result[1]);
                receive.ShowDialog();
            }
            else
            {
                MessageBox.Show("Somthing wrong with your email.");
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
