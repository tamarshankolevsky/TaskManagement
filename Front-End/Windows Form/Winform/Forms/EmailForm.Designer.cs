namespace TaskManagment.Forms.Work
{
    partial class WorkEmail
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_send = new System.Windows.Forms.Button();
            this.txt_contact = new System.Windows.Forms.TextBox();
            this.txt_subject = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_send);
            this.panel1.Controls.Add(this.txt_contact);
            this.panel1.Controls.Add(this.txt_subject);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Font = new System.Drawing.Font("Segoe Print", 9.75F);
            this.panel1.Location = new System.Drawing.Point(92, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(515, 390);
            this.panel1.TabIndex = 1;
            // 
            // btn_send
            // 
            this.btn_send.Font = new System.Drawing.Font("Segoe Print", 9.75F);
            this.btn_send.Location = new System.Drawing.Point(97, 320);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(376, 46);
            this.btn_send.TabIndex = 3;
            this.btn_send.Text = "send ";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txt_contact
            // 
            this.txt_contact.Location = new System.Drawing.Point(96, 90);
            this.txt_contact.Multiline = true;
            this.txt_contact.Name = "txt_contact";
            this.txt_contact.Size = new System.Drawing.Size(376, 224);
            this.txt_contact.TabIndex = 2;
            // 
            // txt_subject
            // 
            this.txt_subject.Location = new System.Drawing.Point(96, 32);
            this.txt_subject.Name = "txt_subject";
            this.txt_subject.Size = new System.Drawing.Size(377, 30);
            this.txt_subject.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe Print", 9.75F);
            this.label2.Location = new System.Drawing.Point(98, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "contact:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe Print", 9.75F);
            this.label1.Location = new System.Drawing.Point(93, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "subject:";
            // 
            // WorkEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 450);
            this.Controls.Add(this.panel1);
            this.Name = "WorkEmail";
            this.Text = "WorkEmail";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.TextBox txt_contact;
        private System.Windows.Forms.TextBox txt_subject;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}