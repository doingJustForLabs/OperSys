namespace Lab3
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeView = new System.Windows.Forms.TreeView();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.createButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeView);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(685, 483);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Файловая система";
            // 
            // treeView
            // 
            this.treeView.Location = new System.Drawing.Point(6, 28);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(673, 444);
            this.treeView.TabIndex = 1;
            this.treeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView_BeforeExpand);
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // pathTextBox
            // 
            this.pathTextBox.Location = new System.Drawing.Point(81, 28);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.ReadOnly = true;
            this.pathTextBox.Size = new System.Drawing.Size(427, 29);
            this.pathTextBox.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(17, 188);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(184, 45);
            this.button1.TabIndex = 0;
            this.button1.Text = "Переименовать";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Путь:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(17, 252);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(184, 44);
            this.button2.TabIndex = 2;
            this.button2.Text = "Удалить";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.pathTextBox);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Location = new System.Drawing.Point(703, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(705, 307);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Работа с папками";
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(513, 501);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(184, 45);
            this.createButton.TabIndex = 0;
            this.createButton.Text = "Создать папку";
            this.createButton.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(763, 339);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(435, 238);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1420, 659);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.createButton);
            this.Name = "Form1";
            this.Text = "Лаб3";
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

