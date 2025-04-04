namespace Lab4
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
            this.logTextBox = new System.Windows.Forms.RichTextBox();
            this.startBtn = new System.Windows.Forms.Button();
            this.stopBtn = new System.Windows.Forms.Button();
            this.resumeBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.clearBtn = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.stopAllBtn = new System.Windows.Forms.Button();
            this.startAllBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // logTextBox
            // 
            this.logTextBox.Location = new System.Drawing.Point(6, 28);
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.Size = new System.Drawing.Size(569, 502);
            this.logTextBox.TabIndex = 0;
            this.logTextBox.Text = "";
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(17, 40);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(133, 50);
            this.startBtn.TabIndex = 1;
            this.startBtn.Tag = "Дочерний поток 1";
            this.startBtn.Text = "Запуск";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(171, 40);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(181, 50);
            this.stopBtn.TabIndex = 2;
            this.stopBtn.Tag = "Дочерний поток 1";
            this.stopBtn.Text = "Пауза";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.pauseBtn_Click);
            // 
            // resumeBtn
            // 
            this.resumeBtn.Location = new System.Drawing.Point(171, 96);
            this.resumeBtn.Name = "resumeBtn";
            this.resumeBtn.Size = new System.Drawing.Size(181, 50);
            this.resumeBtn.TabIndex = 3;
            this.resumeBtn.Tag = "Дочерний поток 1";
            this.resumeBtn.Text = "Возобновление";
            this.resumeBtn.UseVisualStyleBackColor = true;
            this.resumeBtn.Click += new System.EventHandler(this.resumeBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.resumeBtn);
            this.groupBox1.Controls.Add(this.startBtn);
            this.groupBox1.Controls.Add(this.stopBtn);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(608, 164);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Дочерний поток 1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(17, 96);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 50);
            this.button1.TabIndex = 7;
            this.button1.Tag = "Дочерний поток 1";
            this.button1.Text = "Остановка";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(385, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Приоритет потока";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Низкий",
            "Ниже среднего",
            "Средний",
            "Выше среднего"});
            this.comboBox1.Location = new System.Drawing.Point(390, 106);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(181, 32);
            this.comboBox1.TabIndex = 5;
            this.comboBox1.Tag = "Дочерний поток 1";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.priority_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.clearBtn);
            this.groupBox2.Controls.Add(this.logTextBox);
            this.groupBox2.Location = new System.Drawing.Point(626, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(581, 621);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Вывод";
            // 
            // clearBtn
            // 
            this.clearBtn.Location = new System.Drawing.Point(412, 549);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(163, 51);
            this.clearBtn.TabIndex = 10;
            this.clearBtn.Text = "Очистить";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(171, 40);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(181, 50);
            this.button9.TabIndex = 2;
            this.button9.Tag = "Дочерний поток 3";
            this.button9.Text = "Пауза";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.pauseBtn_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(17, 40);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(133, 50);
            this.button8.TabIndex = 1;
            this.button8.Tag = "Дочерний поток 3";
            this.button8.Text = "Запуск";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(171, 96);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(181, 50);
            this.button7.TabIndex = 3;
            this.button7.Tag = "Дочерний поток 3";
            this.button7.Text = "Возобновление";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.resumeBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(385, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(186, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Приоритет потока";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(17, 96);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(133, 50);
            this.button6.TabIndex = 7;
            this.button6.Tag = "Дочерний поток 3";
            this.button6.Text = "Остановка";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.comboBox3);
            this.groupBox4.Controls.Add(this.button6);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.button7);
            this.groupBox4.Controls.Add(this.button8);
            this.groupBox4.Controls.Add(this.button9);
            this.groupBox4.Location = new System.Drawing.Point(12, 352);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(608, 164);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Дочерний поток 3";
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "Низкий",
            "Ниже среднего",
            "Средний",
            "Выше среднего"});
            this.comboBox3.Location = new System.Drawing.Point(390, 106);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(181, 32);
            this.comboBox3.TabIndex = 9;
            this.comboBox3.Tag = "Дочерний поток 3";
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.priority_SelectedIndexChanged);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(171, 40);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(181, 50);
            this.button5.TabIndex = 2;
            this.button5.Tag = "Дочерний поток 2";
            this.button5.Text = "Пауза";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.pauseBtn_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(17, 40);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(133, 50);
            this.button4.TabIndex = 1;
            this.button4.Tag = "Дочерний поток 2";
            this.button4.Text = "Запуск";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(171, 96);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(181, 50);
            this.button3.TabIndex = 3;
            this.button3.Tag = "Дочерний поток 2";
            this.button3.Text = "Возобновление";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.resumeBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(385, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Приоритет потока";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(17, 96);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(133, 50);
            this.button2.TabIndex = 7;
            this.button2.Tag = "Дочерний поток 2";
            this.button2.Text = "Остановка";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.comboBox2);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.button4);
            this.groupBox3.Controls.Add(this.button5);
            this.groupBox3.Location = new System.Drawing.Point(12, 182);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(608, 164);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Дочерний поток 2";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Низкий",
            "Ниже среднего",
            "Средний",
            "Выше среднего"});
            this.comboBox2.Location = new System.Drawing.Point(390, 106);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(181, 32);
            this.comboBox2.TabIndex = 8;
            this.comboBox2.Tag = "Дочерний поток 2";
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.priority_SelectedIndexChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.stopAllBtn);
            this.groupBox5.Controls.Add(this.startAllBtn);
            this.groupBox5.Location = new System.Drawing.Point(12, 522);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(608, 111);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Общие функции";
            // 
            // stopAllBtn
            // 
            this.stopAllBtn.Location = new System.Drawing.Point(390, 40);
            this.stopAllBtn.Name = "stopAllBtn";
            this.stopAllBtn.Size = new System.Drawing.Size(181, 50);
            this.stopAllBtn.TabIndex = 1;
            this.stopAllBtn.Text = "Остановить все";
            this.stopAllBtn.UseVisualStyleBackColor = true;
            this.stopAllBtn.Click += new System.EventHandler(this.stopAllBtn_Click);
            // 
            // startAllBtn
            // 
            this.startAllBtn.Location = new System.Drawing.Point(17, 39);
            this.startAllBtn.Name = "startAllBtn";
            this.startAllBtn.Size = new System.Drawing.Size(179, 50);
            this.startAllBtn.TabIndex = 0;
            this.startAllBtn.Text = "Запустить все";
            this.startAllBtn.UseVisualStyleBackColor = true;
            this.startAllBtn.Click += new System.EventHandler(this.startAllBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1219, 645);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox logTextBox;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.Button resumeBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button stopAllBtn;
        private System.Windows.Forms.Button startAllBtn;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox2;
    }
}

