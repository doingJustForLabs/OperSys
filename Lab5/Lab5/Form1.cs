using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Assembly dll = Assembly.LoadFrom("ClassLibrary1.dll");
                
                Type formType = dll.GetType("ClassLibrary1.Form1");


                // 3. Создаём экземпляр формы
                Form form = (Form)Activator.CreateInstance(formType);

                // 4. Показываем форму
                form.Show();

            } catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
