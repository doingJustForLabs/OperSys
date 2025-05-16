using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab7_tcp
{
    public class XPProgressBar : ProgressBar
    {
        public XPProgressBar()
        {
            // Включаем визуальный стиль Windows XP
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Рисуем фон
            Rectangle rec = new Rectangle(0, 0, this.Width, this.Height);
            using (var brush = new SolidBrush(BackColor))
            {
                e.Graphics.FillRectangle(brush, rec);
            }

            // Рисуем границу
            ControlPaint.DrawBorder3D(e.Graphics, rec, Border3DStyle.Sunken);

            // Рисуем прогресс
            rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - 4;
            rec.Height -= 4;
            rec.X += 2;
            rec.Y += 2;

            // Градиентная заливка как в XP
            using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                rec,
                Color.FromArgb(0, 192, 0),  // Зеленый (можно изменить)
                Color.FromArgb(0, 128, 0),   // Темно-зеленый
                System.Drawing.Drawing2D.LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, rec);
            }
        }
    }
}
