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
            if (textBox1.Text == "" || textBox1.Text == "Username" || textBox2.Text == "" || textBox2.Text == "Password" || textBox3.Text == "" || textBox3.Text == "Email")
            {
                button2.Text = "ERROR: TRY AGAIN";
                button2.BackColor = Color.Maroon;
            }
            else
            {
                button2.BackColor = Color.OliveDrab;
                button2.Text = "Loading...";
                string[] loginInformation = {textBox1.Text,textBox2.Text,textBox3.Text}; //Username, password, email
                // include information to register in the domain, as well as check for existing usernames/ emails
                // Catch no connection error in case the server isn't online
            }
        }
    }
}
