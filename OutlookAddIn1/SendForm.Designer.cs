namespace OutlookAddIn
{
    partial class SendForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.EmailSendLable = new System.Windows.Forms.Label();
            this.EmailContentLable = new System.Windows.Forms.Label();
            this.subgectLable = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BodyEncrypteLable = new System.Windows.Forms.Label();
            this.SignLable = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(613, 352);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // EmailSendLable
            // 
            this.EmailSendLable.AutoSize = true;
            this.EmailSendLable.Enabled = false;
            this.EmailSendLable.Location = new System.Drawing.Point(119, 22);
            this.EmailSendLable.Name = "EmailSendLable";
            this.EmailSendLable.Size = new System.Drawing.Size(44, 16);
            this.EmailSendLable.TabIndex = 1;
            this.EmailSendLable.Text = "Email:";
            this.EmailSendLable.Click += new System.EventHandler(this.label1_Click);
            // 
            // EmailContentLable
            // 
            this.EmailContentLable.AutoSize = true;
            this.EmailContentLable.Enabled = false;
            this.EmailContentLable.Location = new System.Drawing.Point(52, 171);
            this.EmailContentLable.Name = "EmailContentLable";
            this.EmailContentLable.Size = new System.Drawing.Size(39, 16);
            this.EmailContentLable.TabIndex = 2;
            this.EmailContentLable.Text = "Body";
            // 
            // subgectLable
            // 
            this.subgectLable.AutoSize = true;
            this.subgectLable.Enabled = false;
            this.subgectLable.Location = new System.Drawing.Point(111, 60);
            this.subgectLable.Name = "subgectLable";
            this.subgectLable.Size = new System.Drawing.Size(52, 16);
            this.subgectLable.TabIndex = 3;
            this.subgectLable.Text = "Subject";
            this.subgectLable.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(51, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Before:";
            this.label1.Click += new System.EventHandler(this.label1_Click_2);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(416, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "After:";
            // 
            // BodyEncrypteLable
            // 
            this.BodyEncrypteLable.AutoSize = true;
            this.BodyEncrypteLable.Enabled = false;
            this.BodyEncrypteLable.Location = new System.Drawing.Point(416, 171);
            this.BodyEncrypteLable.Name = "BodyEncrypteLable";
            this.BodyEncrypteLable.Size = new System.Drawing.Size(39, 16);
            this.BodyEncrypteLable.TabIndex = 6;
            this.BodyEncrypteLable.Text = "Body";
            // 
            // SignLable
            // 
            this.SignLable.AutoSize = true;
            this.SignLable.Enabled = false;
            this.SignLable.Location = new System.Drawing.Point(51, 402);
            this.SignLable.Name = "SignLable";
            this.SignLable.Size = new System.Drawing.Size(56, 16);
            this.SignLable.TabIndex = 9;
            this.SignLable.Text = "Signture";
            // 
            // SendForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SignLable);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BodyEncrypteLable);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.subgectLable);
            this.Controls.Add(this.EmailContentLable);
            this.Controls.Add(this.EmailSendLable);
            this.Controls.Add(this.button1);
            this.Name = "SendForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label EmailSendLable;
        private System.Windows.Forms.Label EmailContentLable;
        private System.Windows.Forms.Label subgectLable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label BodyEncrypteLable;
        private System.Windows.Forms.Label SignLable;
    }
}