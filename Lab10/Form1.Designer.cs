namespace Lab10
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
            this.txtPath = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnWrite = new System.Windows.Forms.Button();
            this.btnBackup = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.treeRegistry = new System.Windows.Forms.TreeView();
            this.lstValues = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnTestChange = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(164, 30);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(371, 29);
            this.txtPath.TabIndex = 0;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(164, 82);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(216, 29);
            this.txtName.TabIndex = 1;
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(164, 134);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(216, 29);
            this.txtValue.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Путь к ключу";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Имя значения";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Значение";
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(17, 271);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(244, 50);
            this.btnRead.TabIndex = 6;
            this.btnRead.Text = "Чтение";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(291, 271);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(244, 50);
            this.btnWrite.TabIndex = 7;
            this.btnWrite.Text = "Запись";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // btnBackup
            // 
            this.btnBackup.Location = new System.Drawing.Point(17, 344);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(244, 50);
            this.btnBackup.TabIndex = 8;
            this.btnBackup.Text = "Бекап";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(291, 344);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(244, 50);
            this.btnRestore.TabIndex = 9;
            this.btnRestore.Text = "Восстановление";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // treeRegistry
            // 
            this.treeRegistry.Location = new System.Drawing.Point(21, 38);
            this.treeRegistry.Name = "treeRegistry";
            this.treeRegistry.Size = new System.Drawing.Size(386, 340);
            this.treeRegistry.TabIndex = 12;
            this.treeRegistry.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeRegistry_BeforeExpand);
            this.treeRegistry.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeRegistry_AfterSelect);
            // 
            // lstValues
            // 
            this.lstValues.FormattingEnabled = true;
            this.lstValues.ItemHeight = 24;
            this.lstValues.Location = new System.Drawing.Point(21, 38);
            this.lstValues.Name = "lstValues";
            this.lstValues.Size = new System.Drawing.Size(386, 340);
            this.lstValues.TabIndex = 13;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeRegistry);
            this.groupBox1.Location = new System.Drawing.Point(553, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(430, 401);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Реестры";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstValues);
            this.groupBox2.Location = new System.Drawing.Point(989, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(430, 401);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Доступные значения";
            // 
            // btnTestChange
            // 
            this.btnTestChange.Location = new System.Drawing.Point(17, 193);
            this.btnTestChange.Name = "btnTestChange";
            this.btnTestChange.Size = new System.Drawing.Size(518, 50);
            this.btnTestChange.TabIndex = 16;
            this.btnTestChange.Text = "Создать тестовый ключ";
            this.btnTestChange.UseVisualStyleBackColor = true;
            this.btnTestChange.Click += new System.EventHandler(this.btnTestChange_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1431, 421);
            this.Controls.Add(this.btnTestChange);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.btnBackup);
            this.Controls.Add(this.btnWrite);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtPath);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.TreeView treeRegistry;
        private System.Windows.Forms.ListBox lstValues;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnTestChange;
    }
}

