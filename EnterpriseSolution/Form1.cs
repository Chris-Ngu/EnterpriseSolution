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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            //pictureBox2.BackgroundImage = Properties.Resources.user2
            tableLayoutPanel1.BackColor = Color.FromArgb(78, 184, 206);
            textBox1.ForeColor = Color.FromArgb(78, 184, 206);

            //pictureBox3.BackgroundImage = Properties.Resources.pass1;
            tableLayoutPanel2.BackColor = Color.WhiteSmoke;
            textBox2.ForeColor = Color.WhiteSmoke;

            //picturebox4.BackgroundImage = Properties.Resoures.email;
            tableLayoutPanel3.BackColor = Color.WhiteSmoke;
            textBox3.ForeColor = Color.WhiteSmoke;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            //pictureBox2.BackgroundImage = Properties.Resources.user2
            tableLayoutPanel1.BackColor = Color.WhiteSmoke;
            textBox1.ForeColor = Color.WhiteSmoke;

            //pictureBox3.BackgroundImage = Properties.Resources.pass1;
            tableLayoutPanel2.BackColor = Color.FromArgb(78, 184, 206);
            textBox2.ForeColor = Color.FromArgb(78, 184, 206);

            //picturebox4.BackgroundImage = Properties.Resoures.email;
            tableLayoutPanel3.BackColor = Color.WhiteSmoke;
            textBox3.ForeColor = Color.WhiteSmoke;
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
            //pictureBox2.BackgroundImage = Properties.Resources.user2
            tableLayoutPanel1.BackColor = Color.WhiteSmoke;
            textBox1.ForeColor = Color.WhiteSmoke;

            //pictureBox3.BackgroundImage = Properties.Resources.pass1;
            tableLayoutPanel2.BackColor = Color.WhiteSmoke;
            textBox2.ForeColor = Color.WhiteSmoke;

            //picturebox4.BackgroundImage = Properties.Resoures.email;
            tableLayoutPanel3.BackColor = Color.FromArgb(78, 184, 206);
            textBox3.ForeColor = Color.FromArgb(78, 184, 206);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
