namespace lab5
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
            this.listWindows = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttonResize = new System.Windows.Forms.Button();
            this.buttonRename = new System.Windows.Forms.Button();
            this.buttonReplace = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numericPosX = new System.Windows.Forms.NumericUpDown();
            this.numericHeight = new System.Windows.Forms.NumericUpDown();
            this.numericWidth = new System.Windows.Forms.NumericUpDown();
            this.numericPosY = new System.Windows.Forms.NumericUpDown();
            this.listInfo = new System.Windows.Forms.ListBox();
            this.buttonAllInfo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericPosX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPosY)).BeginInit();
            this.SuspendLayout();
            // 
            // listWindows
            // 
            this.listWindows.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listWindows.FormattingEnabled = true;
            this.listWindows.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.listWindows.ItemHeight = 16;
            this.listWindows.Location = new System.Drawing.Point(253, 30);
            this.listWindows.Name = "listWindows";
            this.listWindows.Size = new System.Drawing.Size(356, 388);
            this.listWindows.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // buttonResize
            // 
            this.buttonResize.Location = new System.Drawing.Point(67, 111);
            this.buttonResize.Name = "buttonResize";
            this.buttonResize.Size = new System.Drawing.Size(141, 58);
            this.buttonResize.TabIndex = 1;
            this.buttonResize.Text = "Изменить размер окна";
            this.buttonResize.UseVisualStyleBackColor = true;
            this.buttonResize.Click += new System.EventHandler(this.ResizeWindow);
            // 
            // buttonRename
            // 
            this.buttonRename.Location = new System.Drawing.Point(67, 30);
            this.buttonRename.Name = "buttonRename";
            this.buttonRename.Size = new System.Drawing.Size(141, 58);
            this.buttonRename.TabIndex = 2;
            this.buttonRename.Text = "Изменить название окна";
            this.buttonRename.UseVisualStyleBackColor = true;
            this.buttonRename.Click += new System.EventHandler(this.RenameWindow);
            // 
            // buttonReplace
            // 
            this.buttonReplace.Location = new System.Drawing.Point(67, 193);
            this.buttonReplace.Name = "buttonReplace";
            this.buttonReplace.Size = new System.Drawing.Size(141, 58);
            this.buttonReplace.TabIndex = 3;
            this.buttonReplace.Text = "Изменить положение окна";
            this.buttonReplace.UseVisualStyleBackColor = true;
            this.buttonReplace.Click += new System.EventHandler(this.ReplaceWindow);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(7, 502);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "Пол. по X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(7, 552);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "Пол. по Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(277, 504);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "Ширина";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(277, 554);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 20);
            this.label4.TabIndex = 12;
            this.label4.Text = "Длина";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(112, 464);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = "Положение";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(376, 464);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 20);
            this.label6.TabIndex = 14;
            this.label6.Text = "Размер";
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxTitle.Location = new System.Drawing.Point(597, 504);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(291, 24);
            this.textBoxTitle.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(607, 464);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(122, 20);
            this.label7.TabIndex = 16;
            this.label7.Text = "Название окна";
            // 
            // numericPosX
            // 
            this.numericPosX.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericPosX.Location = new System.Drawing.Point(94, 498);
            this.numericPosX.Maximum = new decimal(new int[] {
            1920,
            0,
            0,
            0});
            this.numericPosX.Name = "numericPosX";
            this.numericPosX.Size = new System.Drawing.Size(120, 24);
            this.numericPosX.TabIndex = 17;
            // 
            // numericHeight
            // 
            this.numericHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericHeight.Location = new System.Drawing.Point(344, 553);
            this.numericHeight.Maximum = new decimal(new int[] {
            1056,
            0,
            0,
            0});
            this.numericHeight.Name = "numericHeight";
            this.numericHeight.Size = new System.Drawing.Size(120, 24);
            this.numericHeight.TabIndex = 18;
            // 
            // numericWidth
            // 
            this.numericWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericWidth.Location = new System.Drawing.Point(344, 501);
            this.numericWidth.Maximum = new decimal(new int[] {
            1936,
            0,
            0,
            0});
            this.numericWidth.Name = "numericWidth";
            this.numericWidth.Size = new System.Drawing.Size(120, 24);
            this.numericWidth.TabIndex = 19;
            // 
            // numericPosY
            // 
            this.numericPosY.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericPosY.Location = new System.Drawing.Point(94, 554);
            this.numericPosY.Maximum = new decimal(new int[] {
            1080,
            0,
            0,
            0});
            this.numericPosY.Name = "numericPosY";
            this.numericPosY.Size = new System.Drawing.Size(120, 24);
            this.numericPosY.TabIndex = 20;
            // 
            // listInfo
            // 
            this.listInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listInfo.FormattingEnabled = true;
            this.listInfo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.listInfo.ItemHeight = 16;
            this.listInfo.Location = new System.Drawing.Point(740, 30);
            this.listInfo.Name = "listInfo";
            this.listInfo.Size = new System.Drawing.Size(356, 388);
            this.listInfo.TabIndex = 21;
            // 
            // buttonAllInfo
            // 
            this.buttonAllInfo.Location = new System.Drawing.Point(67, 282);
            this.buttonAllInfo.Name = "buttonAllInfo";
            this.buttonAllInfo.Size = new System.Drawing.Size(141, 58);
            this.buttonAllInfo.TabIndex = 4;
            this.buttonAllInfo.Text = "Получить информацию об окне";
            this.buttonAllInfo.UseVisualStyleBackColor = true;
            this.buttonAllInfo.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 649);
            this.Controls.Add(this.listInfo);
            this.Controls.Add(this.numericPosY);
            this.Controls.Add(this.numericWidth);
            this.Controls.Add(this.numericHeight);
            this.Controls.Add(this.numericPosX);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxTitle);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonAllInfo);
            this.Controls.Add(this.buttonReplace);
            this.Controls.Add(this.buttonRename);
            this.Controls.Add(this.buttonResize);
            this.Controls.Add(this.listWindows);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericPosX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPosY)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listWindows;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button buttonResize;
        private System.Windows.Forms.Button buttonRename;
        private System.Windows.Forms.Button buttonReplace;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericPosX;
        private System.Windows.Forms.NumericUpDown numericHeight;
        private System.Windows.Forms.NumericUpDown numericWidth;
        private System.Windows.Forms.NumericUpDown numericPosY;
        private System.Windows.Forms.ListBox listInfo;
        private System.Windows.Forms.Button buttonAllInfo;
    }
}

