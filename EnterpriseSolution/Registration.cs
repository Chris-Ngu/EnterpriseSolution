using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace EnterpriseSolution
{
    public partial class Registration : Form
    {
        public Registration()
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

        private void button2_Click(object sender, EventArgs e)
        {
            //need to check if it is empty or if left unchanged from defaults
            if (textBox1.Text.Contains(" ") || textBox1.Text == "Username" || textBox2.Text.Contains(" ") || textBox2.Text == "Password" || textBox3.Text.Contains(" ") || textBox3.Text == "Email" || !textBox3.Text.Contains("@"))
            {
                button2.Text = "ERROR: TRY AGAIN";
                button2.BackColor = Color.Maroon;
            }
            else
            {
                button2.BackColor = Color.OliveDrab;
                button2.Text = "Loading...";

                string username = textBox1.Text;
                string password = textBox2.Text;
                string email = textBox3.Text;
                EnterpriseSolution.MySQLNetworking.Registration(username, password, email);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Startup startup = new Startup();
            startup.ShowDialog();
            this.Close();
        }

        private void Registration_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }
        Point lastPoint;
        private void Registration_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }
    }
}
