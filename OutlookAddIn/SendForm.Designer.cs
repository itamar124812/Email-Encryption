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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendForm));
            this.button1 = new System.Windows.Forms.Button();
            this.EmailSendLable = new System.Windows.Forms.Label();
            this.EmailContentLable = new System.Windows.Forms.Label();
            this.subgectLable = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BodyEncrypteLable = new System.Windows.Forms.Label();
            this.SignLable = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(615, 250);
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
            this.EmailSendLable.BackColor = System.Drawing.SystemColors.InactiveBorder;
            //this.EmailSendLable.Enabled = false;
            this.EmailSendLable.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.EmailSendLable.Location = new System.Drawing.Point(133, 10);
            this.EmailSendLable.Name = "EmailSendLable";
            this.EmailSendLable.Size = new System.Drawing.Size(52, 24);
            this.EmailSendLable.TabIndex = 1;
            this.EmailSendLable.Text = "Email";
            // 
            // EmailContentLable
            // 
            this.EmailContentLable.AutoSize = true;
            this.EmailContentLable.BackColor = System.Drawing.SystemColors.InactiveBorder;
            //this.EmailContentLable.Enabled = false;
            this.EmailContentLable.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.EmailContentLable.Location = new System.Drawing.Point(13, 130);
            this.EmailContentLable.Name = "EmailContentLable";
            this.EmailContentLable.Size = new System.Drawing.Size(79, 24);
            this.EmailContentLable.TabIndex = 2;
            this.EmailContentLable.Text = "Message";
            // 
            // subgectLable
            // 
            this.subgectLable.AutoSize = true;
            this.subgectLable.BackColor = System.Drawing.SystemColors.InactiveBorder;
            //this.subgectLable.Enabled = false;
            this.subgectLable.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.subgectLable.Location = new System.Drawing.Point(133, 40);
            this.subgectLable.Name = "subgectLable";
            this.subgectLable.Size = new System.Drawing.Size(75, 24);
            this.subgectLable.TabIndex = 3;
            this.subgectLable.Text = "Subject";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Underline);
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(13, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "Message unencrypted:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Underline);
            this.label2.Location = new System.Drawing.Point(500, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 24);
            this.label2.TabIndex = 8;
            this.label2.Text = "orders:";
            // 
            // BodyEncrypteLable
            // 
            this.BodyEncrypteLable.AutoSize = true;
            this.BodyEncrypteLable.BackColor = System.Drawing.SystemColors.InactiveBorder;
            //this.BodyEncrypteLable.Enabled = false;
            this.BodyEncrypteLable.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.BodyEncrypteLable.Location = new System.Drawing.Point(500, 130);
            this.BodyEncrypteLable.Name = "BodyEncrypteLable";
            this.BodyEncrypteLable.Size = new System.Drawing.Size(50, 24);
            this.BodyEncrypteLable.TabIndex = 6;
            this.BodyEncrypteLable.Text = "Body";
            // 
            // SignLable
            // 
            this.SignLable.AutoSize = true;
            this.SignLable.BackColor = System.Drawing.SystemColors.InactiveBorder;
            //this.SignLable.Enabled = false;
            this.SignLable.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.SignLable.Location = new System.Drawing.Point(120, 250);
            this.SignLable.Name = "SignLable";
            this.SignLable.Size = new System.Drawing.Size(79, 24);
            this.SignLable.TabIndex = 9;
            this.SignLable.Text = "Signture";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Underline);
            this.label3.Location = new System.Drawing.Point(13, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 24);
            this.label3.TabIndex = 10;
            this.label3.Text = "To:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Underline);
            this.label4.Location = new System.Drawing.Point(13, 40);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 24);
            this.label4.TabIndex = 11;
            this.label4.Text = "Subject:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Underline);
            this.label5.Location = new System.Drawing.Point(13, 250);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 24);
            this.label5.TabIndex = 12;
            this.label5.Text = "Signature:";
            // 
            // SendForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(775, 313);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SignLable);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BodyEncrypteLable);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.subgectLable);
            this.Controls.Add(this.EmailContentLable);
            this.Controls.Add(this.EmailSendLable);
            this.Controls.Add(this.button1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SendForm";
            this.Text = "Sent Message";
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}