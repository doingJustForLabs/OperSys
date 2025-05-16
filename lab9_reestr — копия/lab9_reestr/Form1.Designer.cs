namespace lab9_reestr
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnEditValue = new System.Windows.Forms.Button();
            this.btnCreateValue = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnDeleteValue = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnBackup = new System.Windows.Forms.Button();
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnCreateSubKey = new System.Windows.Forms.Button();
            this.btnDeleteSubKey = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.listView);
            this.splitContainer1.Size = new System.Drawing.Size(1064, 593);
            this.splitContainer1.SplitterDistance = 328;
            this.splitContainer1.TabIndex = 2;
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(328, 593);
            this.treeView.TabIndex = 1;
            this.treeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_BeforeExpand);
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDeleteSubKey);
            this.groupBox1.Controls.Add(this.btnCreateSubKey);
            this.groupBox1.Controls.Add(this.btnEditValue);
            this.groupBox1.Controls.Add(this.btnCreateValue);
            this.groupBox1.Controls.Add(this.btnRefresh);
            this.groupBox1.Controls.Add(this.btnDeleteValue);
            this.groupBox1.Controls.Add(this.btnRestore);
            this.groupBox1.Controls.Add(this.btnBackup);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 496);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(732, 97);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // btnEditValue
            // 
            this.btnEditValue.Location = new System.Drawing.Point(105, 32);
            this.btnEditValue.Name = "btnEditValue";
            this.btnEditValue.Size = new System.Drawing.Size(72, 39);
            this.btnEditValue.TabIndex = 10;
            this.btnEditValue.Text = "Изменить значение";
            this.btnEditValue.UseVisualStyleBackColor = true;
            this.btnEditValue.Click += new System.EventHandler(this.btnEditValue_Click);
            // 
            // btnCreateValue
            // 
            this.btnCreateValue.Location = new System.Drawing.Point(18, 32);
            this.btnCreateValue.Name = "btnCreateValue";
            this.btnCreateValue.Size = new System.Drawing.Size(72, 39);
            this.btnCreateValue.TabIndex = 5;
            this.btnCreateValue.Text = "Создать значение";
            this.btnCreateValue.UseVisualStyleBackColor = true;
            this.btnCreateValue.Click += new System.EventHandler(this.btnCreateValue_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(465, 32);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(72, 39);
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnDeleteValue
            // 
            this.btnDeleteValue.Location = new System.Drawing.Point(192, 32);
            this.btnDeleteValue.Name = "btnDeleteValue";
            this.btnDeleteValue.Size = new System.Drawing.Size(72, 39);
            this.btnDeleteValue.TabIndex = 6;
            this.btnDeleteValue.Text = "Удалить значение";
            this.btnDeleteValue.UseVisualStyleBackColor = true;
            this.btnDeleteValue.Click += new System.EventHandler(this.btnDeleteValue_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(376, 32);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(72, 39);
            this.btnRestore.TabIndex = 8;
            this.btnRestore.Text = "Восстановить";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // btnBackup
            // 
            this.btnBackup.Location = new System.Drawing.Point(282, 32);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(72, 39);
            this.btnBackup.TabIndex = 7;
            this.btnBackup.Text = "Создать бэкап";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(732, 593);
            this.listView.TabIndex = 2;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Имя";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Тип";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Значение";
            // 
            // btnCreateSubKey
            // 
            this.btnCreateSubKey.Location = new System.Drawing.Point(561, 32);
            this.btnCreateSubKey.Name = "btnCreateSubKey";
            this.btnCreateSubKey.Size = new System.Drawing.Size(72, 39);
            this.btnCreateSubKey.TabIndex = 11;
            this.btnCreateSubKey.Text = "Создать подраздел";
            this.btnCreateSubKey.UseVisualStyleBackColor = true;
            this.btnCreateSubKey.Click += new System.EventHandler(this.btnCreateSubKey_Click);
            // 
            // btnDeleteSubKey
            // 
            this.btnDeleteSubKey.Location = new System.Drawing.Point(648, 32);
            this.btnDeleteSubKey.Name = "btnDeleteSubKey";
            this.btnDeleteSubKey.Size = new System.Drawing.Size(72, 39);
            this.btnDeleteSubKey.TabIndex = 12;
            this.btnDeleteSubKey.Text = "Удалить подраздел";
            this.btnDeleteSubKey.UseVisualStyleBackColor = true;
            this.btnDeleteSubKey.Click += new System.EventHandler(this.btnDeleteSubKey_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 593);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Button btnDeleteValue;
        private System.Windows.Forms.Button btnCreateValue;
        private System.Windows.Forms.Button btnEditValue;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCreateSubKey;
        private System.Windows.Forms.Button btnDeleteSubKey;
    }
}

