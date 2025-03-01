using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace Lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Random random = new Random();

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double  degrees;
            if (double.TryParse(textBox1.Text, out degrees))
            {
                double angle = Math.PI * degrees / 180.0;
                textBox2.Text = Math.Sin(angle).ToString();
            }
            else
            {
                MessageBox.Show("Ошибка! Введите целое число");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            textBox3.BackColor = Color.LightGray;
            textBox3.Text = "Пришел";
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            textBox3.BackColor = Color.White;
            textBox3.Text = "Ушел";
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private int count = 0;

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            count++;
            label4.Text = "Счетчик: " + count;
        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private int timeleft = 20;


        private void timer1_Tick(object sender, EventArgs e)
        {
            int newX = random.Next(-50, 50);
            int newY = random.Next(-50, 50);
            int maxX = this.groupBox4.Width - pictureBox1.Width;
            int maxY = this.groupBox4.Height - pictureBox1.Height;

            int nextX = Math.Max(0, Math.Min(pictureBox1.Left + newX, maxX));
            int nextY = Math.Max(0, Math.Min(pictureBox1.Top + newY, maxY));

            pictureBox1.Location = new Point(nextX, nextY);

            if (timeleft > 0)
            {
                timeleft--;
                label5.Text = $"Осталось: {timeleft} сек";
            }
            else
            {
                Timer.Stop(); 
                label5.Text = "Время вышло!";
                pictureBox1.Location = new Point(140, 62);
            }
        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            timeleft = 20;
            label5.Text = $"Осталось: {timeleft} сек";
            Timer.Start();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            
        }


    }
}
