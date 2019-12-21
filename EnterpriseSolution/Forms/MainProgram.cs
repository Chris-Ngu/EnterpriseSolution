using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnterpriseSolution
{
    public partial class MainProgram : Form
    {

        public MainProgram()
        {
            InitializeComponent();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void MainProgram_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }
        Point lastPoint;
        private void MainProgram_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainProgram_Activated(object sender, EventArgs e)
        {
            this.textBox2.Text = "Welcome back, " + EnterpriseSolution.UserCache.GetUsername();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel2.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel2.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel2.Hide();
        }
    }
}
