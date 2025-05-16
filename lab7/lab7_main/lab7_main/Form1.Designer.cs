namespace lab7_main
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
            this.statusLabel = new System.Windows.Forms.Label();
            this.buttonLoadDLL = new System.Windows.Forms.Button();
            this.listBoxEvents = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(23, 25);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(99, 13);
            this.statusLabel.TabIndex = 4;
            this.statusLabel.Text = "DLL не загружена";
            // 
            // buttonLoadDLL
            // 
            this.buttonLoadDLL.Location = new System.Drawing.Point(26, 52);
            this.buttonLoadDLL.Name = "buttonLoadDLL";
            this.buttonLoadDLL.Size = new System.Drawing.Size(108, 50);
            this.buttonLoadDLL.TabIndex = 5;
            this.buttonLoadDLL.Text = "Загрузить DLL";
            this.buttonLoadDLL.UseVisualStyleBackColor = true;
            this.buttonLoadDLL.Click += new System.EventHandler(this.LoadDllButton_Click);
            // 
            // listBoxEvents
            // 
            this.listBoxEvents.FormattingEnabled = true;
            this.listBoxEvents.HorizontalScrollbar = true;
            this.listBoxEvents.Location = new System.Drawing.Point(26, 121);
            this.listBoxEvents.Name = "listBoxEvents";
            this.listBoxEvents.Size = new System.Drawing.Size(740, 303);
            this.listBoxEvents.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listBoxEvents);
            this.Controls.Add(this.buttonLoadDLL);
            this.Controls.Add(this.statusLabel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button buttonLoadDLL;
        private System.Windows.Forms.ListBox listBoxEvents;
    }
}

