namespace lab7_tcp
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtLog = new System.Windows.Forms.TextBox();
            this.txtReceiverIP = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.buttonOpenChat = new System.Windows.Forms.Button();
            this.gifBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.gifBox)).BeginInit();
            this.SuspendLayout();
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(336, 22);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(452, 165);
            this.txtLog.TabIndex = 0;
            // 
            // txtReceiverIP
            // 
            this.txtReceiverIP.Location = new System.Drawing.Point(73, 44);
            this.txtReceiverIP.Name = "txtReceiverIP";
            this.txtReceiverIP.Size = new System.Drawing.Size(174, 20);
            this.txtReceiverIP.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(73, 305);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 68);
            this.button1.TabIndex = 3;
            this.button1.Text = "btnSendFile";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnSendFile_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(70, 106);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(35, 13);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "label1";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // buttonOpenChat
            // 
            this.buttonOpenChat.Location = new System.Drawing.Point(642, 370);
            this.buttonOpenChat.Name = "buttonOpenChat";
            this.buttonOpenChat.Size = new System.Drawing.Size(137, 68);
            this.buttonOpenChat.TabIndex = 5;
            this.buttonOpenChat.Text = "Чат";
            this.buttonOpenChat.UseVisualStyleBackColor = true;
            this.buttonOpenChat.Click += new System.EventHandler(this.buttonOpenChat_Click);
            // 
            // gifBox
            // 
            this.gifBox.Location = new System.Drawing.Point(404, 253);
            this.gifBox.Name = "gifBox";
            this.gifBox.Size = new System.Drawing.Size(336, 50);
            this.gifBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.gifBox.TabIndex = 6;
            this.gifBox.TabStop = false;
            this.gifBox.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gifBox);
            this.Controls.Add(this.buttonOpenChat);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtReceiverIP);
            this.Controls.Add(this.txtLog);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gifBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.TextBox txtReceiverIP;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button buttonOpenChat;
        private System.Windows.Forms.PictureBox gifBox;
    }
}

