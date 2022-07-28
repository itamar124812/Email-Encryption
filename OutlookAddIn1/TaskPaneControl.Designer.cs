namespace OutlookAddIn
{
    partial class TaskPaneControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkSignBtn = new System.Windows.Forms.Button();
            this.decrypteBtn = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkSignBtn
            // 
            this.checkSignBtn.Location = new System.Drawing.Point(0, 0);
            this.checkSignBtn.Name = "checkSignBtn";
            this.checkSignBtn.Size = new System.Drawing.Size(111, 28);
            this.checkSignBtn.TabIndex = 0;
            this.checkSignBtn.Text = "check sign";
            this.checkSignBtn.UseVisualStyleBackColor = true;
            this.checkSignBtn.Click += new System.EventHandler(this.checkSignBtn_Click);
            // 
            // decrypteBtn
            // 
            this.decrypteBtn.Location = new System.Drawing.Point(0, 45);
            this.decrypteBtn.Name = "decrypteBtn";
            this.decrypteBtn.Size = new System.Drawing.Size(111, 27);
            this.decrypteBtn.TabIndex = 1;
            this.decrypteBtn.Text = "show resulte";
            this.decrypteBtn.UseVisualStyleBackColor = true;
            this.decrypteBtn.Click += new System.EventHandler(this.decrypteBtn_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(57, 18);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(8, 21);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // TaskPaneControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button3);
            this.Controls.Add(this.decrypteBtn);
            this.Controls.Add(this.checkSignBtn);
            this.Name = "TaskPaneControl";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button checkSignBtn;
        private System.Windows.Forms.Button decrypteBtn;
        private System.Windows.Forms.Button button3;
    }
}
