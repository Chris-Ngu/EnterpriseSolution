using ChattingInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace EnterpriseSolution
{
    public partial class MainProgram : Form
    {
        public static IChattingService server;
        private static DuplexChannelFactory<IChattingService> _channelFactory; //sending and receiving to and from server bidirectional 

        public static explicit operator MainProgram(Window v)
        {
            throw new NotImplementedException();
        }

        System.Drawing.Point lastPoint;

        public MainProgram()
        {
            InitializeComponent();
            _channelFactory = new DuplexChannelFactory<IChattingService>(new ClientCallback(), "ChattingServiceEndPoint");
            server = _channelFactory.CreateChannel();

            //Logging into chat server whenever the form gets generated
            int returnValue = server.Login(EnterpriseSolution.UserCache.GetUsername());
            if (returnValue  == 1)
            {
                textBox6.Text = "Status: YOU ARE ALREADY LOGGED IN, TRY AGAIN";
            }
            else if (returnValue == 0)
            {
                textBox6.Text = "Status: Logged in";
                textBox6.Enabled = false;
            }
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
        
        private void MainProgram_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new System.Drawing.Point(e.X, e.Y);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void MainProgram_Activated(object sender, EventArgs e)
        {
            this.textBox2.Text = "Welcome back, " + EnterpriseSolution.UserCache.GetUsername();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Hide();
            panel3.Show();
            panel4.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel2.Hide();
            panel3.Hide();
            panel4.Show();
            monthCalendar1.SetSelectionRange(DateTime.Today, DateTime.Today.AddMonths(2));
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
        public void TakeMessage(string message, string username)
        {
            richTextBox4.Text += username + ": " + message + "\n";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            server.SendMessageToAll(richTextBox5.Text, EnterpriseSolution.UserCache.GetUsername()); //sends the messages to all people connected to the chat
            TakeMessage(richTextBox5.Text, EnterpriseSolution.UserCache.GetUsername()); //Takes the message into the textbox client side
            richTextBox5.Clear();
        }
    }
}
