﻿using System;
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
    public partial class Startup : Form
    {
        public static MainProgram mp;
        public Startup()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Registration form2 = new Registration();
            form2.ShowDialog();
            this.Close();
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
        }

        private void Startup_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }
        Point lastPoint;
        private void Startup_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //need email socket to send out basic password reset request
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text == "Username" || this.textBox1.Text.Contains(" ") || this.textBox2.Text == "Password" || this.textBox2.Text.Contains(" "))
            {
                button1.Text = "Error: TRY AGAIN";
                button1.BackColor = Color.Maroon;
            }
            else
            {
                button1.BackColor = Color.OliveDrab;
                button1.Text = "Loading...";
                string username = textBox1.Text;
                string password = textBox2.Text;
                bool loginStatus = EnterpriseSolution.MySQLNetworking.login(username, password);
                if (loginStatus == true)
                {
                    EnterpriseSolution.MySQLNetworking.UpdateLoginDate(username, password);
                    EnterpriseSolution.UserCache.username = username;
                    mp = new MainProgram();
                    this.Hide();
                    mp.ShowDialog();
                }
                else if (loginStatus == false)
                {
                    button1.BackColor = Color.Maroon;
                    button1.Text = "No account exists, try again";
                }

            }
        }
    }
}
