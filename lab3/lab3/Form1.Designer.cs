namespace lab3
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
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.createButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.copyButton = new System.Windows.Forms.Button();
            this.replaceButton = new System.Windows.Forms.Button();
            this.renameButton = new System.Windows.Forms.Button();
            this.sizeIndButton = new System.Windows.Forms.Button();
            this.infoButton = new System.Windows.Forms.Button();
            this.restoreButton = new System.Windows.Forms.Button();
            this.selectPathButton = new System.Windows.Forms.Button();
            this.fileSystemWatcher = new System.IO.FileSystemWatcher();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxPath
            // 
            this.textBoxPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPath.Location = new System.Drawing.Point(52, 60);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(616, 29);
            this.textBoxPath.TabIndex = 0;
            this.textBoxPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxPath_KeyDown);
            // 
            // listView1
            // 
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(52, 143);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(681, 265);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            this.listView1.DoubleClick += new System.EventHandler(this.ListView1_DoubleClick);
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(52, 439);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(146, 43);
            this.createButton.TabIndex = 4;
            this.createButton.Text = "Создать";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(481, 514);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(146, 43);
            this.deleteButton.TabIndex = 5;
            this.deleteButton.Text = "Удалить";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // copyButton
            // 
            this.copyButton.Location = new System.Drawing.Point(481, 439);
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(146, 43);
            this.copyButton.TabIndex = 6;
            this.copyButton.Text = "Копировать";
            this.copyButton.UseVisualStyleBackColor = true;
            this.copyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // replaceButton
            // 
            this.replaceButton.Location = new System.Drawing.Point(692, 439);
            this.replaceButton.Name = "replaceButton";
            this.replaceButton.Size = new System.Drawing.Size(146, 43);
            this.replaceButton.TabIndex = 7;
            this.replaceButton.Text = "Переместить";
            this.replaceButton.UseVisualStyleBackColor = true;
            this.replaceButton.Click += new System.EventHandler(this.ReplaceButton_Click);
            // 
            // renameButton
            // 
            this.renameButton.Location = new System.Drawing.Point(263, 439);
            this.renameButton.Name = "renameButton";
            this.renameButton.Size = new System.Drawing.Size(146, 43);
            this.renameButton.TabIndex = 8;
            this.renameButton.Text = "Переименовать";
            this.renameButton.UseVisualStyleBackColor = true;
            this.renameButton.Click += new System.EventHandler(this.RenameButton_Click);
            // 
            // sizeIndButton
            // 
            this.sizeIndButton.Location = new System.Drawing.Point(52, 514);
            this.sizeIndButton.Name = "sizeIndButton";
            this.sizeIndButton.Size = new System.Drawing.Size(146, 43);
            this.sizeIndButton.TabIndex = 9;
            this.sizeIndButton.Text = "Определить размер";
            this.sizeIndButton.UseVisualStyleBackColor = true;
            this.sizeIndButton.Click += new System.EventHandler(this.SizeIndButton_Click);
            // 
            // infoButton
            // 
            this.infoButton.Location = new System.Drawing.Point(263, 514);
            this.infoButton.Name = "infoButton";
            this.infoButton.Size = new System.Drawing.Size(146, 43);
            this.infoButton.TabIndex = 10;
            this.infoButton.Text = "Определить информацию";
            this.infoButton.UseVisualStyleBackColor = true;
            this.infoButton.Click += new System.EventHandler(this.InfoButton_Click);
            // 
            // restoreButton
            // 
            this.restoreButton.Location = new System.Drawing.Point(692, 514);
            this.restoreButton.Name = "restoreButton";
            this.restoreButton.Size = new System.Drawing.Size(146, 43);
            this.restoreButton.TabIndex = 11;
            this.restoreButton.Text = "Восстановить из корзины";
            this.restoreButton.UseVisualStyleBackColor = true;
            this.restoreButton.Click += new System.EventHandler(this.RestoreButton_Click);
            // 
            // selectPathButton
            // 
            this.selectPathButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.selectPathButton.Location = new System.Drawing.Point(701, 61);
            this.selectPathButton.Name = "selectPathButton";
            this.selectPathButton.Size = new System.Drawing.Size(32, 29);
            this.selectPathButton.TabIndex = 12;
            this.selectPathButton.Text = "...";
            this.selectPathButton.UseVisualStyleBackColor = true;
            this.selectPathButton.Click += new System.EventHandler(this.SelectPathButton_Click);
            // 
            // fileSystemWatcher
            // 
            this.fileSystemWatcher.EnableRaisingEvents = true;
            this.fileSystemWatcher.SynchronizingObject = this;
            this.fileSystemWatcher.Changed += new System.IO.FileSystemEventHandler(this.FileSystemWatcher_Changed);
            this.fileSystemWatcher.Created += new System.IO.FileSystemEventHandler(this.FileSystemWatcher_Changed);
            this.fileSystemWatcher.Deleted += new System.IO.FileSystemEventHandler(this.FileSystemWatcher_Deleted);
            this.fileSystemWatcher.Renamed += new System.IO.RenamedEventHandler(this.FileSystemWatcher_Renamed);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 569);
            this.Controls.Add(this.selectPathButton);
            this.Controls.Add(this.restoreButton);
            this.Controls.Add(this.infoButton);
            this.Controls.Add(this.sizeIndButton);
            this.Controls.Add(this.renameButton);
            this.Controls.Add(this.replaceButton);
            this.Controls.Add(this.copyButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.textBoxPath);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button copyButton;
        private System.Windows.Forms.Button replaceButton;
        private System.Windows.Forms.Button renameButton;
        private System.Windows.Forms.Button sizeIndButton;
        private System.Windows.Forms.Button infoButton;
        private System.Windows.Forms.Button restoreButton;
        private System.Windows.Forms.Button selectPathButton;
        private System.IO.FileSystemWatcher fileSystemWatcher;
    }
}

