namespace lab7_tcp
{
    partial class ClientChat
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.btnSendFile = new System.Windows.Forms.Button();
            this.gifBox = new System.Windows.Forms.PictureBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.txtReceiverIP = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.gifBox)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(54, 22);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(653, 398);
            this.richTextBox1.TabIndex = 10;
            this.richTextBox1.TabStop = false;
            this.richTextBox1.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(54, 453);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(653, 36);
            this.richTextBox2.TabIndex = 0;
            this.richTextBox2.Text = "";
            this.richTextBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtMessage_KeyDown);
            // 
            // buttonSend
            // 
            this.buttonSend.Enabled = false;
            this.buttonSend.Location = new System.Drawing.Point(744, 453);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(88, 36);
            this.buttonSend.TabIndex = 2;
            this.buttonSend.Text = "Отправить";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(758, 22);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(121, 52);
            this.buttonConnect.TabIndex = 11;
            this.buttonConnect.Text = "Подключиться к чату";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(758, 151);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(121, 20);
            this.txtServerIP.TabIndex = 12;
            // 
            // btnSendFile
            // 
            this.btnSendFile.Enabled = false;
            this.btnSendFile.Location = new System.Drawing.Point(1165, 605);
            this.btnSendFile.Name = "btnSendFile";
            this.btnSendFile.Size = new System.Drawing.Size(137, 68);
            this.btnSendFile.TabIndex = 13;
            this.btnSendFile.Text = "btnSendFile";
            this.btnSendFile.UseVisualStyleBackColor = true;
            this.btnSendFile.Click += new System.EventHandler(this.btnSendFile_Click);
            // 
            // gifBox
            // 
            this.gifBox.Location = new System.Drawing.Point(966, 470);
            this.gifBox.Name = "gifBox";
            this.gifBox.Size = new System.Drawing.Size(336, 50);
            this.gifBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.gifBox.TabIndex = 14;
            this.gifBox.TabStop = false;
            this.gifBox.Visible = false;
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(966, 22);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(336, 398);
            this.txtLog.TabIndex = 15;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // txtReceiverIP
            // 
            this.txtReceiverIP.Location = new System.Drawing.Point(930, 630);
            this.txtReceiverIP.Name = "txtReceiverIP";
            this.txtReceiverIP.Size = new System.Drawing.Size(174, 20);
            this.txtReceiverIP.TabIndex = 16;
            this.txtReceiverIP.Visible = false;
            // 
            // ClientChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1348, 700);
            this.Controls.Add(this.txtReceiverIP);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.gifBox);
            this.Controls.Add(this.btnSendFile);
            this.Controls.Add(this.txtServerIP);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Name = "ClientChat";
            this.Text = "ClientChat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientChat_FormClosing);
            this.Load += new System.EventHandler(this.ClientChat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gifBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.Button btnSendFile;
        private System.Windows.Forms.PictureBox gifBox;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox txtReceiverIP;
    }
}