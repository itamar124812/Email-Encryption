using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools;



namespace OutlookAddIn
{
    public class InspectorWrapper
    {
        private Outlook.Inspector inspector;
        private CustomTaskPane taskPane;

        public InspectorWrapper(Outlook.Inspector Inspector)
        {
            inspector = Inspector;
            ((Outlook.InspectorEvents_Event)inspector).Close +=
                new Outlook.InspectorEvents_CloseEventHandler(InspectorWrapper_Close);

            taskPane = Globals.ThisAddIn.CustomTaskPanes.Add(
                new TaskPaneControl(), "My task pane", inspector);
            taskPane.VisibleChanged += new EventHandler(TaskPane_VisibleChanged);
        }

        void TaskPane_VisibleChanged(object sender, EventArgs e)
        {
            Globals.Ribbons[inspector].Ribbon1.toggleButton1.Checked =
                taskPane.Visible;
        }

        
        void InspectorWrapper_Close()
        {
            if (taskPane != null)
            {
                Globals.ThisAddIn.CustomTaskPanes.Remove(taskPane);
            }

            taskPane = null;
            Globals.ThisAddIn.InspectorWrappers.Remove(inspector);
            ((Outlook.InspectorEvents_Event)inspector).Close -=
                new Outlook.InspectorEvents_CloseEventHandler(InspectorWrapper_Close);
            inspector = null;
        }

        public CustomTaskPane CustomTaskPane
        {
            get
            {
                return taskPane;
            }
        }
    }


    public partial class ThisAddIn
    {
        private Dictionary<Outlook.Inspector, InspectorWrapper> inspectorWrappersValue =
         new Dictionary<Outlook.Inspector, InspectorWrapper>();
        private Outlook.Inspectors inspectors;

        public Dictionary<Outlook.Inspector, InspectorWrapper> InspectorWrappers
        {
            get
            {
                return inspectorWrappersValue;
            }
        }

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            Application.ItemSend += new Outlook.ApplicationEvents_11_ItemSendEventHandler(Application_ItemSend);

            var inspectors = this.Application.Inspectors;
            inspectors.NewInspector +=
                new Outlook.InspectorsEvents_NewInspectorEventHandler(Inspectors_NewInspector);

            foreach (Outlook.Inspector inspector in inspectors)
            {
                Inspectors_NewInspector(inspector);
            }
        }





        void Application_ItemSend(object Item, ref bool Cancel)
        {
            Outlook.MailItem mailItem = Item as Outlook.MailItem;
            var body = mailItem.Body;
            var subject = mailItem.Subject;
            var senderEmail = mailItem.SenderEmailAddress;

            Encrypte Enc = new Encrypte(senderEmail, body);
            string messageToEnc = subject + "☀" + body;
            var encMsg = Enc.EncrypteMsgAndSign(messageToEnc, senderEmail);


            SendForm form = new SendForm(senderEmail, subject, body, "The message is ready for delivery.\nfor sending please press send:", "");
            form.ShowDialog();
            
            

            if (!SendForm.Send())
                Cancel = true;
            else
            {
                var atts = mailItem.Attachments;
                atts.Add(@"..\file.bin");
                mailItem.Subject = "encrypted message";
                mailItem.Body = "The message has been encrypted and signed.\n" + Encrypte.p_separator_character; 
                // mailItem.Body = encMsg + "\n" + Encrypte.p_separator_character ; //enc + (4 * new line) + sign
                Cancel = false;
            }

            
        }

       

        void Inspectors_NewInspector(Outlook.Inspector Inspector)
        {
            if (Inspector.CurrentItem is Outlook.MailItem)
            {
                inspectorWrappersValue.Add(Inspector, new InspectorWrapper(Inspector));
            }
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {

            inspectors.NewInspector -=
    new Outlook.InspectorsEvents_NewInspectorEventHandler(
    Inspectors_NewInspector);
            inspectors = null;
            inspectorWrappersValue = null;


            // Note: Outlook no longer raises this event. If you have code that 
            //    must run when Outlook shuts down, see https://go.microsoft.com/fwlink/?LinkId=506785
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
