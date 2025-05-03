namespace Lab9_client
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
            this.chatRichTextBox = new System.Windows.Forms.RichTextBox();
            this.serverIpTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sendTextBtn = new System.Windows.Forms.Button();
            this.messageTextBox = new System.Windows.Forms.TextBox();
            this.connectBtn = new System.Windows.Forms.Button();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chatRichTextBox
            // 
            this.chatRichTextBox.Location = new System.Drawing.Point(328, 12);
            this.chatRichTextBox.Name = "chatRichTextBox";
            this.chatRichTextBox.ReadOnly = true;
            this.chatRichTextBox.Size = new System.Drawing.Size(462, 271);
            this.chatRichTextBox.TabIndex = 0;
            this.chatRichTextBox.Text = "";
            // 
            // serverIpTextBox
            // 
            this.serverIpTextBox.Location = new System.Drawing.Point(124, 40);
            this.serverIpTextBox.Name = "serverIpTextBox";
            this.serverIpTextBox.Size = new System.Drawing.Size(149, 29);
            this.serverIpTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Server IP:";
            // 
            // sendTextBtn
            // 
            this.sendTextBtn.Enabled = false;
            this.sendTextBtn.Location = new System.Drawing.Point(658, 306);
            this.sendTextBtn.Name = "sendTextBtn";
            this.sendTextBtn.Size = new System.Drawing.Size(132, 46);
            this.sendTextBtn.TabIndex = 3;
            this.sendTextBtn.Text = "Отправить";
            this.sendTextBtn.UseVisualStyleBackColor = true;
            this.sendTextBtn.Click += new System.EventHandler(this.sendTextBtn_Click);
            // 
            // messageTextBox
            // 
            this.messageTextBox.Location = new System.Drawing.Point(328, 306);
            this.messageTextBox.Multiline = true;
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.Size = new System.Drawing.Size(324, 46);
            this.messageTextBox.TabIndex = 4;
            // 
            // connectBtn
            // 
            this.connectBtn.Location = new System.Drawing.Point(24, 143);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(249, 51);
            this.connectBtn.TabIndex = 5;
            this.connectBtn.Text = "Подключиться";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(124, 89);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(149, 29);
            this.nameTextBox.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Имя";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.serverIpTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.nameTextBox);
            this.groupBox1.Controls.Add(this.connectBtn);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(298, 340);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Подключение";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 370);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.messageTextBox);
            this.Controls.Add(this.sendTextBtn);
            this.Controls.Add(this.chatRichTextBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox chatRichTextBox;
        private System.Windows.Forms.TextBox serverIpTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button sendTextBtn;
        private System.Windows.Forms.TextBox messageTextBox;
        private System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

