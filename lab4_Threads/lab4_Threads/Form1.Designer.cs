namespace lab4_Threads
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
            this.components = new System.ComponentModel.Container();
            this.startButton = new System.Windows.Forms.Button();
            this.pauseButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.priorityButton = new System.Windows.Forms.Button();
            this.textBoxResult = new System.Windows.Forms.TextBox();
            this.lstThreads = new System.Windows.Forms.ListBox();
            this.textLog = new System.Windows.Forms.TextBox();
            this.logView = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.priorityChangeTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(44, 428);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(159, 106);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Добавить поток";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.AddThread);
            // 
            // pauseButton
            // 
            this.pauseButton.Enabled = false;
            this.pauseButton.Location = new System.Drawing.Point(247, 428);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(174, 106);
            this.pauseButton.TabIndex = 2;
            this.pauseButton.Text = "Пауза всех";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.PauseAllThreads);
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(460, 428);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(174, 106);
            this.stopButton.TabIndex = 3;
            this.stopButton.Text = "Остановить все";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.StopAllThreads);
            // 
            // priorityButton
            // 
            this.priorityButton.Location = new System.Drawing.Point(667, 428);
            this.priorityButton.Name = "priorityButton";
            this.priorityButton.Size = new System.Drawing.Size(174, 106);
            this.priorityButton.TabIndex = 4;
            this.priorityButton.Text = "Изменить приоритет";
            this.priorityButton.UseVisualStyleBackColor = true;
            this.priorityButton.Click += new System.EventHandler(this.ChangePriorityForSelected);
            // 
            // textBoxResult
            // 
            this.textBoxResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxResult.Location = new System.Drawing.Point(44, 78);
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.Size = new System.Drawing.Size(351, 29);
            this.textBoxResult.TabIndex = 5;
            // 
            // lstThreads
            // 
            this.lstThreads.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lstThreads.FormattingEnabled = true;
            this.lstThreads.ItemHeight = 24;
            this.lstThreads.Location = new System.Drawing.Point(722, 12);
            this.lstThreads.Name = "lstThreads";
            this.lstThreads.Size = new System.Drawing.Size(326, 196);
            this.lstThreads.TabIndex = 6;
            // 
            // textLog
            // 
            this.textLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textLog.Location = new System.Drawing.Point(44, 12);
            this.textLog.Name = "textLog";
            this.textLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textLog.Size = new System.Drawing.Size(351, 29);
            this.textLog.TabIndex = 7;
            // 
            // logView
            // 
            this.logView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader1,
            this.columnHeader3,
            this.columnHeader4});
            this.logView.FullRowSelect = true;
            this.logView.GridLines = true;
            this.logView.HideSelection = false;
            this.logView.Location = new System.Drawing.Point(44, 137);
            this.logView.Name = "logView";
            this.logView.Size = new System.Drawing.Size(613, 215);
            this.logView.TabIndex = 8;
            this.logView.UseCompatibleStateImageBehavior = false;
            this.logView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Время";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Тип события";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Поток";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Событие";
            this.columnHeader4.Width = 250;
            // 
            // priorityChangeTimer
            // 
            this.priorityChangeTimer.Tick += new System.EventHandler(this.ChangePriorityDyn);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1135, 591);
            this.Controls.Add(this.logView);
            this.Controls.Add(this.textLog);
            this.Controls.Add(this.lstThreads);
            this.Controls.Add(this.textBoxResult);
            this.Controls.Add(this.priorityButton);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.startButton);
            this.Name = "Form1";
            this.Text = "Работа с потоками";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button pauseButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button priorityButton;
        private System.Windows.Forms.TextBox textBoxResult;
        private System.Windows.Forms.ListBox lstThreads;
        private System.Windows.Forms.TextBox textLog;
        private System.Windows.Forms.ListView logView;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Timer priorityChangeTimer;
    }
}

