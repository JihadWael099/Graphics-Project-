using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        //List<KeyValuePair<int, int>> v = new List<KeyValuePair<int, int>>();
        public DataTable DDAtable = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //DDA 
        int DDAx1, DDAx2, DDAy1, DDAy2;
        void DDAline(int x1, int y1, int x2, int y2)
        {
            Bitmap p = new Bitmap(this.Width, this.Height);
            float dx = x2 - x1;
            float dy = y2 - y1;


            float steps;
            if (Math.Abs(dx) > Math.Abs(dy))
                steps = dx;
            else
                steps = dy;

            float xinc = dx / steps;
            float yinc = dy / steps;
            float x = x1;
            float y = y1;
            p.SetPixel((int)x, (int)y, Color.Red);

            for (int i = 0; i < steps; i++)
            {
                x = x + xinc;
                y = y + yinc;
                p.SetPixel((int)x, (int)y, Color.Red);
            }
            pictureBox1.Image = p;
        }

        private void button1_Click(object sender, EventArgs e)
        {


            int v = Convert.ToInt32(textBox1.Text);
            int x1 = v;
            int y1 = Convert.ToInt32(textBox2.Text);

            int x2 = Convert.ToInt32(textBox3.Text);
            int y2 = Convert.ToInt32(textBox4.Text);
            DDAx1 = x1; DDAy1 = y1; DDAx2 = x2;DDAy2 = y2;
            DDAline(x1, y1, x2, y2);

        }



        //Bresenham 
        void Bresenham(int x1, int y1, int x2, int y2)
        {
            Bitmap p = new Bitmap(this.Width, this.Height);

            float dx = x2 - x1;
            float dy = y2 - y1;
            float p0 = (2 * dy) - dx;
            float c1 = 2 * dy;
            float c2 = 2 * (dy - dx);

            float x = x1;
            float y = y1;
            p.SetPixel((int)Math.Round(x), (int)Math.Round(y), Color.Red);

            float x_2 = x2;
            float y_2 = y2;

            while (x < x_2)
            {
                if (p0 < 0)
                    p0 += c1;
                else
                {
                    p0 += c2;
                    y += 1;
                }
                x++;
                p.SetPixel((int)Math.Round(x), (int)Math.Round(y), Color.Red);

            }
            pictureBox1.Image = p;

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            int x1 = Convert.ToInt32(textBox1.Text);
            int y1 = Convert.ToInt32(textBox2.Text);

            int x2 = Convert.ToInt32(textBox3.Text);
            int y2 = Convert.ToInt32(textBox4.Text);

            Bresenham(x1, y1, x2, y2);

        }



        //Circle MidPoint
        void circle(int x_center, int y_center, int r)
        {
            Bitmap picture = new Bitmap(this.Width, this.Height);

            void circleplotpoint(int xCenter, int yCenter, int X, int Y)
            {

                picture.SetPixel(xCenter + X, yCenter + Y, Color.Red);
                picture.SetPixel(xCenter - X, yCenter + Y, Color.Red);
                picture.SetPixel(xCenter + X, yCenter - Y, Color.Red);
                picture.SetPixel(xCenter - X, yCenter - Y, Color.Red);
                picture.SetPixel(xCenter + Y, yCenter + X, Color.Red);
                picture.SetPixel(xCenter - Y, yCenter + X, Color.Red);
                picture.SetPixel(xCenter + Y, yCenter - X, Color.Red);
                picture.SetPixel(xCenter - Y, yCenter - X, Color.Red);

            }

            int x = 0, y = r, p = 1 - r;

            circleplotpoint(x_center, y_center, x, y);

            while (x < y)
            {

                x++;
                if (p < 0)
                {
                    p += 2 * x + 1;
                }
                else
                {
                    y--;
                    p += 2 * (x - y) + 1;
                }

                circleplotpoint(x_center, y_center, x, y);

                pictureBox1.Image = picture;
            }
        }
        private void button3_Click_1(object sender, EventArgs e)
        {

            int x_center = Convert.ToInt32(textBox5.Text);
            int y_center = Convert.ToInt32(textBox6.Text);

            int r = Convert.ToInt32(textBox7.Text);

            if (r > x_center && r > y_center)
            {
                MessageBox.Show("ERROR - radius must be less or equal to x_center and y_center");
            }
            else
            {
                circle(x_center, y_center, r);
            }
        }



        //Ellipse
        void Ellipse(double x_center, double y_center, double r_x, double r_y)
        {
            Bitmap p = new Bitmap(this.Width, this.Height);


            double x = 0, y = r_y;
            double d1 = Math.Pow(r_y, 2) - (Math.Pow(r_x, 2) * r_y) + (0.25 * Math.Pow(r_x, 2));
            double d_x = 2 * Math.Pow(r_y, 2) * x;
            double d_y = 2 * Math.Pow(r_x, 2) * y;

            //Region 1
            while (d_x < d_y)
            {
                p.SetPixel((int)(x + x_center), (int)(y + y_center), Color.Red);
                p.SetPixel((int)(-x + x_center), (int)(y + y_center), Color.Red);
                p.SetPixel((int)(x + x_center), (int)(-y + y_center), Color.Red);
                p.SetPixel((int)(-x + x_center), (int)(-y + y_center), Color.Red);

                if (d1 < 0)
                {
                    x++;
                    d_x = d_x + (2 * Math.Pow(r_y, 2));
                    d1 = d1 + (2 * Math.Pow(r_y, 2) * r_y * x) + (3 * Math.Pow(r_y, 2));
                }
                else
                {
                    x++;
                    y--;
                    d_x = d_x + (2 * Math.Pow(r_y, 2));
                    d_y = d_y - (2 * Math.Pow(d_x, 2));
                    d1 = d1 + d_x - d_y + Math.Pow(r_y, 2);
                }
            }

            //Region 2
            double d2 = Math.Pow(r_y, 2) * Math.Pow(x, 2) + x * Math.Pow(r_y, 2) + (Math.Pow(r_y, 2) / 4)
                + Math.Pow(r_x, 2) * Math.Pow(y, 2) - 2 * y * Math.Pow(r_x, 2) - Math.Pow(r_x, 2) -
                Math.Pow(r_x, 2) * Math.Pow(r_y, 2);
            while (y > 0)
            {
                p.SetPixel((int)(x + x_center), (int)(y + y_center), Color.Red);
                p.SetPixel((int)(-x + x_center), (int)(y + y_center), Color.Red);
                p.SetPixel((int)(x + x_center), (int)(-y + y_center), Color.Red);
                p.SetPixel((int)(-x + x_center), (int)(-y + y_center), Color.Red);

                if (d2 > 0)
                {
                    y--;
                    d_y = d_y - (2 * Math.Pow(r_x, 2));
                    d2 = d2 - d_y + Math.Pow(r_x, 2);
                }
                else
                {
                    y--;
                    x++;
                    d_x = d_x + (2 * Math.Pow(r_y, 2));
                    d_y = d_y - (2 * Math.Pow(r_x, 2));
                    d2 = d2 + d_x - d_y + Math.Pow(r_x, 2);
                }
            }
            pictureBox1.Image = p;
        }

        private void button4_Click(object sender, EventArgs e)
        {

            double x_center = Convert.ToDouble(textBox8.Text);
            double y_center = Convert.ToDouble(textBox9.Text);

            double r_x = Convert.ToDouble(textBox10.Text);
            double r_y = Convert.ToDouble(textBox11.Text);

            if (r_x > x_center && r_x > y_center && r_y > x_center && r_y > y_center)
            {
                MessageBox.Show("ERROR - radius must be less or equal to x_center and y_center");
            }
            else
            {
                Ellipse(x_center, y_center, r_x, r_y);
            }
        }



        //Line Translation
        void translation(int x,int y)
        {
            int x1 = DDAx1 + x, y1 = DDAy1 + y, x2 = DDAx2 + x, y2 = DDAy2 + y;
            DDAline(x1, y1, x2, y2);
        }
        private void button5_Click_2(object sender, EventArgs e)
        {
            int x_center = Convert.ToInt32(textBox12.Text);
            int y_center = Convert.ToInt32(textBox13.Text);

            translation(x_center, y_center);
        }

       

        //Line Rotation
        double Rx1, Rx2, Ry1, Ry2;
        void Rotation(int theta)
        {
            
            Rx1 = DDAx1 * Math.Cos(theta) - DDAy1 * Math.Sin(theta);
            Ry1 = DDAx1 * Math.Sin(theta) + DDAy1 * Math.Cos(theta);
            Rx2 = DDAx2 * Math.Cos(theta) - DDAy2 * Math.Sin(theta);
            Ry2 = DDAx2 * Math.Sin(theta) + DDAy2 * Math.Cos(theta);
            DDAline((int)Rx1, (int)Ry1, (int)Rx2, (int)Ry2);

        }
        private void button6_Click_1(object sender, EventArgs e)
        {
            int theta = Convert.ToInt32(textBox14.Text);
            Rotation(theta);
        }


        void Scaling(int s)
        {
            DDAx1 *= s; DDAy1 *= s; DDAx2 *= s; DDAy2 *= s;
            DDAline(DDAx1, DDAy1, DDAx2, DDAy2);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            int s = Convert.ToInt32(textBox15.Text);
            Scaling(s);
        }



    }


}

